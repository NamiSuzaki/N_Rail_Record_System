using System;
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
        public W03_RecordsList w03_n = null;
        public W09_RecordsInsert w09 = null;

        public W01()
        {
            InitializeComponent();
        }

        // 『乗車記録確認・修正・削除』ボタン押下
        // 一覧＆検索画面を表示
        private void goW02_Click(object sender, EventArgs e)
        {
            // 二重起動防止
            // null、または画面が破棄されていたら開く
            if (this.w03_n == null || this.w03_n.IsDisposed)
            {
                this.w03_n = new W03_RecordsList();
                w03_n.Show();
            }
        }

        // 乗車記録新規登録ボタン押下
        // 新規登録フォームを開く
        private void goW09_Click(object sender, EventArgs e)
        {
            // 二重起動防止
            // null、または画面が破棄されていたら開く
            if (this.w09 == null || this.w09.IsDisposed)
            {
                W09_RecordsInsert w09 = new W09_RecordsInsert();
                w09.Show();
            }
        }

        // 『アプリ終了』ボタン押下
        private void goW12_Click(object sender, EventArgs e)
        {
            // アプリ終了確認ダイアログの表示
            DialogResult result = MessageBox.Show
                ("アプリケーションを終了しますか？", "アプリケーションの終了", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            // 『はい』を選択
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // アプリ終了
                Application.Exit();
            }
        }
    }
}
