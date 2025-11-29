using globalData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace website
{
    abstract class website
    {

        //漫画名称
        public string name;
        protected string poster;
        protected string route;
        public string infoRoute;
        public bool useMobile = false;
        //漫画主页链接
        protected string url;
        //漫画网站用户标识
        protected string cookie;
        //漫画主页html代码
        protected string html;
        //漫画网站名称
        protected string webSiteName;
        //漫画网站标识
        protected string webSiteMark;
        //漫画网站域名
        protected string webSiteDomain;
        //漫画下载路径
        public string downloadRoute;
        protected int numberSuffix = 1;
        //章节合集
        public List<ChapterItem> chapters = new List<ChapterItem>();

        protected bool downloadSwitch = true;
        //章节页合集
        protected List<string> chapterPages = new List<string>();
        //章节链接合集-下载用
        protected List<string> chapterInfos = new List<string>();

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="url">漫画首页的链接</param>
        protected void init(string saveRoute)
        {
            string route = saveRoute;
            this.route = route;
            //设置网站标识
            global.website = this.webSiteMark;
            //获取漫画主页数据
            if (this.html == "")
            {
                this.html = get_html_by_request(this.url, this.cookie);
            }
            //获取漫画名称
            this.name = this.get_manga_name(this.html);
            this.poster = this.get_manga_poster(this.html);
            //构建下载路径
            this.downloadRoute = Path.Combine(route, this.name);
            this.infoRoute = this.downloadRoute + "-smanga-info\\";

            if (!Directory.Exists(this.downloadRoute)) Directory.CreateDirectory(this.downloadRoute);
            if (!Directory.Exists(this.infoRoute)) Directory.CreateDirectory(this.infoRoute);

            //获取所有页码页面
            this.chapterPages = this.get_chapter_pages(this.html);
            //获取所有章节链接
            this.chapterPages.ForEach((string i) => { this.chapterInfos = chapterInfos.Union(this.get_chapter_info(i)).ToList(); });
            //获取章节图片
            this.chapterInfos.ForEach((string i) =>
            {
                this.chapters.Add(this.get_chapter_images(i));
            });
        }

        protected void load_base_data()
        {

        }

        public void download_chapter_poster()
        {
            //下载章节图片
            this.chapters.ForEach((ChapterItem i) => { this.download_chapter_images(i); });

            delete(this.downloadRoute + "\\temp");
        }

        /// <summary>
        /// 删除文件或目录
        /// </summary>
        /// <param name="fileName">文件路径</param>
        public void delete(string fileName)
        {
            // 如果是目录,调用文件夹方法
            if (Directory.Exists(fileName)) Directory.Delete(fileName, true);

            // 如果是文件,调用文件方法
            if (File.Exists(fileName)) File.Delete(fileName);

        }

        /// <summary>
        /// 获取页面内所有图片的链接
        /// </summary>
        /// <param name="html">当前漫画浏览页的url链接</param>
        /// <returns>当前页面所有图片的链接</returns>
        protected abstract ChapterItem get_chapter_images(string url);

        /// <summary>
        /// 获取所有章节的链接
        /// </summary>
        /// <param name="html">当前漫画目录页的html代码</param>
        /// <returns>当前页面所有章节的信息</returns>
        protected abstract List<string> get_chapter_info(string html);

        /// <summary>
        /// 获取章节分页
        /// 有些网站在漫画章节目录也会进行分页，使得其在章节首页只展出一部分内容
        /// 调用此方法获取全部的页面
        /// </summary>
        /// <param name="html">漫画首页的html代码</param>
        /// <returns>所有的页码的html代码</returns>
        protected abstract List<string> get_chapter_pages(string html);

        /// <summary>
        /// 获取漫画名称
        /// </summary>
        /// <param name="html">漫画主页的html数据</param>
        /// <returns>漫画名称</returns>
        protected abstract string get_manga_name(string html);

        /// <summary>
        /// 获取漫画封面
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        protected abstract string get_manga_poster(string html);
        /// <summary>
        /// 展示消息
        /// </summary>
        /// <param name="msg"></param>
        protected abstract void show_message(string msg);

        /// <summary>
        /// 校验当前章节的图片数量
        /// </summary>
        /// <param name="chapter">当前章节的实体类</param>
        /// <returns>返回当前章节下载的错误数量</returns>
        public int check_chapter_files(ChapterItem chapter, string route)
        {
            //错误数量
            int error = 0;
            //图片列表
            List<string> images = chapter.images;
            //图片数量
            int l = images.Count;
            //后缀名
            string suffix = chapter.suffix;

            for (int i = 0; i < l; i++)
            {
                string iamgeName = i == 0 ? "" : "-" + i.ToString();

                //生成文件名
                string fileName = route + iamgeName + suffix;
                //图片下载路径
                string fileUrl = images[i];
                //如果存在则跳出，不记录错误
                if (File.Exists(fileName)) continue;

                //输出错误信息（错误图片序号与链接）
                //lkw.WriteLine(route + "log.txt", "图片" + i + "下载错误 " + fileUrl);
                //错误数量+1
                error++;
                //再次尝试下载
                download_image_by_http(fileUrl, fileName);
            }

            //返回错误数量
            return error;
        }

        /// <summary>
        /// 下载当前章节的图片
        /// </summary>
        /// <param name="chapter">当前章节的实体类</param>
        /// <returns>返回当前章节的图片数量</returns>
        public int download_chapter_images(ChapterItem chapter)
        {
            //图片下载链接
            List<string> images = chapter.images;
            //下载路径
            string route = Path.Combine(this.downloadRoute, chapter.name);
            //后缀名
            string suffix = chapter.suffix;

            //执行下载
            for (int i = 0, l = images.Count; i < l; i++)
            {
                //输出提示信息
                this.show_message("正在下载章节\"" + chapter.name + "+\"" + "第" + i.ToString() + "张图片.");

                string iamgeName = i == 0 ? "" : "-" + i.ToString();

                string saveName = route + iamgeName + suffix;

                if (System.IO.File.Exists(saveName))
                {
                    continue;
                }

                try
                {
                    //下载图片
                    download_image_by_http(images[i], saveName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    continue;
                }

            }

            //校验图片数量
            //this.check_chapter_files(chapter, route);

            //返回图片数量
            return images.Count;
        }
        /// <summary>
        /// 
        /// </summary>
        protected void download_manga_poster(string route, string name, string poster)
        {
            //下载图片
            string suffix = this.get_image_suffix(poster);
            download_image_by_http(poster, route + name + suffix);
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
                if (useMobile)
                {
                    wReq.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 16_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/16.6 Mobile/15E148 Safari/604.1";
                }
                
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
        /// 根据链接获取页面html代码-使用浏览器
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <returns>html代码</returns>
        protected static string get_html_by_browser(string url, string cookie = "", string referer = "")
        {
            throw new NotImplementedException();
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

            if (System.IO.File.Exists(tempFile))
            {
                System.IO.File.Delete(tempFile);    //存在则删除
            }

            try
            {
                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                // 添加请求头
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                request.Accept = "image/webp,image/apng,image/*,*/*;q=0.8";
                request.Headers.Add("Accept-Language", "en-US,en;q=0.9");
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
        /// 获取图片后缀名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected string get_image_suffix(string fileName)
        {
            Match match = Regex.Match(fileName, "\\.(jpg|jpeg|png|bmp|gif|webp)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return ".jpg";
            }
        }

        /// <summary>
        /// 格式化文件名-章节名
        /// 去除换行符等特殊字符,保证创建文件夹的安全
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected string format_file_name(string str)
        {
            //替换关键字
            string[] key = { "\n", "\r", "\t", "<", ">", "\\", "/", "|", ":", "*", "?", " " };

            //循环替换
            for (int i = 0, l = key.Length; i < l; i++)
            {
                str = str.Replace(key[i], "");
            }

            //返回格式化后的结果
            return str;
        }


    }
    /// <summary>
    /// 章节类
    /// name 章节名称
    /// url 章节链接
    /// imageList 章节中的图片列表 记录图片的下载链接
    /// </summary>
    public class ChapterItem
    {
        public string name;
        public string subName;
        public string pubDate;
        public string suffix;
        public List<string> images;

        public ChapterItem(string name, string subName,string pubDate, string suffix, List<string> images)
        {
            this.name = name;
            this.subName = subName;
            this.pubDate = pubDate;
            this.suffix = suffix;
            this.images = images;
        }
    }

    public class MangaInfo
    {
        public string title;
        public string author;
        public string star;
        public string describe;
        public List<string> tags;
        public string publishDate;
        public List<Character> character;
        public List<ChapterItem> chapters;
        public string subName;

        public MangaInfo(string title, string author, string star, string describe, List<string> tags, string publishDate, string subName = "")
        {
            this.title = title;
            this.author = author;
            this.star = star;
            this.describe = describe;
            this.tags = tags;
            this.publishDate = publishDate;
            this.subName = subName;
        }
    }
    public class Character
    {
        public string name;
        public string description;

        public Character(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
    }
}
