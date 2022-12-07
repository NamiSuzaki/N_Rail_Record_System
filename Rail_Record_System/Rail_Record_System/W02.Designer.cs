
namespace Rail_Record_System
{
    partial class W02
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.W02_Title = new System.Windows.Forms.Label();
            this.goW03 = new System.Windows.Forms.Button();
            this.gobacktoW01 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // W02_Title
            // 
            this.W02_Title.AutoSize = true;
            this.W02_Title.Font = new System.Drawing.Font("MS UI Gothic", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.W02_Title.Location = new System.Drawing.Point(315, 49);
            this.W02_Title.Name = "W02_Title";
            this.W02_Title.Size = new System.Drawing.Size(308, 48);
            this.W02_Title.TabIndex = 0;
            this.W02_Title.Text = "乗車記録一覧";
            this.W02_Title.Click += new System.EventHandler(this.W02_Title_Click);
            // 
            // goW03
            // 
            this.goW03.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.goW03.Location = new System.Drawing.Point(669, 34);
            this.goW03.Name = "goW03";
            this.goW03.Size = new System.Drawing.Size(230, 90);
            this.goW03.TabIndex = 1;
            this.goW03.Text = "検索画面表示";
            this.goW03.UseVisualStyleBackColor = true;
            this.goW03.Click += new System.EventHandler(this.goW03_Click);
            // 
            // gobacktoW01
            // 
            this.gobacktoW01.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.gobacktoW01.Location = new System.Drawing.Point(48, 32);
            this.gobacktoW01.Name = "gobacktoW01";
            this.gobacktoW01.Size = new System.Drawing.Size(224, 38);
            this.gobacktoW01.TabIndex = 2;
            this.gobacktoW01.Text = "戻る";
            this.gobacktoW01.UseVisualStyleBackColor = true;
            this.gobacktoW01.Click += new System.EventHandler(this.gobacktoW01_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(43, 165);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(856, 372);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(48, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 56);
            this.button1.TabIndex = 4;
            this.button1.Text = "更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // W02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.gobacktoW01);
            this.Controls.Add(this.goW03);
            this.Controls.Add(this.W02_Title);
            this.MaximumSize = new System.Drawing.Size(938, 589);
            this.MinimumSize = new System.Drawing.Size(938, 589);
            this.Name = "W02";
            this.Size = new System.Drawing.Size(938, 589);
            this.Load += new System.EventHandler(this.W02_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label W02_Title;
        private System.Windows.Forms.Button goW03;
        private System.Windows.Forms.Button gobacktoW01;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
    }
}
