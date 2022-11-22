
namespace Rail_Record_System
{
    partial class W01
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
            this.goW12 = new System.Windows.Forms.Button();
            this.goW09 = new System.Windows.Forms.Button();
            this.goW02 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // goW12
            // 
            this.goW12.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.goW12.Location = new System.Drawing.Point(304, 390);
            this.goW12.Name = "goW12";
            this.goW12.Size = new System.Drawing.Size(331, 125);
            this.goW12.TabIndex = 10;
            this.goW12.Text = "アプリ終了";
            this.goW12.UseVisualStyleBackColor = true;
            this.goW12.Click += new System.EventHandler(this.goW12_Click);
            // 
            // goW09
            // 
            this.goW09.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.goW09.Location = new System.Drawing.Point(487, 73);
            this.goW09.Name = "goW09";
            this.goW09.Size = new System.Drawing.Size(361, 284);
            this.goW09.TabIndex = 9;
            this.goW09.Text = "乗車記録\r\n新規登録";
            this.goW09.UseVisualStyleBackColor = true;
            this.goW09.Click += new System.EventHandler(this.goW09_Click);
            // 
            // goW02
            // 
            this.goW02.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.goW02.Location = new System.Drawing.Point(91, 73);
            this.goW02.Name = "goW02";
            this.goW02.Size = new System.Drawing.Size(361, 284);
            this.goW02.TabIndex = 8;
            this.goW02.Text = "乗車記録\r\n確認・修正・削除";
            this.goW02.UseVisualStyleBackColor = true;
            this.goW02.Click += new System.EventHandler(this.goW02_Click);
            // 
            // W01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.goW12);
            this.Controls.Add(this.goW09);
            this.Controls.Add(this.goW02);
            this.MaximumSize = new System.Drawing.Size(938, 589);
            this.MinimumSize = new System.Drawing.Size(938, 589);
            this.Name = "W01";
            this.Size = new System.Drawing.Size(938, 589);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button goW12;
        private System.Windows.Forms.Button goW09;
        private System.Windows.Forms.Button goW02;
    }
}
