namespace sMangaManager
{
    partial class FormRename
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.listBoxChapters = new System.Windows.Forms.ListBox();
            this.buttonTest = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.buttonSortByNum = new System.Windows.Forms.Button();
            this.buttonSortByAscii = new System.Windows.Forms.Button();
            this.labelFileLength = new System.Windows.Forms.Label();
            this.labelchapterLength = new System.Windows.Forms.Label();
            this.buttonDo = new System.Windows.Forms.Button();
            this.labelTip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.ItemHeight = 12;
            this.listBoxFiles.Location = new System.Drawing.Point(12, 12);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(370, 424);
            this.listBoxFiles.TabIndex = 0;
            this.listBoxFiles.SelectedIndexChanged += new System.EventHandler(this.listBoxFiles_SelectedIndexChanged);
            // 
            // listBoxChapters
            // 
            this.listBoxChapters.FormattingEnabled = true;
            this.listBoxChapters.ItemHeight = 12;
            this.listBoxChapters.Location = new System.Drawing.Point(388, 12);
            this.listBoxChapters.Name = "listBoxChapters";
            this.listBoxChapters.Size = new System.Drawing.Size(400, 424);
            this.listBoxChapters.TabIndex = 1;
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(713, 499);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 2;
            this.buttonTest.Text = "button1";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Visible = false;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // buttonSortByNum
            // 
            this.buttonSortByNum.Location = new System.Drawing.Point(12, 539);
            this.buttonSortByNum.Name = "buttonSortByNum";
            this.buttonSortByNum.Size = new System.Drawing.Size(99, 23);
            this.buttonSortByNum.TabIndex = 3;
            this.buttonSortByNum.Text = "按照数字排序";
            this.buttonSortByNum.UseVisualStyleBackColor = true;
            this.buttonSortByNum.Click += new System.EventHandler(this.buttonSortByNum_Click);
            // 
            // buttonSortByAscii
            // 
            this.buttonSortByAscii.Location = new System.Drawing.Point(12, 581);
            this.buttonSortByAscii.Name = "buttonSortByAscii";
            this.buttonSortByAscii.Size = new System.Drawing.Size(105, 23);
            this.buttonSortByAscii.TabIndex = 4;
            this.buttonSortByAscii.Text = "按照ASCII排序";
            this.buttonSortByAscii.UseVisualStyleBackColor = true;
            this.buttonSortByAscii.Click += new System.EventHandler(this.buttonSortByAscii_Click);
            // 
            // labelFileLength
            // 
            this.labelFileLength.AutoSize = true;
            this.labelFileLength.Location = new System.Drawing.Point(10, 448);
            this.labelFileLength.Name = "labelFileLength";
            this.labelFileLength.Size = new System.Drawing.Size(41, 12);
            this.labelFileLength.TabIndex = 5;
            this.labelFileLength.Text = "label1";
            // 
            // labelchapterLength
            // 
            this.labelchapterLength.AutoSize = true;
            this.labelchapterLength.Location = new System.Drawing.Point(386, 448);
            this.labelchapterLength.Name = "labelchapterLength";
            this.labelchapterLength.Size = new System.Drawing.Size(41, 12);
            this.labelchapterLength.TabIndex = 6;
            this.labelchapterLength.Text = "label1";
            // 
            // buttonDo
            // 
            this.buttonDo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonDo.Location = new System.Drawing.Point(388, 539);
            this.buttonDo.Name = "buttonDo";
            this.buttonDo.Size = new System.Drawing.Size(160, 63);
            this.buttonDo.TabIndex = 7;
            this.buttonDo.Text = "确认文件数量与顺序无误,执行重命名";
            this.buttonDo.UseVisualStyleBackColor = true;
            this.buttonDo.Click += new System.EventHandler(this.buttonDo_Click);
            // 
            // labelTip
            // 
            this.labelTip.AutoSize = true;
            this.labelTip.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTip.ForeColor = System.Drawing.Color.Red;
            this.labelTip.Location = new System.Drawing.Point(113, 485);
            this.labelTip.Name = "labelTip";
            this.labelTip.Size = new System.Drawing.Size(530, 21);
            this.labelTip.TabIndex = 8;
            this.labelTip.Text = "请调整重命名文件夹内的文件循序与章节名保持逐行匹配,并保证数量一致.";
            // 
            // FormRename
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 628);
            this.Controls.Add(this.labelTip);
            this.Controls.Add(this.buttonDo);
            this.Controls.Add(this.labelchapterLength);
            this.Controls.Add(this.labelFileLength);
            this.Controls.Add(this.buttonSortByAscii);
            this.Controls.Add(this.buttonSortByNum);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.listBoxChapters);
            this.Controls.Add(this.listBoxFiles);
            this.Name = "FormRename";
            this.Text = "FormRename";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.ListBox listBoxChapters;
        private System.Windows.Forms.Button buttonTest;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button buttonSortByNum;
        private System.Windows.Forms.Button buttonSortByAscii;
        private System.Windows.Forms.Label labelFileLength;
        private System.Windows.Forms.Label labelchapterLength;
        private System.Windows.Forms.Button buttonDo;
        private System.Windows.Forms.Label labelTip;
    }
}