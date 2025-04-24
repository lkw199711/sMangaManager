using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace website
{
    class Toomics : website
    {

        //Lkw lkw = new Lkw();

        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formIndex"></param>
        public Toomics(string saveRoute, string url, string cookie = "", bool downloadSwitch = true, string html = "")
        {
            //获取链接
            this.url = url;
            //设置漫画网站名称
            this.webSiteName = "玩漫";
            //设置漫画网站标识
            this.webSiteMark = "toomics";
            //设置漫画网站域名
            this.webSiteDomain = "toomics.com";
            //设置使用的cookie
            this.cookie = cookie;
            this.html = html;
            this.useMobile = true;
            this.downloadSwitch = downloadSwitch;
            //执行初始化方法
            this.init(saveRoute);
        }

        protected override ChapterItem get_chapter_images(string info)
        {
            List<string> list = new List<string>();

            //章节名
            //string name = new Regex("(?<=span\\sclass=\"num\">)[^<]+").Match(info).Value;
            string name = new Regex("(?<=small>)[^<]+").Match(info).Value;
            string subName = new Regex("(?<=strong.+?>)[^<]+").Match(info).Value; 
            string pubDate = new Regex("(?<=text-muted\">)[^<]+").Match(info).Value;
            name = this.format_file_name(name);
            subName = this.format_file_name(subName);

            //获取章节名称
            string chapterName;
            if (name == null)
            {
                chapterName = subName;
            }
            else
            {
                chapterName = name + " " + subName;
            }

            // 如有重名则后缀加1
            this.chapters.ForEach((ChapterItem item) =>
            {
                string existName = item.subName;
                if (chapterName == existName)
                {
                    chapterName = chapterName + this.numberSuffix++.ToString();
                }
            });

            //获取所有图片链接
            Match src = new Regex("(?<=data-original=\").+?[.png|.jpg]*(?=\")", RegexOptions.Singleline).Match(info);

            //获取图片的扩展名
            string suffix = this.get_image_suffix(src.Value);
            list.Add(src.Value);

            /*
             生成章节实体类 (章节名,链接,图片扩展名,图片链接list)*/
            return new ChapterItem(chapterName, chapterName, pubDate, suffix, list);
        }

        /// <summary>
        /// 获取目录的所有页码
        /// 禁止天堂仅有一页目录
        /// 直接返回包含html 的list
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        protected override List<string> get_chapter_pages(string html)
        {
            List<string> list = new List<string>();

            //仅有一页 直接返回包含html list
            list.Add(html);

            //返回list
            return list;
        }

        /// <summary>
        /// 获取所有章节的链接
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        protected override List<string> get_chapter_info(string html)
        {
            List<string> list = new List<string>();

            //截取包含章节列表的部分
            string chapterBox = html;

            //获取所有章节内容
            MatchCollection chapterList = new Regex("(?<=normal_ep).+?(?=</li>)", RegexOptions.Singleline).Matches(chapterBox);

            for (int i = 0, l = chapterList.Count; i < l; i++)
            {
                //章节内容
                string str = chapterList[i].Value;

                list.Add(str);
            }

            return list;
        }

        /// <summary>
        /// 获取漫画名称
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        protected override string get_manga_name(string html)
        {
            //筛选漫画名的正则
            Regex getName = new Regex("(?<=<h2.+?>)[^<]+");

            //获取漫画名
            string mangaName = getName.Match(html).Value;

            //替换掉 换行符 等不能命名为文件夹的特殊字符
            mangaName = this.format_file_name(mangaName);

            return mangaName;
        }

        protected override string get_manga_poster(string html)
        {
            //筛选漫画封面的正则
            Regex getName = new Regex("(?<=<!--\\smobile\\s-->.+?src=\").+?(?=\")", RegexOptions.Singleline);

            //获取漫画封面
            string mangaPoster = getName.Match(html).Value;
            
            return mangaPoster;

            //玩漫页面中没有封面 需要手动获取
        }

        /// <summary>
        /// 输出消息信息
        /// </summary>
        /// <param name="msg">消息内容</param>
        protected override void show_message(string msg)
        {
            //FormIndex.set_label_text(this.form.labelMessage, msg);
        }

        public ToomicesMangaInfo get_manga_info()
        {
            List<string> tagList = new List<string>();
            string name = this.name;
            name = format_file_name(name);
            string author = new Regex("(?<=mb-0\\stext-xs\\sfont-normal\\stext-gray-300\">)[^<]+", RegexOptions.Singleline).Match(html).Value;
            author = format_file_name(author);

            string describe = new Regex("(?<=name=\"description\"\\scontent=\")[^\"]+", RegexOptions.Singleline).Match(html).Value;
            string publishDate = chapters[0].pubDate;
            return new ToomicesMangaInfo(name, author, describe, publishDate);
        }

        /// <summary>
        /// 下载漫画banner图
        /// </summary>
        /// <param name="html"></param>
        public void download_manga_info_poster()
        {
            Match pc = new Regex("(?<=<!--\\spc\\s-->.+?srcset=\")[^\"]+",RegexOptions.Singleline).Match(html);
            Match mobile = new Regex("(?<=<!--\\smobile\\s-->.+?src=\")[^\"]+", RegexOptions.Singleline).Match(html);
            Match pcbg = new Regex("(?<=<!--\\spc\\sbg\\s-->.+?src=\")[^\"]+", RegexOptions.Singleline).Match(html);

            download_image_by_http(pc.Value, infoRoute + "\\banner" + get_image_suffix(pc.Value));
            download_image_by_http(mobile.Value, infoRoute + "\\cover0" + get_image_suffix(mobile.Value));
            download_image_by_http(pcbg.Value, infoRoute + "\\banner-background" + get_image_suffix(pcbg.Value));

            delete(infoRoute + "\\temp");
        }

        public void set_info()
        {
            // 下载角色信息
            ToomicesMangaInfo info = get_manga_info();

            if (!File.Exists(this.infoRoute))
            {
                Directory.CreateDirectory(this.infoRoute);
            }


            string json = JsonConvert.SerializeObject(info, Formatting.Indented);
            File.WriteAllText(this.infoRoute + "meta.json", json);
            File.WriteAllText(this.infoRoute + "info.html", html);
        }

        public class ToomicesMangaInfo
        {
            public string title;
            public string author;
            public string describe;
            public string publishDate;
            public ToomicesMangaInfo(string title, string author, string describe, string publishDate)
            {
                this.title = title;
                this.author = author;
                this.describe = describe;
                this.publishDate = publishDate;
            }
        }
    }
}
