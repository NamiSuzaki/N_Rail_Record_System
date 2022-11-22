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
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        // 乗車記録確認・修正・削除
        private void button1_Click(object sender, EventArgs e)
        {

        }

        // 乗車記録新規登録
        private void button2_Click(object sender, EventArgs e)
        {

        }

        // 『アプリ終了』ボタン押下
        private void button3_Click(object sender, EventArgs e)
        {
            // アプリ終了確認ダイアログの表示
            DialogResult result = MessageBox.Show
                ("アプリケーションを終了しますか？", "アプリケーションの終了", MessageBoxButtons.YesNo);

            // 『はい』を選択
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // テストフォームの表示
        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}
