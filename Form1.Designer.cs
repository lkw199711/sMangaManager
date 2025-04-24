namespace sMangaManager
{
    partial class FormIndex
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
            this.buttonReName = new System.Windows.Forms.Button();
            this.buttonDownLoad = new System.Windows.Forms.Button();
            this.richTextBoxHtml = new System.Windows.Forms.RichTextBox();
            this.buttonSinglePage = new System.Windows.Forms.Button();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.buttonBanner = new System.Windows.Forms.Button();
            this.buttonAll = new System.Windows.Forms.Button();
            this.buttonNoCookie = new System.Windows.Forms.Button();
            this.labelUrlTip = new System.Windows.Forms.Label();
            this.labelHtmlCodeTip = new System.Windows.Forms.Label();
            this.groupBoxHtmlCode = new System.Windows.Forms.GroupBox();
            this.groupBoxURL = new System.Windows.Forms.GroupBox();
            this.groupBoxSvaeRoute = new System.Windows.Forms.GroupBox();
            this.buttonAddRoute = new System.Windows.Forms.Button();
            this.comboBoxRoute = new System.Windows.Forms.ComboBox();
            this.groupBoxBtn = new System.Windows.Forms.GroupBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.panelMangaList = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxBtn2 = new System.Windows.Forms.GroupBox();
            this.groupBoxHtmlCode.SuspendLayout();
            this.groupBoxURL.SuspendLayout();
            this.groupBoxSvaeRoute.SuspendLayout();
            this.groupBoxBtn.SuspendLayout();
            this.groupBoxBtn2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Location = new System.Drawing.Point(6, 20);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(542, 21);
            this.textBoxUrl.TabIndex = 0;
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(87, 20);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 40);
            this.buttonTest.TabIndex = 1;
            this.buttonTest.Text = "Text";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // buttonReName
            // 
            this.buttonReName.Location = new System.Drawing.Point(6, 66);
            this.buttonReName.Name = "buttonReName";
            this.buttonReName.Size = new System.Drawing.Size(75, 40);
            this.buttonReName.TabIndex = 3;
            this.buttonReName.Text = "重命名";
            this.buttonReName.UseVisualStyleBackColor = true;
            this.buttonReName.Click += new System.EventHandler(this.buttonReName_Click);
            // 
            // buttonDownLoad
            // 
            this.buttonDownLoad.Location = new System.Drawing.Point(6, 204);
            this.buttonDownLoad.Name = "buttonDownLoad";
            this.buttonDownLoad.Size = new System.Drawing.Size(75, 40);
            this.buttonDownLoad.TabIndex = 4;
            this.buttonDownLoad.Text = "下载封面图";
            this.buttonDownLoad.UseVisualStyleBackColor = true;
            this.buttonDownLoad.Click += new System.EventHandler(this.buttonDownLoad_Click);
            // 
            // richTextBoxHtml
            // 
            this.richTextBoxHtml.Location = new System.Drawing.Point(6, 20);
            this.richTextBoxHtml.Name = "richTextBoxHtml";
            this.richTextBoxHtml.Size = new System.Drawing.Size(542, 260);
            this.richTextBoxHtml.TabIndex = 5;
            this.richTextBoxHtml.Text = "";
            // 
            // buttonSinglePage
            // 
            this.buttonSinglePage.Location = new System.Drawing.Point(6, 66);
            this.buttonSinglePage.Name = "buttonSinglePage";
            this.buttonSinglePage.Size = new System.Drawing.Size(75, 40);
            this.buttonSinglePage.TabIndex = 6;
            this.buttonSinglePage.Text = "单页下载";
            this.buttonSinglePage.UseVisualStyleBackColor = true;
            this.buttonSinglePage.Click += new System.EventHandler(this.buttonSinglePage_Click);
            // 
            // buttonInfo
            // 
            this.buttonInfo.Location = new System.Drawing.Point(6, 112);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(75, 40);
            this.buttonInfo.TabIndex = 7;
            this.buttonInfo.Text = "生成info";
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // buttonBanner
            // 
            this.buttonBanner.Location = new System.Drawing.Point(6, 158);
            this.buttonBanner.Name = "buttonBanner";
            this.buttonBanner.Size = new System.Drawing.Size(75, 40);
            this.buttonBanner.TabIndex = 8;
            this.buttonBanner.Text = "下载banner";
            this.buttonBanner.UseVisualStyleBackColor = true;
            this.buttonBanner.Click += new System.EventHandler(this.buttonBanner_Click);
            // 
            // buttonAll
            // 
            this.buttonAll.Location = new System.Drawing.Point(6, 20);
            this.buttonAll.Name = "buttonAll";
            this.buttonAll.Size = new System.Drawing.Size(75, 40);
            this.buttonAll.TabIndex = 9;
            this.buttonAll.Text = "直接下载";
            this.buttonAll.UseVisualStyleBackColor = true;
            this.buttonAll.Click += new System.EventHandler(this.buttonAll_Click);
            // 
            // buttonNoCookie
            // 
            this.buttonNoCookie.Location = new System.Drawing.Point(6, 19);
            this.buttonNoCookie.Name = "buttonNoCookie";
            this.buttonNoCookie.Size = new System.Drawing.Size(104, 40);
            this.buttonNoCookie.TabIndex = 10;
            this.buttonNoCookie.Text = "无cookie下载";
            this.buttonNoCookie.UseVisualStyleBackColor = true;
            this.buttonNoCookie.Click += new System.EventHandler(this.buttonNoCookie_Click);
            // 
            // labelUrlTip
            // 
            this.labelUrlTip.ForeColor = System.Drawing.Color.Green;
            this.labelUrlTip.Location = new System.Drawing.Point(6, 44);
            this.labelUrlTip.Name = "labelUrlTip";
            this.labelUrlTip.Size = new System.Drawing.Size(544, 15);
            this.labelUrlTip.TabIndex = 16;
            this.labelUrlTip.Text = "请键入目标漫画的章节页面地址";
            // 
            // labelHtmlCodeTip
            // 
            this.labelHtmlCodeTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelHtmlCodeTip.Location = new System.Drawing.Point(6, 283);
            this.labelHtmlCodeTip.Name = "labelHtmlCodeTip";
            this.labelHtmlCodeTip.Size = new System.Drawing.Size(458, 28);
            this.labelHtmlCodeTip.TabIndex = 18;
            this.labelHtmlCodeTip.Text = "顶通在大陆需要代理访问,如果遇到网页可访问但是此软件连接不通的情况,可以将页面源代码输入到此框中,代替url使用. 如不懂请留空";
            // 
            // groupBoxHtmlCode
            // 
            this.groupBoxHtmlCode.AutoSize = true;
            this.groupBoxHtmlCode.Controls.Add(this.richTextBoxHtml);
            this.groupBoxHtmlCode.Controls.Add(this.labelHtmlCodeTip);
            this.groupBoxHtmlCode.Location = new System.Drawing.Point(17, 203);
            this.groupBoxHtmlCode.Name = "groupBoxHtmlCode";
            this.groupBoxHtmlCode.Size = new System.Drawing.Size(554, 328);
            this.groupBoxHtmlCode.TabIndex = 19;
            this.groupBoxHtmlCode.TabStop = false;
            this.groupBoxHtmlCode.Text = "HTML CODE";
            // 
            // groupBoxURL
            // 
            this.groupBoxURL.AutoSize = true;
            this.groupBoxURL.Controls.Add(this.textBoxUrl);
            this.groupBoxURL.Controls.Add(this.labelUrlTip);
            this.groupBoxURL.Location = new System.Drawing.Point(17, 121);
            this.groupBoxURL.Name = "groupBoxURL";
            this.groupBoxURL.Size = new System.Drawing.Size(556, 76);
            this.groupBoxURL.TabIndex = 21;
            this.groupBoxURL.TabStop = false;
            this.groupBoxURL.Text = "URL";
            // 
            // groupBoxSvaeRoute
            // 
            this.groupBoxSvaeRoute.AutoSize = true;
            this.groupBoxSvaeRoute.Controls.Add(this.buttonAddRoute);
            this.groupBoxSvaeRoute.Controls.Add(this.comboBoxRoute);
            this.groupBoxSvaeRoute.Location = new System.Drawing.Point(17, 45);
            this.groupBoxSvaeRoute.Name = "groupBoxSvaeRoute";
            this.groupBoxSvaeRoute.Size = new System.Drawing.Size(556, 61);
            this.groupBoxSvaeRoute.TabIndex = 22;
            this.groupBoxSvaeRoute.TabStop = false;
            this.groupBoxSvaeRoute.Text = "存储路径";
            // 
            // buttonAddRoute
            // 
            this.buttonAddRoute.Location = new System.Drawing.Point(475, 19);
            this.buttonAddRoute.Name = "buttonAddRoute";
            this.buttonAddRoute.Size = new System.Drawing.Size(75, 21);
            this.buttonAddRoute.TabIndex = 11;
            this.buttonAddRoute.Text = "添加路径";
            this.buttonAddRoute.UseVisualStyleBackColor = true;
            this.buttonAddRoute.Click += new System.EventHandler(this.buttonAddRoute_Click);
            // 
            // comboBoxRoute
            // 
            this.comboBoxRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRoute.FormattingEnabled = true;
            this.comboBoxRoute.Location = new System.Drawing.Point(8, 20);
            this.comboBoxRoute.Name = "comboBoxRoute";
            this.comboBoxRoute.Size = new System.Drawing.Size(456, 20);
            this.comboBoxRoute.TabIndex = 25;
            this.comboBoxRoute.SelectedIndexChanged += new System.EventHandler(this.comboBoxRoute_SelectedIndexChanged);
            // 
            // groupBoxBtn
            // 
            this.groupBoxBtn.AutoSize = true;
            this.groupBoxBtn.Controls.Add(this.buttonReName);
            this.groupBoxBtn.Controls.Add(this.buttonDownLoad);
            this.groupBoxBtn.Controls.Add(this.buttonNoCookie);
            this.groupBoxBtn.Controls.Add(this.buttonInfo);
            this.groupBoxBtn.Controls.Add(this.buttonBanner);
            this.groupBoxBtn.Location = new System.Drawing.Point(1259, 45);
            this.groupBoxBtn.Name = "groupBoxBtn";
            this.groupBoxBtn.Size = new System.Drawing.Size(206, 310);
            this.groupBoxBtn.TabIndex = 23;
            this.groupBoxBtn.TabStop = false;
            this.groupBoxBtn.Visible = false;
            this.groupBoxBtn.Enter += new System.EventHandler(this.groupBoxBtn_Enter);
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(1263, 569);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(23, 12);
            this.labelVersion.TabIndex = 24;
            this.labelVersion.Text = "1.0";
            this.labelVersion.Click += new System.EventHandler(this.labelVersion_Click);
            // 
            // panelMangaList
            // 
            this.panelMangaList.AutoScroll = true;
            this.panelMangaList.BackColor = System.Drawing.Color.White;
            this.panelMangaList.Location = new System.Drawing.Point(591, 45);
            this.panelMangaList.Name = "panelMangaList";
            this.panelMangaList.Size = new System.Drawing.Size(662, 500);
            this.panelMangaList.TabIndex = 26;
            // 
            // groupBoxBtn2
            // 
            this.groupBoxBtn2.Controls.Add(this.buttonTest);
            this.groupBoxBtn2.Controls.Add(this.buttonAll);
            this.groupBoxBtn2.Controls.Add(this.buttonSinglePage);
            this.groupBoxBtn2.Location = new System.Drawing.Point(1259, 372);
            this.groupBoxBtn2.Name = "groupBoxBtn2";
            this.groupBoxBtn2.Size = new System.Drawing.Size(200, 173);
            this.groupBoxBtn2.TabIndex = 27;
            this.groupBoxBtn2.TabStop = false;
            this.groupBoxBtn2.Text = "功能按钮";
            // 
            // FormIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1478, 592);
            this.Controls.Add(this.groupBoxBtn2);
            this.Controls.Add(this.panelMangaList);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.groupBoxBtn);
            this.Controls.Add(this.groupBoxSvaeRoute);
            this.Controls.Add(this.groupBoxURL);
            this.Controls.Add(this.groupBoxHtmlCode);
            this.Name = "FormIndex";
            this.Text = "韩漫元数据下载器(顶通)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxHtmlCode.ResumeLayout(false);
            this.groupBoxURL.ResumeLayout(false);
            this.groupBoxURL.PerformLayout();
            this.groupBoxSvaeRoute.ResumeLayout(false);
            this.groupBoxBtn.ResumeLayout(false);
            this.groupBoxBtn2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Button buttonReName;
        private System.Windows.Forms.Button buttonDownLoad;
        private System.Windows.Forms.RichTextBox richTextBoxHtml;
        private System.Windows.Forms.Button buttonSinglePage;
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.Button buttonBanner;
        private System.Windows.Forms.Button buttonAll;
        private System.Windows.Forms.Button buttonNoCookie;
        private System.Windows.Forms.Label labelUrlTip;
        private System.Windows.Forms.Label labelHtmlCodeTip;
        private System.Windows.Forms.GroupBox groupBoxHtmlCode;
        private System.Windows.Forms.GroupBox groupBoxURL;
        private System.Windows.Forms.GroupBox groupBoxSvaeRoute;
        private System.Windows.Forms.GroupBox groupBoxBtn;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.ComboBox comboBoxRoute;
        private System.Windows.Forms.Button buttonAddRoute;
        private System.Windows.Forms.FlowLayoutPanel panelMangaList;
        private System.Windows.Forms.GroupBox groupBoxBtn2;
    }
}

