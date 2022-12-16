﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rail_Record_System
{
    public partial class W01 : UserControl
    {
        public W03_N w03_n = null;

        public W01()
        {
            InitializeComponent();
        }

        // 『乗車記録確認・修正・削除』ボタン押下
        private void goW02_Click(object sender, EventArgs e)
        {
            // メインメニュー画面の非表示、一覧表示画面を表示
            //Formmain.uc_w01.Visible = false;
            //Formmain.uc_w02.Visible = true;

            /* 二重起動防止 */
            if (this.w03_n == null || this.w03_n.IsDisposed)
            { /* ヌル、または破棄されていたら */
                this.w03_n = new W03_N();
                w03_n.Show();
            }
        }

        // 乗車記録新規登録ボタン押下
        // 新規登録フォームを開く
        private void goW09_Click(object sender, EventArgs e)
        {
            W09 w09 = new W09();
            w09.Show();
        }

        // 『アプリ終了』ボタン押下
        private void goW12_Click(object sender, EventArgs e)
        {
            // アプリ終了確認ダイアログの表示
            DialogResult result = MessageBox.Show
                ("アプリケーションを終了しますか？", "アプリケーションの終了", MessageBoxButtons.YesNo);

            // 『はい』を選択
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // アプリ終了
                Application.Exit();
            }
        }
    }
}
