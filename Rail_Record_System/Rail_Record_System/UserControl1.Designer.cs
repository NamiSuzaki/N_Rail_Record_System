
namespace Rail_Record_System
{
    partial class UserControl1
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
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.goW02 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(687, 397);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(160, 61);
            this.button4.TabIndex = 7;
            this.button4.Text = "テストフォームへ";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button3.Location = new System.Drawing.Point(303, 397);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(331, 125);
            this.button3.TabIndex = 6;
            this.button3.Text = "アプリ終了";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button2.Location = new System.Drawing.Point(486, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(361, 284);
            this.button2.TabIndex = 5;
            this.button2.Text = "乗車記録\r\n新規登録";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // goW02
            // 
            this.goW02.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.goW02.Location = new System.Drawing.Point(90, 80);
            this.goW02.Name = "goW02";
            this.goW02.Size = new System.Drawing.Size(361, 284);
            this.goW02.TabIndex = 4;
            this.goW02.Text = "乗車記録\r\n確認・修正・削除";
            this.goW02.UseVisualStyleBackColor = true;
            this.goW02.Click += new System.EventHandler(this.button1_Click);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.goW02);
            this.MaximumSize = new System.Drawing.Size(938, 589);
            this.MinimumSize = new System.Drawing.Size(938, 589);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(938, 589);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button goW02;
    }
}
