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

        public Formmain()
        {
            InitializeComponent();

            uc_w01 = new W01();

            // パネルに各画面を追加
            panel1.Controls.Add(uc_w01);

            // ユーコンの全画面表示
            uc_w01.Dock = DockStyle.Fill;

            // 最初はコントロール1（メインメニュー画面）のみを見えるようにする
            uc_w01.Visible = true;
        }

        private void Formmain_Load(object sender, EventArgs e)
        {

        }
    }
}
