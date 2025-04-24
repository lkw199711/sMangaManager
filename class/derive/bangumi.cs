using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace website
{
    class Bangumi : website
    {
        public string subjectId;
        public string saveRoute;
        public string scrapingRouter;

        public BangumiMeta meta;
        public List<Character> characters = new List<Character>();
        public Bangumi(string saveRoute, string url, string cookie = "", bool downloadSwitch = true, string html = "")
        {
            //获取链接
            this.url = url;
            //设置漫画网站名称
            this.webSiteName = "Bangumi";
            //设置漫画网站标识
            this.webSiteMark = "Bangumi";
            //设置漫画网站域名
            this.webSiteDomain = "bangumi.tv";
            //设置使用的cookie
            this.cookie = cookie;
            this.html = html;

            this.downloadSwitch = downloadSwitch;
            this.saveRoute = saveRoute;

            string pattern = @"\d+";
            Match match = Regex.Match(url, pattern);
            this.subjectId = match.Value;

            this.scrapingRouter = Path.Combine(saveRoute, this.name);
            if (!Directory.Exists(scrapingRouter)) Directory.CreateDirectory(scrapingRouter);
        }

        public async Task get_subject()
        {
            string subjectUrl = $"https://api.bgm.tv/v0/subjects/{this.subjectId}";
            using (HttpClient client = new HttpClient())
            {
                // 添加自定义的请求头
                client.DefaultRequestHeaders.Add("Accept-Token", "EyVxLWZgPfDKYFdDLxkAzR4ibRviYx472Knk5LeM");
                client.DefaultRequestHeaders.Add("User-Agent", "smanga");
                try
                {
                    HttpResponseMessage response = await client.GetAsync(subjectUrl);
                    response.EnsureSuccessStatusCode(); // 确保请求成功

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                    var tagList = new List<string>();
                    for (int i = 0, l = json.tags.Count; i < l; i++)
                    {
                        tagList.Add(json.tags[i].name.ToString());
                    }



                    string painting = "";
                    string author = "";
                    string publisher = "";

                    for (int i = 0; i < json.infobox.Count; i++)
                    {
                        var info = json.infobox[i];
                        if (info.key == "作画") painting = info.value.ToString();
                        if (info.key == "原作") author = info.value.ToString();
                        if (info.key == "出版社") publisher = info.value.ToString();
                    }

                    this.poster = json.images.common.ToString();
                    this.name = json.name_cn.ToString();


                    //构建下载路径
                    this.downloadRoute = Path.Combine(this.saveRoute, this.name);
                    this.infoRoute = this.downloadRoute + "-smanga-info\\";

                    this.meta = new BangumiMeta();

                    this.meta.title = json.name_cn.ToString();
                    this.meta.subTitle = json.name.ToString();
                    this.meta.author = $"{author}&{painting}";
                    this.meta.publisher = publisher.ToString();
                    this.meta.describe = json.summary.ToString();
                    this.meta.tags = tagList;
                    this.meta.publishDate = json.date.ToString();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("请求失败: " + e.Message);
                }
            }
        }

        public async Task get_characters()
        {
            string subjectUrl = $"https://api.bgm.tv/v0/subjects/{this.subjectId}/characters";
            List<Character> list = new List<Character>();
            using (HttpClient client = new HttpClient())
            {
                // 添加自定义的请求头
                client.DefaultRequestHeaders.Add("Accept-Token", "EyVxLWZgPfDKYFdDLxkAzR4ibRviYx472Knk5LeM");
                client.DefaultRequestHeaders.Add("User-Agent", "smanga");
                try
                {
                    HttpResponseMessage response = await client.GetAsync(subjectUrl);
                    response.EnsureSuccessStatusCode(); // 确保请求成功

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(jsonResponse);

                    var json = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                    for (int i = 0, l = json.Count; i < l; i++)
                    {
                        var character = json[i];
                        string name = character.name.ToString();
                        string description = character.relation.ToString();
                        string image = character.images.medium.ToString();
                        list.Add(new Character(name, description, image));
                    }

                    this.characters = list;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("请求失败: " + e.Message);
                }
            }
        }
        protected override ChapterItem get_chapter_images(string info)
        {
            List<string> list = new List<string>();


            /*
             生成章节实体类 (章节名,链接,图片扩展名,图片链接list)*/
            return new ChapterItem("", "", "", "", list);
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

            return list;
        }

        /// <summary>
        /// 输出消息信息
        /// </summary>
        /// <param name="msg">消息内容</param>
        protected override void show_message(string msg)
        {
            //FormIndex.set_label_text(this.form.labelMessage, msg);
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

        public async void download_manga_info_poster()
        {
            //执行初始化方法
            await this.get_subject();
            await this.get_characters();

            this.set_info();

            //下载漫画封面
            download_image_by_http(this.poster, this.infoRoute + "cover" + this.get_image_suffix(this.poster));

            //下载角色图片
            this.characters.ForEach(chara =>
            {
                string file = this.infoRoute + chara.name + ".jpg";
                string url = chara.image;
                download_image_by_http(url, file);
            });

            delete(this.infoRoute + "\\temp");
        }

        public void set_info()
        {
            //info.character = download_character(this.html);

            if (!File.Exists(this.infoRoute))
            {
                Directory.CreateDirectory(this.infoRoute);
            }


            string json = JsonConvert.SerializeObject(this.meta, Formatting.Indented);
            File.WriteAllText(this.infoRoute + "meta.json", json);
            File.WriteAllText(this.infoRoute + "info.html", html);
        }


        public class BangumiMeta
        {
            public string title;
            public string author;
            public string describe;
            public List<string> tags;
            public string publishDate;
            public List<Character> character;
            public string subTitle;
            public string publisher;
        }
        public class Character
        {
            public string name;
            public string description;
            public string image;

            public Character(string name, string description, string image)
            {
                this.name = name;
                this.description = description;
                this.image = image;
            }
        }
    }
    internal class bangumi
    {
    }
}
