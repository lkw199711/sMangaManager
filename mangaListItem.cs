using globalData;
using lkw;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using website;


namespace sMangaManager
{
    public partial class MangaListItem : UserControl
    {
        public Form form = null;
        Lkw lkw = new Lkw();
        public string mediaPath;
        TopToon toptoon = null;
        Bangumi bangumi = null;
        Ehentai ehentai = null;
        public string mangaPath = "";
        public string mangaName;
        public MangaListItem(Form form, List<string> tags, string mangaName = "", string mangaPath = "", string mediaPath = "", string imagePath = "", string describe = "", bool scraped = false)
        {
            InitializeComponent();
            labelMangaName.Text = mangaName;
            labelDescribe.Text = describe;
            labelTags.Text = String.Join("  ", tags);


            this.form = form;
            this.mediaPath = mediaPath;
            this.mangaPath = mangaPath;
            this.mangaName = mangaName;

            if (scraped)
            {
                buttonScraping.Text = "再次刮削";
            }

            pictureBoxCover.SizeMode = PictureBoxSizeMode.StretchImage; // 设置图片模式
            // pictureBoxCover.Dock = DockStyle.Fill; // 填充控件
            //var aaa = Image.FromFile(imagePath);
            if (File.Exists(imagePath))
            {
                try
                {
                    pictureBoxCover.Image = Image.FromFile(imagePath);
                }
                catch
                {
                    if (!global.alreadyAlertImageProblem)
                    {
                        MessageBox.Show("检测到有封面图,但图片展示错误,win端图片兼容性有限,请忽略此错误.");
                        global.alreadyAlertImageProblem = true;
                    }

                }
            }
        }

        private void mangaListItem_Load(object sender, EventArgs e)
        {

        }

        private void buttonScraping_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(global.url) && string.IsNullOrEmpty(global.html))
            {
                lkw.msbox("请先输入URL,或填充HTML CODE字段");
                return;
            }

            lkw.msbox("开始下载元数据");
            _ = lkw.NewWork(() =>
            {
                if (global.url.Contains("toptoon"))
                {
                    // 下载元数据与图片
                    toptoon = new TopToon(url: global.url, cookie: global.cookie, html: global.html, saveRoute: mediaPath);
                    toptoon.download_chapter_poster();
                    toptoon.download_chapter_poster();
                    toptoon.download_manga_info_poster();
                    toptoon.set_info();


                    lkw.log("任务执行完毕");
                    lkw.msbox("任务执行完毕");
                }

                //bangumi
                if (global.url.Contains("bangumi"))
                {
                    bangumi = new Bangumi(url: global.url, cookie: global.cookie, html: global.html, saveRoute: mediaPath);
                    bangumi.download_manga_info_poster();
                }

                //E站
                if (global.url.Contains("hentai"))
                {
                    ehentai = new Ehentai(url: global.url, cookie: global.cookie, html: global.html, saveRoute: mediaPath);
                    ehentai.download_manga_info_poster();
                    ehentai.set_info();
                }


            });


        }

        public bool is_img(string filePath)
        {
            string pattern = @"(.bmp|.jpg|.jpeg|.png|.tif|.gif|.pcx|.tga|.exif|.fpx|.svg|.psd|.cdr|.pcd|.dxf|.ufo|.eps|.ai|.raw|.WMF|.webp|.avif|.apng)$";

            return Regex.IsMatch(filePath, pattern, RegexOptions.IgnoreCase);
        }

        private void buttonArrange_Click(object sender, EventArgs e)
        {

            if (toptoon != null)
            {
                var targetMedia = toptoon;

                BindingList<FileInfo> fileList = new BindingList<FileInfo>();
                BindingList<string> chapterList = new BindingList<string>();

                foreach (string fileName in Directory.GetFileSystemEntries(mangaPath))
                {
                    // 目标为文件夹与压缩包 发现为图片则剔除
                    if (is_img(fileName)) continue;

                    fileList.Add(new FileInfo(fileName));

                    lkw.log(fileName);

                    // 替换文件名
                    //replace(fileName, oldStr, newStr);
                }

                toptoon.chapters.ForEach(item =>
                {
                    chapterList.Add(item.name);
                });
                FormRename form2 = new FormRename(fileList, chapterList, toptoon.name, toptoon.downloadRoute, toptoon.name != mangaName);
                form2.Show();
            }
            else if (bangumi != null)
            {
                foreach (string fileName in Directory.GetFileSystemEntries(mangaPath))
                {
                    string baseName = Path.GetFileName(fileName);
                    // 创建新名称 路径+漫画名+章节名+扩展名
                    string newName = Path.Combine(bangumi.downloadRoute, baseName);

                    if (File.Exists(fileName)) File.Move(fileName, newName);
                    else if (Directory.Exists(fileName)) Directory.Move(fileName, newName);
                }

                bool deleteOldPath = Path.GetFileName(this.mangaPath) != ehentai.name;
                if (deleteOldPath)
                {
                    string floder = Path.Combine(this.mediaPath, ".smangaManager");
                    string newPath = Path.Combine(floder, Path.GetFileName(this.mangaPath));
                    if (!Directory.Exists(floder))
                    {
                        Directory.CreateDirectory(floder);
                    }

                    if (!Directory.Exists(newPath))
                    {
                        Directory.Move(this.mangaPath, floder);
                    }
                    else
                    {
                        Directory.Delete(this.mangaPath, true);
                    }
                }
            }
            else if (ehentai != null)
            {
                foreach (string fileName in Directory.GetFileSystemEntries(mangaPath))
                {
                    string baseName = Path.GetFileName(fileName);
                    // 创建新名称 路径+漫画名+章节名+扩展名
                    string newName = Path.Combine(ehentai.downloadRoute, baseName);
                    Console.WriteLine(fileName + " " + newName);
                    if (File.Exists(fileName)) File.Move(fileName, newName);
                    else if (Directory.Exists(fileName)) Directory.Move(fileName, newName);
                }

                bool deleteOldPath = Path.GetFileName(this.mangaPath) != ehentai.name;
                if (deleteOldPath)
                {
                    string floder = Path.Combine(this.mediaPath, ".smangaManager");
                    string newPath = Path.Combine(floder, Path.GetFileName(this.mangaPath));
                    if (!Directory.Exists(floder))
                    {
                        Directory.CreateDirectory(floder);
                    }

                    if (!Directory.Exists(newPath))
                    {
                        Directory.Move(this.mangaPath, floder);
                    }
                    else
                    {
                        Directory.Delete(this.mangaPath, true);
                    }
                }
            }
            else
            {
                lkw.msbox("请先对漫画进行刮削!");
                return;
            }


        }
    }
}
