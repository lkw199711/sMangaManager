using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace sMangaManager
{
    public partial class FormRename : Form
    {
        private BindingList<string> items;
        private BindingList<FileInfo> fileList;
        private BindingList<string> chapterList;
        private string mangaName;
        private string scrapingRouter;
        private bool deleteOldPath;
        public FormRename(BindingList<FileInfo> fileList, BindingList<string> chapterList, string mangaName, string scrapingRouter, bool deleteOldPath = false)
        {
            InitializeComponent();

            // 准备数据源
            items = new BindingList<string>
        {
            "Item 1已加载“C:\\Windows\\Microsoft.Net\\assembly\\GAC_MSIL\\System.Windows.Forms\\v4.0_4.0.0.0__b77a5c561934e089\\System.Windows.Forms.dll”。已跳过加载符号。模块进行了优化，并且调试器选项“仅我的代码”已启用。",
            "Item 2",
            "Item 3",
            "Item 4"
        };

            this.fileList = fileList;
            this.chapterList = chapterList;
            this.mangaName = mangaName;
            // 整理后的漫画路径
            this.scrapingRouter = scrapingRouter;
            this.deleteOldPath = deleteOldPath;
            // 设置 ListBox 的 DataSource
            listBoxFiles.DisplayMember = "Name";
            listBoxFiles.ValueMember = "FullName";
            listBoxFiles.DataSource = this.fileList;

            listBoxChapters.DataSource = this.chapterList;

            labelFileLength.Text = $"重命名文件夹内文件数量: {fileList.Count.ToString()}";
            labelchapterLength.Text = $"匹配漫画的章节数量: {chapterList.Count.ToString()}";


        }

        private void SortItemsByNumber()
        {
            // 使用 LINQ 提取数字并排序
            var sortedItems = fileList
                .Select((item, indexer) => new
                {
                    Original = item,
                    Number = ExtractNumber(item.Name, indexer + 9999)
                })
                .OrderBy(x => x.Number)
                .Select(x => x.Original)
                .ToList();

            // 清空原 BindingList 并重新添加排序后的项
            fileList.Clear();
            foreach (var item in sortedItems)
            {
                fileList.Add(item);
            }
        }

        private void SortItemsByAscii()
        {
            // 直接使用 Sort 方法进行 ASCII 排序
            var sortedItems = fileList.OrderBy(item => item.Name).ToList();

            // 清空原 BindingList 并重新添加排序后的项
            fileList.Clear();
            foreach (var item in sortedItems)
            {
                fileList.Add(item);
            }
        }

        private int ExtractNumber(string input, int defaultNum)
        {
            // 使用正则表达式提取字符串中的数字
            var match = Regex.Match(input, @"\d+");
            return match.Success ? int.Parse(match.Value) : defaultNum; // 如果没有找到数字，则返回 0
        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            fileList.Add(new FileInfo("item5"));
        }

        private void buttonSortByNum_Click(object sender, EventArgs e)
        {
            SortItemsByNumber();
        }

        private void buttonSortByAscii_Click(object sender, EventArgs e)
        {
            SortItemsByAscii();
        }

        private void buttonDo_Click(object sender, EventArgs e)
        {
            if (fileList.Count != chapterList.Count)
            {
                MessageBox.Show("章节数量不一致,不能重命名\n" + labelFileLength.Text + "\n" + labelchapterLength.Text);
                return;
            }

            for (int i = 0; i < chapterList.Count; i++)
            {
                // 创建新名称 路径+漫画名+章节名+扩展名
                string newName = Path.Combine(scrapingRouter, chapterList[i] + fileList[i].Extension); ;

                try //路径含有非法字符 跳过
                {
                    if (File.Exists(fileList[i].FullName)) File.Move(fileList[i].FullName, newName);
                    else if (Directory.Exists(fileList[i].FullName)) Directory.Move(fileList[i].FullName, newName);
                }
                catch
                {
                    Console.WriteLine(newName + "路径含有非法字符 跳过 请手动重命名");
                    continue;
                }
            }

            if (deleteOldPath)
            {
                string oldPath = fileList[0].DirectoryName;
                string floder = Path.Combine(scrapingRouter,"..", ".smangaManager");
                string newPath = Path.Combine(floder, Path.GetFileName(oldPath));
                if (!Directory.Exists(floder))
                {
                    Directory.CreateDirectory(floder);
                }

                if (!Directory.Exists(newPath))
                {
                    Directory.Move(oldPath, newPath);
                }
                else
                {
                    Directory.Delete(oldPath, true);
                }
            }

            MessageBox.Show("重命名执行完毕!");
        }
    }


}
