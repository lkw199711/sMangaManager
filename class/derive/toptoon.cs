using manga_reptile;
using System;
using System.Collections.Generic;
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
        public TopToon(string url, string cookie = "", bool downloadSwitch = true, string html = "")
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
            this.init();
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
            string chapterName = name + " " + subName;

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
    }
}
