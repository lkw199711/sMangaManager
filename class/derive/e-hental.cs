using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace website
{
    class Ehentai : website
    {
        public string scrapingRouter;
        public Ehentai(string saveRoute, string url, string cookie = "", bool downloadSwitch = true, string html = "")
        {
            //获取链接
            this.url = url;
            //设置漫画网站名称
            this.webSiteName = "E站";
            //设置漫画网站标识
            this.webSiteMark = "e-hental";
            //设置漫画网站域名
            this.webSiteDomain = "e-hental.org";
            //设置使用的cookie
            this.cookie = cookie;
            this.html = html;

            this.downloadSwitch = downloadSwitch;
            //执行初始化方法
            this.init(saveRoute);
        }

        protected override ChapterItem get_chapter_images(string url)
        {
            return new ChapterItem("", "", "", "", new List<string>());
        }

        /// <summary>
        /// 不适用
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        protected override List<string> get_chapter_info(string html)
        {
            return new List<string>();
        }

        protected override List<string> get_chapter_pages(string html)
        {
            List<string> list = new List<string>();

            //仅有一页 直接返回包含html list
            list.Add(html);

            //返回list
            return list;
        }

        protected override string get_manga_name(string html)
        {
            //筛选漫画名的正则
            Regex getName = new Regex("(?<=<h1\\sid=\"gn\">)[^<]+");

            //获取漫画名
            string mangaName = getName.Match(html).Value;

            //替换掉 换行符 等不能命名为文件夹的特殊字符
            mangaName = this.format_file_name(mangaName);

            return mangaName;
        }

        protected override string get_manga_poster(string html)
        {
            //筛选漫画封面的正则
            Regex getName = new Regex(@"(?<=url\().+?(?=\))");

            //获取漫画封面
            string mangaPoster = getName.Match(html).Value;

            return mangaPoster;
        }

        protected override void show_message(string msg)
        {
            throw new System.NotImplementedException();
        }

        public void download_manga_info_poster()
        {
            //下载漫画封面
            download_image_by_http(this.poster, this.infoRoute + "cover" + this.get_image_suffix(this.poster));

            delete(this.infoRoute + "\\temp");
        }

        public void set_info()
        {
            // 下载角色信息
            EhentaiMangaInfo info = get_manga_info();

            if (!File.Exists(this.infoRoute))
            {
                Directory.CreateDirectory(this.infoRoute);
            }

            string json = JsonConvert.SerializeObject(info, Formatting.Indented);
            File.WriteAllText(this.infoRoute + "meta.json", json);
            File.WriteAllText(this.infoRoute + "info.html", html);
        }

        public EhentaiMangaInfo get_manga_info()
        {
            EhentaiMangaInfo ehentaiMangaInfo = new EhentaiMangaInfo();
            List<string> list = new List<string>();
            string infoBox = new Regex("(?<=id=\"gdd\">).+?(?=id=\"gdr\")", RegexOptions.Singleline).Match(html).Value;
            MatchCollection infos = new Regex("(?=<tr>).+?(?=</tr>)", RegexOptions.Singleline).Matches(infoBox);
            foreach (Match match in infos)
            {
                if (!match.Success) continue;
                string key = new Regex("(?<=class=\"gdt1\">)[^<]+", RegexOptions.Singleline).Match(match.Value).Value;

                string value = new Regex("(?<=class=\"gdt2\">)[^<]+").Match(match.Value).Value;
                if (key == "Posted:") ehentaiMangaInfo.publishDate = value;
                if (key == "Parent:") ehentaiMangaInfo.parent = value;
                if (key == "Language:") ehentaiMangaInfo.language = value;
                if (key == "File Size:") ehentaiMangaInfo.fileSize = value;
                if (key == "Length:") ehentaiMangaInfo.imagesLength = value;

            }

            string tagsBox = new Regex("(?<=id=\"taglist\">).+?(?=tagmenu_act)", RegexOptions.Singleline).Match(html).Value;
            MatchCollection tags = new Regex("(?<=<a[^>]+>)[^<]+", RegexOptions.Singleline).Matches(tagsBox);
            foreach (Match tag in tags)
            {
                if (!tag.Success) continue;
                list.Add(tag.Value);
            }
            ehentaiMangaInfo.title = this.name;
            ehentaiMangaInfo.tags = list;
            return ehentaiMangaInfo;
        }

        public class EhentaiMangaInfo
        {
            public string title;
            public string publishDate;
            public string parent;
            public string language;
            public string fileSize;
            public string imagesLength;
            public List<string> tags;
        }
    }
}
