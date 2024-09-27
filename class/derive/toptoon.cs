using manga_reptile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace website
{
    class TopToon : website
    {

        //Lkw lkw = new Lkw();

        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formIndex"></param>
        public TopToon(string saveRoute, string url, string cookie = "", bool downloadSwitch = true, string html = "")
        {
            //获取链接
            this.url = url;
            //设置漫画网站名称
            this.webSiteName = "顶通";
            //设置漫画网站标识
            this.webSiteMark = "toptoon";
            //设置漫画网站域名
            this.webSiteDomain = "toptoon.net";
            //设置使用的cookie
            this.cookie = cookie;
            this.html = html;

            this.downloadSwitch = downloadSwitch;
            //执行初始化方法
            this.init(saveRoute);
        }

        protected override ChapterItem get_chapter_images(string info)
        {
            List<string> list = new List<string>();

            //章节名
            string name = new Regex("(?<=title[^>]+>)[^<]+").Match(info).Value;

            string subName = new Regex("(?<=subTitle[^>]+>)[^<]+").Match(info).Value;

            string pubDate = new Regex("(?<=pubDate[^>]+>)[^<]+").Match(info).Value;

            name = this.format_file_name(name);
            subName = this.format_file_name(subName);


            //获取章节名称
            string chapterName;
            if (name == null) {
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

            Console.WriteLine(chapterName);
            

            //截取图片链接部分
            string imageBox = info;

            //获取所有图片链接
            MatchCollection src = new Regex("(?<=ata-src=\"//).+?[.png|.jpg]*(?=\")", RegexOptions.Singleline).Matches(imageBox);

            //获取图片的扩展名
            string suffix = this.get_image_suffix(src[0].Value);

            foreach (Match m in src)
            {
                string img = "https://" + m.Value;
                Console.WriteLine(img);
                list.Add(img);
            }

            /*
             生成章节实体类 (章节名,链接,图片扩展名,图片链接list)*/
            return new ChapterItem(chapterName, chapterName, suffix, list);
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
            MatchCollection chapterList = new Regex("(?<=data-order).+?(?=badge2)", RegexOptions.Singleline).Matches(chapterBox);

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
            Regex getName = new Regex("(?<=</i><span\\sclass=\"txt\">)[^<]+");

            //获取漫画名
            string mangaName = getName.Match(html).Value;

            //替换掉 换行符 等不能命名为文件夹的特殊字符
            mangaName = this.format_file_name(mangaName);

            return mangaName;
        }

        protected override string get_manga_poster(string html)
        {
            //筛选漫画封面的正则
            Regex getName = new Regex("(?<=: \"//).+?(?=\")");

            //获取漫画封面
            string mangaPoster = "https://" + getName.Match(html).Value;

            return mangaPoster;
        }

        /// <summary>
        /// 输出消息信息
        /// </summary>
        /// <param name="msg">消息内容</param>
        protected override void show_message(string msg)
        {
            //FormIndex.set_label_text(this.form.labelMessage, msg);
        }

        public MangaInfo get_manga_info(string html)
        {
            List<string> tagList = new List<string>();
            string infoBox = new Regex("(?<=class=\"arTit\">作品介紹).+?(?=section)", RegexOptions.Singleline).Match(html).Value;
            string name = new Regex("(?<=class=\"title\">)[^<]+", RegexOptions.Singleline).Match(infoBox).Value;
            name = format_file_name(name);
            string author = new Regex("(?<=作家\\s:\\s)[^<]+", RegexOptions.Singleline).Match(infoBox).Value;
            author = format_file_name(author);
            string star = new Regex("(?<=iconStar\"></span>)[^<]+", RegexOptions.Singleline).Match(infoBox).Value;
            star = format_file_name(star);
            string desc = new Regex("(?<=class=\"desc\">)[^<]+", RegexOptions.Singleline).Match(infoBox).Value;
            string tagBox = new Regex("(?<=hashTag\">).+?(?=<div\\sclass=\"infoBottomArea)", RegexOptions.Singleline).Match(infoBox).Value;
            MatchCollection tagMatchs = new Regex("(?=#)[^<]+", RegexOptions.Singleline).Matches(tagBox);

            string publishDate = new Regex("(?<=class=\'pubDate\'>)[^<]+", RegexOptions.Singleline).Match(html).Value;

            for (int i = 0, l = tagMatchs.Count; i < l; i++)
            {
                tagList.Add(tagMatchs[i].Value.Replace("#", ""));
            }

            return new MangaInfo(name, author, star, desc, tagList, publishDate);
        }

        /// <summary>
        /// 下载漫画banner图
        /// </summary>
        /// <param name="html"></param>
        public void download_banner(string html)
        {
            string bannerBox = new Regex("(?<=class=\"comicThumb_wp\">).+?(?=comicInfo)", RegexOptions.Singleline).Match(html).Value;
            MatchCollection imageList = new Regex("(?<=<img\\ssrc=\')[^\']+", RegexOptions.Singleline).Matches(bannerBox);

            for (int i = 0, l = imageList.Count; i < l; i++)
            {
                string image = imageList[i].Value;
                string saveName = this.route + this.name + "-smanga-info\\banner" + i.ToString().PadLeft(2, '0') + this.get_image_suffix(image);

                if (System.IO.File.Exists(saveName))
                {
                    continue;
                }

                download_image_by_http("https:" + imageList[i].Value, saveName);

            }
        }

        public void download_thumbnail(string html)
        {
            string thumbnailBox = new Regex("(?<=infoContent\"\\sdata-type=\"scenesteal\">).+?(?=section)", RegexOptions.Singleline).Match(html).Value;
            MatchCollection imageList = new Regex("(?<=<img\\ssrc=\')[^\']+", RegexOptions.Singleline).Matches(thumbnailBox);

            for (int i = 0, l = imageList.Count; i < l; i++)
            {
                string image = imageList[i].Value;
                string saveName = this.route + this.name + "-smanga-info\\thumbnail" + i.ToString().PadLeft(2, '0') + this.get_image_suffix(image);

                if (System.IO.File.Exists(saveName))
                {
                    continue;
                }

                download_image_by_http("https:" + imageList[i].Value, saveName);

            }
        }

        public List<Character> download_character(string html)
        {
            List<Character> list = new List<Character>();

            string characterBox = new Regex("(?<=data-type=\"character\").+?(?=infoContent)", RegexOptions.Singleline).Match(html).Value;
            MatchCollection characterList = new Regex("(?<=characterName\'>)[^<]+", RegexOptions.Singleline).Matches(characterBox);
            MatchCollection characterDescriptionList = new Regex("(?<=characterDescription\'>)[^<]+", RegexOptions.Singleline).Matches(characterBox);
            MatchCollection imageList = new Regex("(?<=src=\')[^\']+", RegexOptions.Singleline).Matches(characterBox);

            for (int i = 0, l = characterList.Count; i < l; i++)
            {
                string name = characterList[i].Value;
                string description = characterDescriptionList[i].Value;
                string image = imageList[i].Value;

                name = this.format_file_name(name);

                list.Add(new Character(name, description));

                string saveName = this.route + this.name + "-smanga-info\\" + name + this.get_image_suffix(image);

                if (System.IO.File.Exists(saveName))
                {
                    continue;
                }

                download_image_by_http("https:" + image, saveName);

            }

            return list;
        }

        public void download_manga_info_poster()
        {
            string smangaPath = this.route + this.name + "-smanga-info";

            this.download_banner(this.html);
            this.download_thumbnail(this.html);
            this.download_character(this.html);
            //下载漫画封面
            download_image_by_http(this.poster, this.infoRoute + "cover"+ this.get_image_suffix(this.poster));

            delete(smangaPath + "\\temp");
        }

        public void set_info()
        {
            // 下载角色信息
            MangaInfo info = get_manga_info(this.html);

            info.character = download_character(this.html);

            if (!File.Exists(this.infoRoute))
            {
                Directory.CreateDirectory(this.infoRoute);
            }


            string json = JsonConvert.SerializeObject(info, Formatting.Indented);
            File.WriteAllText(this.infoRoute + "info.json", json);
            File.WriteAllText(this.infoRoute + "info.html", html);
        }
    }

    class TopToonParams
    {
        public TopToonParams() { }
    }
}
