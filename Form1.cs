using lkw;
using manga_reptile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using website;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sMangaManager
{
    public partial class Form1 : Form
    {
        Lkw lkw = new Lkw();
        List<website.ChapterItem> chapters;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxReNameFolder.Text = "D:\\8other\\01manga\\顶通\\download\\test";

            // 重命名功能提示
            labelReNameTip.Text += "程序获取到的文件顺序是错位的,目前本程序无补位功能.请提前补位并保证文件顺序正常,如您无法理解,请勿使用重命名功能.";

            // 下载路径
            textBoxSaveRoute.Text = global.downloadRoute;
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
            string route = get_save_route() + "single-page\\";

            string url = textBoxUrl.Text; string html = richTextBoxHtml.Text;

            TopToon toptoon = new TopToon(url: url, cookie: global.cookie, downloadSwitch: false, html: html, saveRoute: get_save_route());
            chapters = toptoon.chapters;

            List<string> list = new List<string>();
            string dir = textBoxReNameFolder.Text;

            lkw.NewWork(() =>
            {
                // 非文件夹
                if (!Directory.Exists(dir)) return;

                foreach (string fileName in Directory.GetFileSystemEntries(dir))
                {

                    list.Add(fileName);
                    lkw.log(fileName);
                }


                chapters.ForEach((website.ChapterItem item) =>
                {
                    lkw.log(item.subName);
                });
            });
        }

        /// <summary>
        /// 重命名按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReName_Click(object sender, EventArgs e)
        {
            string url = textBoxUrl.Text;

            string html = richTextBoxHtml.Text;

            TopToon toptoon = new TopToon(url: url, cookie: global.cookie, downloadSwitch: false, html: html, saveRoute: get_save_route());

            string dir = textBoxReNameFolder.Text;

            List<string> list = new List<string>();
            chapters = toptoon.chapters;

            lkw.NewWork(() =>
            {
                // 非文件夹
                if (!Directory.Exists(dir)) return;

                foreach (string fileName in Directory.GetFileSystemEntries(dir))
                {

                    list.Add(fileName);



                    // 替换文件名
                    //replace(fileName, oldStr, newStr);
                }


                if (list.Count != chapters.Count)
                {
                    lkw.msbox("章节数量不一致,不能重命名");
                    Console.WriteLine(list.Count.ToString());
                    Console.WriteLine(chapters.Count.ToString());

                }


                for (int i = 0; i < chapters.Count; i++)
                {
                    //获取文件扩展名
                    string suffix = Path.GetExtension(list[i]);

                    string newName = dir + "\\" + chapters[i].name + suffix;

                    try //路径含有非法字符 跳过
                    {
                        File.Move(list[i], newName);
                    }catch {
                        lkw.log(newName+ "路径含有非法字符 跳过 请手动重命名");
                        continue;
                    }
                    
                }
            });
            
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
                TopToon toptoon = new TopToon(url: url, cookie: cookie, html: html, saveRoute:get_save_route());
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
                    lkw.log("https:" + imageList[i].Value);
                    download_image_by_http("https:" + imageList[i].Value, get_save_route() + "single-page\\" + i.ToString().PadLeft(5, '0') + ".jpg");

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
                TopToon toptoon = new TopToon(url: url, cookie: global.cookie, html: html, saveRoute: get_save_route());
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
                TopToon toptoon = new TopToon(url: url, cookie: global.cookie,html: html, saveRoute: get_save_route());
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

            string cookie = "language=tw; viewModeSaving=1; userKeyToken=15d27a24e1a22987d24aee4c99c1917e_1689925853; service_key=e7cdac5be1e5cd7b6041b87bfb616665; _gcl_au=1.1.2074426609.1689925857; __lt__cid=75a9432a-e104-4e09-8a16-dbab6d5b9064; _ga=GA1.1.1319261843.1689925859; adultStatus=1; notOpenConfirmAdult=1; saveId=lkw199711%40163.com; freeTimeGiveInfo=%7B%22LD3%22%3A1689926119%7D; isLoginUsed=1; net_session=elFmR3g5UWRsYm1ZZlIrOHlNNk4wRWdHTTd5b1dDeHA0MDRVblNXK0VmV1N1RjFhWXJOZStqUlIxcnU5bTA5VUZkVDgvS1lwUlNFdldKM3JBTkFvZlNWUEkzQkU2cUxPbndmcVFuUFB2QmV5bGJXRnlMOHlpcjNtOUMyWDZaZWRCOWVvbmZOdXMzUkpDQ3V5MERnbHhvYmhORmEyVmVJZ0F3NEtGbVN4SkMzbFRLVWU0Q2d1VVB2aEs3bXprNy9MYy9sRE42VU9Wd2FJRzR6UHExZUozUnR5WDhVeCtETlJ1U1NvMG53OFZneEVwSmNuRUxydlQ2K3FoU09XbTNtdTZ3T1gvYTRYUnFHdTBMTXprMGUxbW9GWFk5RlVEaUtNRFhPNDRTTlFEc2g4VmoraGJlazBtV3JwM3BEZEFsREFHdFN4VXlhMGtzcnZOR284cW4rdVJnbGI0b1Fabi9kL2V1Rk12N3JmNGUxNTlmRHpEREQ0QVhwcm9vdVlrVzFCMktwTXV3S0t5SlRtYzBqdlpCZGpGZzZJclFvcmQ0SnlqLzBJVjhEa0pZbEJYNzlYQkxnYk9PUWhSZldzMXpiM3p5aHFSMzRjamVCOHFEazliYWtsQ3g5N3V4L0wvVEhYS3VQclJ5eFViQVpaZkFJZGlLOTl1SW1CeTA4alBBamI3Tmw4VjhOZzZteWtqa0JROWgwdUdWdnc5VGVGakxlWWNWOHhKMFdaYVZQYjJVR05PZG5MQ1B6eTZaV2NZeHlGf0118085649aad90f5226e57852d563f581163bc; userGender=1; notOpenGiftNotice=1; existsBannerTop=1; userKey=6BC96BA0A3A0CD6122EC2C2319EEBB921D8A4763360662553ECACCED9DF9C108; comicEpListSort=asc; userRecentTop10ListToLS=1; viewNonstop=1; notOpenPhoneCertNotice=1; notOpenAttendancePopup=1; hotTimeGiveInfo=%7B%22hotTimeSet%22%3A1689933579%7D; notOpenPopupNotice=1; __lt__sid=c93d9a72-7fc17385; epListAccessPath=%2Fsearch; spush_nowUrl=https%253A%252F%252Fwww.toptoon.net%252Fcomic%252FepList%252F80643; _ga_CZ8J0XSEDJ=GS1.1.1689933471.3.1.1689933617.31.0.0";

            // 存储路径到全局
            global.downloadRoute = textBoxSaveRoute.Text;

            _ = lkw.NewWork(() =>
            {
                TopToon toptoon = new TopToon(url: url, cookie: cookie,html: html, saveRoute: get_save_route());
                toptoon.download_chapter_poster();
                toptoon.download_chapter_poster();
                toptoon.download_manga_info_poster();
                toptoon.set_info();

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
                TopToon toptoon = new TopToon(url: url, html: html, saveRoute:get_save_route());
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
        
        /// <summary>
        /// 获取存储路径
        /// </summary>
        /// <returns></returns>
        public string get_save_route()
        {
            string route = textBoxSaveRoute.Text;

            return route;
        }
    }
}
