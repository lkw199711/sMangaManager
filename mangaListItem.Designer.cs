namespace sMangaManager
{
    partial class MangaListItem
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxCover = new System.Windows.Forms.PictureBox();
            this.labelMangaName = new System.Windows.Forms.Label();
            this.buttonScraping = new System.Windows.Forms.Button();
            this.labelDescribe = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.labelTags = new System.Windows.Forms.Label();
            this.buttonArrange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxCover
            // 
            this.pictureBoxCover.Location = new System.Drawing.Point(13, 10);
            this.pictureBoxCover.Name = "pictureBoxCover";
            this.pictureBoxCover.Size = new System.Drawing.Size(90, 135);
            this.pictureBoxCover.TabIndex = 0;
            this.pictureBoxCover.TabStop = false;
            // 
            // labelMangaName
            // 
            this.labelMangaName.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMangaName.Location = new System.Drawing.Point(124, 18);
            this.labelMangaName.Name = "labelMangaName";
            this.labelMangaName.Size = new System.Drawing.Size(311, 28);
            this.labelMangaName.TabIndex = 1;
            this.labelMangaName.Text = "漫画名称";
            // 
            // buttonScraping
            // 
            this.buttonScraping.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonScraping.Location = new System.Drawing.Point(537, 18);
            this.buttonScraping.Name = "buttonScraping";
            this.buttonScraping.Size = new System.Drawing.Size(90, 30);
            this.buttonScraping.TabIndex = 2;
            this.buttonScraping.Text = "刮削";
            this.buttonScraping.UseVisualStyleBackColor = true;
            this.buttonScraping.Click += new System.EventHandler(this.buttonScraping_Click);
            // 
            // labelDescribe
            // 
            this.labelDescribe.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDescribe.Location = new System.Drawing.Point(127, 62);
            this.labelDescribe.Name = "labelDescribe";
            this.labelDescribe.Size = new System.Drawing.Size(500, 60);
            this.labelDescribe.TabIndex = 3;
            this.labelDescribe.Text = "漫画简介";
            // 
            // labelTags
            // 
            this.labelTags.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTags.ForeColor = System.Drawing.Color.Red;
            this.labelTags.Location = new System.Drawing.Point(126, 133);
            this.labelTags.Name = "labelTags";
            this.labelTags.Size = new System.Drawing.Size(501, 19);
            this.labelTags.TabIndex = 4;
            this.labelTags.Text = "标签";
            // 
            // buttonArrange
            // 
            this.buttonArrange.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonArrange.Location = new System.Drawing.Point(441, 18);
            this.buttonArrange.Name = "buttonArrange";
            this.buttonArrange.Size = new System.Drawing.Size(90, 30);
            this.buttonArrange.TabIndex = 5;
            this.buttonArrange.Text = "整理文件";
            this.buttonArrange.UseVisualStyleBackColor = true;
            this.buttonArrange.Click += new System.EventHandler(this.buttonArrange_Click);
            // 
            // MangaListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Moccasin;
            this.Controls.Add(this.buttonArrange);
            this.Controls.Add(this.labelTags);
            this.Controls.Add(this.labelDescribe);
            this.Controls.Add(this.buttonScraping);
            this.Controls.Add(this.labelMangaName);
            this.Controls.Add(this.pictureBoxCover);
            this.Name = "MangaListItem";
            this.Size = new System.Drawing.Size(630, 155);
            this.Load += new System.EventHandler(this.mangaListItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxCover;
        private System.Windows.Forms.Label labelMangaName;
        private System.Windows.Forms.Button buttonScraping;
        private System.Windows.Forms.Label labelDescribe;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label labelTags;
        private System.Windows.Forms.Button buttonArrange;
    }
}
