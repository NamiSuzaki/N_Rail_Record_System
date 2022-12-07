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
    public partial class W02 : UserControl
    {
        public W02()
        {
            InitializeComponent();
        }

        private void W02_Title_Click(object sender, EventArgs e)
        {

        }

        // 一番最初　コントロールを読み込んだ時
        // 記録一覧全件表示
        private void W02_Load(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                // DataTableを生成します。
                var dataTable = new DataTable();

                // SQLの実行
                var adapter = new SQLiteDataAdapter("SELECT 乗車記録ID,記録タイトル,列車名,乗車駅,乗車日時,降車駅,降車日時 FROM 乗車記録", con);
                // select id, name from user;
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        // 検索画面表示ボタン押下
        // 検索フォームを開く
        private void goW03_Click(object sender, EventArgs e)
        {
            W03_delete_this w03 = new W03_delete_this();
            w03.Show();
        }

        // 戻るボタン押下
        private void gobacktoW01_Click(object sender, EventArgs e)
        {
            Formmain.uc_w02.Visible = false;
            Formmain.uc_w01.Visible = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // 更新ボタン押下、データ読み込み
        private void button1_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                // DataTableを生成します。
                var dataTable = new DataTable();

                // SQLの実行
                var adapter = new SQLiteDataAdapter("SELECT 乗車記録ID,記録タイトル,列車名,乗車駅,乗車日時,降車駅,降車日時 FROM 乗車記録", con);
                // select id, name from user;
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
    }
}
