using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Rail_Record_System
{
    public partial class Formmain : Form
    {
        // staticで宣言することでインスタンスを固定
        // 各画面のユーザーコントロールのハコを置いておく
        public static W01 uc_w01;
        public static W02 uc_w02;

        public Formmain()
        {
            InitializeComponent();

            uc_w01 = new W01();
            uc_w02 = new W02();

            // パネルに各画面を追加
            panel1.Controls.Add(uc_w01);
            panel1.Controls.Add(uc_w02);

            // ユーコンの全画面表示
            uc_w01.Dock = DockStyle.Fill;
            uc_w02.Dock = DockStyle.Fill;

            // 最初はコントロール1（メインメニュー画面）のみを見えるようにする
            uc_w01.Visible = true;
            uc_w02.Visible = false;
        }

        // 起動時
        // テストフォームの表示
        private void Formmain_Load(object sender, EventArgs e)
        {
            // Form2 f2 = new Form2();
            // f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void w011_Load(object sender, EventArgs e)
        {

        }

        private void w021_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
