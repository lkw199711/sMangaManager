namespace sMangaManager
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.buttonTest = new System.Windows.Forms.Button();
            this.textBoxReNameFolder = new System.Windows.Forms.TextBox();
            this.buttonReName = new System.Windows.Forms.Button();
            this.buttonDownLoad = new System.Windows.Forms.Button();
            this.richTextBoxHtml = new System.Windows.Forms.RichTextBox();
            this.buttonSinglePage = new System.Windows.Forms.Button();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.buttonBanner = new System.Windows.Forms.Button();
            this.buttonAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Location = new System.Drawing.Point(67, 24);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(542, 21);
            this.textBoxUrl.TabIndex = 0;
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(713, 72);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 1;
            this.buttonTest.Text = "button1";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // textBoxReNameFolder
            // 
            this.textBoxReNameFolder.Location = new System.Drawing.Point(67, 90);
            this.textBoxReNameFolder.Name = "textBoxReNameFolder";
            this.textBoxReNameFolder.Size = new System.Drawing.Size(542, 21);
            this.textBoxReNameFolder.TabIndex = 2;
            // 
            // buttonReName
            // 
            this.buttonReName.Location = new System.Drawing.Point(713, 180);
            this.buttonReName.Name = "buttonReName";
            this.buttonReName.Size = new System.Drawing.Size(75, 23);
            this.buttonReName.TabIndex = 3;
            this.buttonReName.Text = "重命名";
            this.buttonReName.UseVisualStyleBackColor = true;
            this.buttonReName.Click += new System.EventHandler(this.buttonReName_Click);
            // 
            // buttonDownLoad
            // 
            this.buttonDownLoad.Location = new System.Drawing.Point(713, 242);
            this.buttonDownLoad.Name = "buttonDownLoad";
            this.buttonDownLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonDownLoad.TabIndex = 4;
            this.buttonDownLoad.Text = "下载封面图";
            this.buttonDownLoad.UseVisualStyleBackColor = true;
            this.buttonDownLoad.Click += new System.EventHandler(this.buttonDownLoad_Click);
            // 
            // richTextBoxHtml
            // 
            this.richTextBoxHtml.Location = new System.Drawing.Point(67, 147);
            this.richTextBoxHtml.Name = "richTextBoxHtml";
            this.richTextBoxHtml.Size = new System.Drawing.Size(542, 291);
            this.richTextBoxHtml.TabIndex = 5;
            this.richTextBoxHtml.Text = "";
            // 
            // buttonSinglePage
            // 
            this.buttonSinglePage.Location = new System.Drawing.Point(713, 300);
            this.buttonSinglePage.Name = "buttonSinglePage";
            this.buttonSinglePage.Size = new System.Drawing.Size(75, 23);
            this.buttonSinglePage.TabIndex = 6;
            this.buttonSinglePage.Text = "单页下载";
            this.buttonSinglePage.UseVisualStyleBackColor = true;
            this.buttonSinglePage.Click += new System.EventHandler(this.buttonSinglePage_Click);
            // 
            // buttonInfo
            // 
            this.buttonInfo.Location = new System.Drawing.Point(713, 349);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(75, 23);
            this.buttonInfo.TabIndex = 7;
            this.buttonInfo.Text = "生成info";
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // buttonBanner
            // 
            this.buttonBanner.Location = new System.Drawing.Point(713, 397);
            this.buttonBanner.Name = "buttonBanner";
            this.buttonBanner.Size = new System.Drawing.Size(75, 23);
            this.buttonBanner.TabIndex = 8;
            this.buttonBanner.Text = "下载banner";
            this.buttonBanner.UseVisualStyleBackColor = true;
            this.buttonBanner.Click += new System.EventHandler(this.buttonBanner_Click);
            // 
            // buttonAll
            // 
            this.buttonAll.Location = new System.Drawing.Point(713, 122);
            this.buttonAll.Name = "buttonAll";
            this.buttonAll.Size = new System.Drawing.Size(75, 23);
            this.buttonAll.TabIndex = 9;
            this.buttonAll.Text = "下载全部";
            this.buttonAll.UseVisualStyleBackColor = true;
            this.buttonAll.Click += new System.EventHandler(this.buttonAll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonAll);
            this.Controls.Add(this.buttonBanner);
            this.Controls.Add(this.buttonInfo);
            this.Controls.Add(this.buttonSinglePage);
            this.Controls.Add(this.richTextBoxHtml);
            this.Controls.Add(this.buttonDownLoad);
            this.Controls.Add(this.buttonReName);
            this.Controls.Add(this.textBoxReNameFolder);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.textBoxUrl);
            this.Name = "Form1";
            this.Text = "FormIndex";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.TextBox textBoxReNameFolder;
        private System.Windows.Forms.Button buttonReName;
        private System.Windows.Forms.Button buttonDownLoad;
        private System.Windows.Forms.RichTextBox richTextBoxHtml;
        private System.Windows.Forms.Button buttonSinglePage;
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.Button buttonBanner;
        private System.Windows.Forms.Button buttonAll;
    }
}

