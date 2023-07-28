using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manga_reptile
{
    interface IAnalysis
    {
        /// <summary>
        /// 获取章节分页
        /// 有些网站在漫画章节目录也会进行分页，使得其在章节首页只展出一部分内容
        /// 调用此方法获取全部的页面
        /// </summary>
        /// <param name="html">漫画首页的html代码</param>
        /// <returns>所有的页码的html代码</returns>
        List<string> get_chapter_pages(string html);

        /// <summary>
        /// 获取漫画名称
        /// </summary>
        /// <param name="html">漫画主页的html数据</param>
        /// <returns>漫画名称</returns>
        string get_manga_name(string html);

        /// <summary>
        /// 获取所有章节的链接
        /// </summary>
        /// <param name="html">当前漫画目录页的html代码</param>
        /// <returns>当前页面所有章节的链接</returns>
        List<string> get_chapter_list(string html);

        /// <summary>
        /// 获取页面内所有图片的链接
        /// </summary>
        /// <param name="html">当前漫画浏览页的html代码</param>
        /// <returns>当前页面所有图片的链接</returns>
        List<string> get_chapter_images(string html);

        /// <summary>
        /// 下载当前章节的图片
        /// </summary>
        /// <param name="chapter">当前章节的实体类</param>
        /// <returns>返回当前章节的图片数量</returns>
        int download_chapter_images(ChapterItem chapter);

        /// <summary>
        /// 校验当前章节的图片数量
        /// </summary>
        /// <param name="chapter">当前章节的实体类</param>
        /// <returns>返回当前章节下载的错误数量</returns>
        int check_chapter_files(ChapterItem chapter);

        /// <summary>
        /// 根据链接获取页面html代码-使用请求
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <returns>html代码</returns>
        string get_html_by_request(string url, string cookie = "", string referer = "");

        /// <summary>
        /// 根据链接获取页面html代码-使用浏览器
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <returns>html代码</returns>
        string get_html_by_browser(string url, string cookie = "", string referer = "");
    }
}
