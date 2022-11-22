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
    public partial class W02 : UserControl
    {
        public W02()
        {
            InitializeComponent();
        }

        private void W02_Title_Click(object sender, EventArgs e)
        {

        }

        // 検索画面表示ボタン押下
        // 検索フォームを開く
        private void goW03_Click(object sender, EventArgs e)
        {

        }

        // 戻るボタン押下
        private void gobacktoW01_Click(object sender, EventArgs e)
        {
            Formmain.uc_w02.Visible = false;
            Formmain.uc_w01.Visible = true;
        }
    }
}
