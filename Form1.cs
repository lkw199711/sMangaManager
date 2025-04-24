using globalData;
using lkw;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using website;
using static website.Bangumi;

namespace sMangaManager
{
    public partial class FormIndex : Form
    {
        Lkw lkw = new Lkw();
        List<website.ChapterItem> chapters;
        public FormIndex()
        {
            InitializeComponent();
        }

        public void config_load()
        {
            // 读取 JSON 文件
            string json = File.ReadAllText("./config.json");

            // 反序列化 JSON 到对象
            Config globalJson = JsonConvert.DeserializeObject<Config>(json);
            global.config = globalJson;
            comboBoxRoute.DataSource = global.config.routers;
        }

        public void config_update()
        {
            string filePath = "config.json"; // 当前路径下的 config.json 文件
            string json = JsonConvert.SerializeObject(global.config, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public class Config
        {

            public BindingList<string> routers { get; set; }

            public Config()
            {

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string filePath = "config.json"; // 当前路径下的 config.json 文件

            // 判断文件是否存在
            if (!File.Exists(filePath))
            {
                Config config = new Config();
                config.routers = new BindingList<string>();

                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                // 创建文件并写入空的 JSON 对象
                File.WriteAllText(filePath, json);
            }

            // 读取配置信息
            config_load();

            // 获取当前程序集
            Assembly assembly = Assembly.GetExecutingAssembly();

            // 获取程序集版本号
            Version version = assembly.GetName().Version;

            // 输出版本号信息
            Console.WriteLine("程序集版本号：" + version.ToString());

            labelVersion.Text = "版本号：" + version.ToString();

            textBoxUrl.TextChanged += (sender1, e1) => global.url = textBoxUrl.Text;
            richTextBoxHtml.TextChanged += (s2, e2) => global.html = richTextBoxHtml.Text;
        }

        /// <summary>
        /// 根据链接获取页面html代码-使用请求
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <returns>html代码</returns>
        protected string get_html_by_request(string url, string cookie = "", string referer = "")
        {
            var data = new byte[4];
            new Random().NextBytes(data);
            string ip = new IPAddress(data).ToString();

            Encoding encode = Encoding.GetEncoding("utf-8");
            try
            {
                //构造httpwebrequest对象，注意，这里要用Create而不是new
                HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(url);

                //伪造浏览器数据，避免被防采集程序过滤
                //wReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; .NET CLR 1.1.4322; .NET CLR 2.0.50215; CrazyCoder.cn;www.aligong.com)";
                wReq.UserAgent = "Mozilla/5.0 (Linux; Android 9; Mi Note 3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.132 Mobile Safari/537.36";
                wReq.Accept = "*/*";
                wReq.KeepAlive = true;
                wReq.Headers.Add("cookie", cookie);

                wReq.Headers.Add("origin", referer);
                wReq.Headers.Add("x-requested-with", "XMLHttpRequest");

                wReq.Headers.Add("pragma", "no-cache");
                wReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                wReq.Headers.Add("sec-fetch-dest", "document");
                wReq.Headers.Add("sec-fetch-mode", "navigate");
                wReq.Headers.Add("sec-fetch-site", "none");
                wReq.Headers.Add("sec-fetch-user", "?1");

                //wReq.Headers.Add("User-Agent", "Mozilla/5.0 (Linux; U; Android 9; zh-cn; Mi Note 3 Build/PKQ1.181007.001) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/71.0.3578.141 Mobile Safari/537.36 XiaoMi/MiuiBrowser/11.8.14");
                //wReq.Headers.Add("Pragma", "no-cache");
                //wReq.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
                //wReq.Headers.Add("Connection", "Keep-Alive");
                //随机ip
                //wReq.Headers.Add("CLIENT-IP", ip);
                //wReq.Headers.Add("X-FORWARDED-FOR", ip);
                //注意，为了更全面，可以加上如下一行，避开ASP常用的POST检查              

                wReq.Referer = referer;//您可以将这里替换成您要采集页面的主页

                HttpWebResponse wResp = wReq.GetResponse() as HttpWebResponse;
                // 获取输入流
                System.IO.Stream respStream = wResp.GetResponseStream();

                System.IO.StreamReader reader = new System.IO.StreamReader(respStream, encode);

                string content = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();

                return content;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return "";
        }
        /// <summary>
        /// 测试按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTest_Click(object sender, EventArgs e)
        {
            string html = get_html_by_request("https://manga.bilibili.com/detail/mc34355", "");
            Console.WriteLine(html);
        }

        public async Task bilibili_test()
        {
            
            string subjectUrl = $"https://manga.bilibili.com/twirp/comic.v1.Comic/ComicDetail";
            using (HttpClient client = new HttpClient())
            {
                // 添加自定义的请求头
                client.DefaultRequestHeaders.Add("Origin", "https://manga.bilibili.com");
                client.DefaultRequestHeaders.Add("Referer", "https://manga.bilibili.com");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/134.0.0.0 Safari/537.36");
                // 设置请求内容
                string comic_id = "34355";
                var jsonData = $"{{\"comic_id\":{comic_id}}}";
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage response = await client.PostAsync(subjectUrl, content);
                    response.EnsureSuccessStatusCode(); // 确保请求成功

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                    Console.WriteLine(json);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("请求失败: " + e.Message);
                }
            }
        }


        /// <summary>
        /// 重命名按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReName_Click(object sender, EventArgs e)
        {
            string dir = "";
            string url = textBoxUrl.Text;

            string html = richTextBoxHtml.Text;

            TopToon toptoon = new TopToon(url: url, cookie: global.cookie, downloadSwitch: false, html: html, saveRoute: comboBoxRoute.Text);

            List<string> list = new List<string>();

            chapters = toptoon.chapters;

            BindingList<FileInfo> fileList = new BindingList<FileInfo>();
            BindingList<string> chapterList = new BindingList<string>();

            foreach (string fileName in Directory.GetFileSystemEntries(dir))
            {
                // 目标为文件夹与压缩包 发现为图片则剔除
                if (is_img(fileName)) continue;

                fileList.Add(new FileInfo(fileName));

                lkw.log(fileName);

                // 替换文件名
                //replace(fileName, oldStr, newStr);
            }

            chapters.ForEach(item =>
            {
                chapterList.Add(item.name);
            });
            FormRename form2 = new FormRename(fileList, chapterList, toptoon.name, "");
            form2.Show();
        }

        /// <summary>
        /// 下载章节封面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDownLoad_Click(object sender, EventArgs e)
        {
            string url = textBoxUrl.Text;
            string html = richTextBoxHtml.Text;

            string cookie = "language=tw; viewModeSaving=1; userKeyToken=15d27a24e1a22987d24aee4c99c1917e_1689925853; service_key=e7cdac5be1e5cd7b6041b87bfb616665; _gcl_au=1.1.2074426609.1689925857; __lt__cid=75a9432a-e104-4e09-8a16-dbab6d5b9064; _ga=GA1.1.1319261843.1689925859; adultStatus=1; notOpenConfirmAdult=1; saveId=lkw199711%40163.com; freeTimeGiveInfo=%7B%22LD3%22%3A1689926119%7D; isLoginUsed=1; net_session=elFmR3g5UWRsYm1ZZlIrOHlNNk4wRWdHTTd5b1dDeHA0MDRVblNXK0VmV1N1RjFhWXJOZStqUlIxcnU5bTA5VUZkVDgvS1lwUlNFdldKM3JBTkFvZlNWUEkzQkU2cUxPbndmcVFuUFB2QmV5bGJXRnlMOHlpcjNtOUMyWDZaZWRCOWVvbmZOdXMzUkpDQ3V5MERnbHhvYmhORmEyVmVJZ0F3NEtGbVN4SkMzbFRLVWU0Q2d1VVB2aEs3bXprNy9MYy9sRE42VU9Wd2FJRzR6UHExZUozUnR5WDhVeCtETlJ1U1NvMG53OFZneEVwSmNuRUxydlQ2K3FoU09XbTNtdTZ3T1gvYTRYUnFHdTBMTXprMGUxbW9GWFk5RlVEaUtNRFhPNDRTTlFEc2g4VmoraGJlazBtV3JwM3BEZEFsREFHdFN4VXlhMGtzcnZOR284cW4rdVJnbGI0b1Fabi9kL2V1Rk12N3JmNGUxNTlmRHpEREQ0QVhwcm9vdVlrVzFCMktwTXV3S0t5SlRtYzBqdlpCZGpGZzZJclFvcmQ0SnlqLzBJVjhEa0pZbEJYNzlYQkxnYk9PUWhSZldzMXpiM3p5aHFSMzRjamVCOHFEazliYWtsQ3g5N3V4L0wvVEhYS3VQclJ5eFViQVpaZkFJZGlLOTl1SW1CeTA4alBBamI3Tmw4VjhOZzZteWtqa0JROWgwdUdWdnc5VGVGakxlWWNWOHhKMFdaYVZQYjJVR05PZG5MQ1B6eTZaV2NZeHlGf0118085649aad90f5226e57852d563f581163bc; userGender=1; notOpenGiftNotice=1; existsBannerTop=1; userKey=6BC96BA0A3A0CD6122EC2C2319EEBB921D8A4763360662553ECACCED9DF9C108; comicEpListSort=asc; userRecentTop10ListToLS=1; viewNonstop=1; notOpenPhoneCertNotice=1; notOpenAttendancePopup=1; hotTimeGiveInfo=%7B%22hotTimeSet%22%3A1689933579%7D; notOpenPopupNotice=1; __lt__sid=c93d9a72-7fc17385; epListAccessPath=%2Fsearch; spush_nowUrl=https%253A%252F%252Fwww.toptoon.net%252Fcomic%252FepList%252F80643; _ga_CZ8J0XSEDJ=GS1.1.1689933471.3.1.1689933617.31.0.0";

            _ = lkw.NewWork(() =>
            {
                TopToon toptoon = new TopToon(url: url, cookie: cookie, html: html, saveRoute: comboBoxRoute.Text);
                toptoon.download_chapter_poster();
                toptoon.download_chapter_poster();
                //new TopToon(url, cookie, true, html);
                //toptoon.chapters.ForEach((website.ChapterItem i) => { toptoon.download_chapter_images(i); });

                lkw.msbox("任务执行完毕");
            });
        }

        /// <summary>
        /// 单页下载
        /// </summary>
        public void single_page()
        {
            string url = textBoxUrl.Text; string html = richTextBoxHtml.Text;


            MatchCollection imageList = new Regex("(?<=class=\"cImg\"><img\\sdata-src=\")[^\"]+", RegexOptions.Singleline).Matches(html);

            lkw.NewWork(() =>
            {
                for (int i = 0, l = imageList.Count; i < l; i++)
                {
                    string imgUrl = "https:" + imageList[i].Value;
                    imgUrl = imgUrl.Replace("amp;", "");
                    lkw.log(imgUrl);
                    download_image_by_http(imgUrl, ".\\single-page\\" + i.ToString().PadLeft(5, '0') + ".jpg");

                }
            });
        }

        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="path">文件存放地址，包含文件名</param>
        /// <returns>文件是否保存成功</returns>
        protected static bool download_image_by_http(string url, string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);    //存在则删除
            }

            string tempPath = System.IO.Path.GetDirectoryName(path) + @"\temp";
            System.IO.Directory.CreateDirectory(tempPath);  //创建临时文件目录
            string tempFile = tempPath + @"\" + System.IO.Path.GetFileName(path) + ".temp"; //临时文件
            /*
            if (System.IO.File.Exists(tempFile))
            {
                System.IO.File.Delete(tempFile);    //存在则删除
            }
            */
            try
            {
                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                // 添加请求头
                request.Headers.Add("sec-ch-ua", "\"Not(A:Brand\";v=\"99\", \"Google Chrome\";v=\"133\", \"Chromium\";v=\"133\"");
                request.Headers.Add("sec-ch-ua-mobile", "?0");
                request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
                request.Headers.Add("upgrade-insecure-requests", "1");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/133.0.0.0 Safari/537.36";
                request.Accept = "image/webp,image/apng,image/*,*/*;q=0.8";
                request.Headers.Add("Accept-Language", "en-US,en;q=0.9");

                string domain = "https://www.toptoon.net/";
                CookieContainer cookies = ConvertStringToCookieContainer(global.cookie, domain);
                request.CookieContainer = cookies;

                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                //Stream stream = new FileStream(tempFile, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    //stream.Write(bArr, 0, size);
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                //stream.Close();
                fs.Close();
                responseStream.Close();
                System.IO.File.Move(tempFile, path);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public static CookieContainer ConvertStringToCookieContainer(string cookieString, string url)
        {
            CookieContainer cookieContainer = new CookieContainer();
            Uri uri = new Uri(url);

            // 分割 cookie 字符串
            string[] cookies = cookieString.Split(';');
            foreach (string cookie in cookies)
            {
                string[] cookieParts = cookie.Split('=');
                if (cookieParts.Length == 2)
                {
                    string cookieName = cookieParts[0].Trim();
                    string cookieValue = cookieParts[1].Trim();
                    cookieContainer.Add(uri, new Cookie(cookieName, cookieValue));
                }
            }

            return cookieContainer;
        }


        /// <summary>
        /// 单页下载按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSinglePage_Click(object sender, EventArgs e)
        {
            single_page();
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            string url = textBoxUrl.Text; string html = richTextBoxHtml.Text;

            lkw.NewWork(() =>
            {
                TopToon toptoon = new TopToon(url: url, cookie: global.cookie, html: html, saveRoute: comboBoxRoute.Text);
                toptoon.set_info();
            });
        }

        /// <summary>
        /// 下载横幅图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBanner_Click(object sender, EventArgs e)
        {
            string url = textBoxUrl.Text; string html = richTextBoxHtml.Text;

            lkw.NewWork(() =>
            {
                TopToon toptoon = new TopToon(url: url, cookie: global.cookie, html: html, saveRoute: comboBoxRoute.Text);
                toptoon.download_manga_info_poster();
            });
        }

        /// <summary>
        /// 下载全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAll_Click(object sender, EventArgs e)
        {
            string url = textBoxUrl.Text;
            string html = richTextBoxHtml.Text;
            string saveRoute = comboBoxRoute.Text;

            _ = lkw.NewWork(() =>
            {
                //顶通
                if (global.url.Contains("toptoon"))
                {
                    TopToon toptoon = new TopToon(url: url, cookie: global.cookie, html: html, saveRoute: saveRoute);
                    toptoon.download_chapter_poster();
                    toptoon.download_chapter_poster();
                    toptoon.download_manga_info_poster();
                    toptoon.set_info();
                }
                // 玩漫
                if (global.url.Contains("toomics"))
                {
                    Toomics toomics = new Toomics(url: url, cookie: global.cookie, html: html, saveRoute: saveRoute);
                    toomics.download_chapter_poster();
                    toomics.download_chapter_poster();
                    toomics.download_manga_info_poster();
                    toomics.set_info();
                }
                //bangumi
                if (global.url.Contains("bangumi"))
                {
                    Bangumi bangumi;
                    bangumi = new Bangumi(url: global.url, cookie: global.cookie, html: global.html, saveRoute: @"D:\tmp");
                    bangumi.download_manga_info_poster();
                }
                //E站
                if (global.url.Contains("hentai"))
                {
                    Ehentai ehentai;
                    ehentai = new Ehentai(url: global.url, cookie: global.cookie, html: global.html, saveRoute: @"D:\tmp");
                    ehentai.download_manga_info_poster();
                    ehentai.set_info();
                    //ehental.download_manga_info_poster();
                }



                lkw.log("任务执行完毕");
                lkw.msbox("任务执行完毕");

            });
        }

        private void buttonNoCookie_Click(object sender, EventArgs e)
        {
            string url = textBoxUrl.Text;
            string html = richTextBoxHtml.Text;

            _ = lkw.NewWork(() =>
            {
                TopToon toptoon = new TopToon(url: url, html: html, saveRoute: comboBoxRoute.Text);
                toptoon.download_chapter_poster();
                toptoon.download_chapter_poster();
                toptoon.download_manga_info_poster();
                toptoon.set_info();

                lkw.log("任务执行完毕");
                lkw.msbox("任务执行完毕");

            });
        }

        private void saveRoute_Click(object sender, EventArgs e)
        {

        }

        private void labelHtmlCode_Click(object sender, EventArgs e)
        {

        }

        private void groupBoxBtn_Enter(object sender, EventArgs e)
        {

        }

        private void labelVersion_Click(object sender, EventArgs e)
        {

        }

        public bool is_img(string filePath)
        {
            string pattern = @"(.bmp|.jpg|.jpeg|.png|.tif|.gif|.pcx|.tga|.exif|.fpx|.svg|.psd|.cdr|.pcd|.dxf|.ufo|.eps|.ai|.raw|.WMF|.webp|.avif|.apng)$";

            return Regex.IsMatch(filePath, pattern, RegexOptions.IgnoreCase);
        }

        private void buttonAddRoute_Click(object sender, EventArgs e)
        {
            // VB的输入对话框 在点击取消时返回的空字符串
            string routerInput = Interaction.InputBox("请输入新路径:", "添加路径", "");
            // 输入空或点击了取消
            if (string.IsNullOrEmpty(routerInput))
            {
                return;
            }
            if (!Directory.Exists(routerInput))
            {
                MessageBox.Show("路径读取错误");
                return;
            }
            global.config.routers.Add(routerInput);
            config_update();
        }

        private void comboBoxRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 清空面板
            panelMangaList.Controls.Clear();

            string mediaPath = comboBoxRoute.Text;


            foreach (string mangaPath in Directory.GetFileSystemEntries(mediaPath))
            {
                // 目前只刮削章节漫画,因此只接受目录 剔除文件
                if (!Directory.Exists(mangaPath)) continue;
                if (mangaPath.Contains("smanga-info")) continue;
                if (mangaPath.Contains("smangaManager")) continue;

                string mangaName = Path.GetFileName(mangaPath);
                string mangaCover = "";
                string describe = "";
                Meta meta = null;
                bool scraped = false;
                List<string> tags = new List<string>();
                if (Directory.Exists(mangaPath + "-smanga-info"))
                {
                    string infoFile = Path.Combine(mangaPath + "-smanga-info", "meta.json");
                    if (!File.Exists(infoFile)) continue;

                    mangaCover = Path.Combine(mangaPath + "-smanga-info", "cover.jpg");
                    // 读取元数据 JSON 文件
                    string json = File.ReadAllText(infoFile);
                    meta = JsonConvert.DeserializeObject<Meta>(json);
                    describe = meta.describe;
                    tags = meta.tags;
                    if (meta.tags == null)
                    {
                        tags = new List<string>();
                    }
                    scraped = true;
                }
                MangaListItem mangaListItem = new MangaListItem(
                    this,
                    tags,
                    mangaName,
                    mangaPath,
                    mediaPath,
                    //imagePath: "A:\\02manga\\download\\2025\\直播主的流量密碼-smanga-info\\cover.webp",
                    imagePath: mangaCover,
                    describe,
                    scraped
                    );

                panelMangaList.Controls.Add(mangaListItem);
            }

        }

        class Meta
        {
            public string title { get; set; }
            public string author { get; set; }
            public string describe { get; set; }
            public List<string> tags { get; set; }
            public string publishDate { get; set; }

        }
    }
}
