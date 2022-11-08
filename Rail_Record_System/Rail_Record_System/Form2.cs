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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        // テーブル作成
        private void button1_Click(object sender, EventArgs e)
        {
            // コネクションを開いてテーブル作成して閉じる
            // Data Source=で指定した名前がDBのファイル名になります
            using (var con = new SQLiteConnection("Data Source=test.db"))
            {
                con.Open();

                using (SQLiteCommand command = con.CreateCommand())
                {
                    // t_productテーブルを作成します
                    command.CommandText =
                        "Create table t_product (CD INTEGER  PRIMARY KEY AUTOINCREMENT, productname TEXT, price INTEGER)";
                    command.ExecuteNonQuery();
                }

                con.Close();

                // con.Open();でDBを接続しcon.Close();でDB接続を切断します
                // 使い終わったら必ず接続を切断しましょう
                // command.CommandTextには実行するSQL文を入れます
                // Command.ExecuteNonQuery(); でSQLを実行します
            }
        }

        // データ追加
        private void button2_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=test.db"))
            {
                con.Open();

                using (SQLiteTransaction trans = con.BeginTransaction())
                {
                    SQLiteCommand cmd = con.CreateCommand();

                    // インサート
                    // @をつけることで後述のParametersでセットした値をプログラム実行時に付加してSQLを実行できます
                    cmd.CommandText = "INSERT INTO t_product (productname, price) VALUES (@Product, @Price)";

                    // パラメータセット
                    // ParametersにProductとPriceを追加します
                    cmd.Parameters.Add("Product", System.Data.DbType.String);
                    cmd.Parameters.Add("Price", System.Data.DbType.Int64);

                    // データ追加
                    // textBoxに入力されている文字列をParametersに設定します
                    // textBox1とtextBox2は追加したテキストボックスの名称です　デザイナー画面のプロパティから確認できます
                    cmd.Parameters["Product"].Value = textBox1.Text;
                    cmd.Parameters["Price"].Value = int.Parse(textBox2.Text);

                    cmd.ExecuteNonQuery();

                    // コミット
                    // trans.commit();でDBの変更を確定します
                    trans.Commit();

                    // テキストボックスにデータを入力してデータ追加ボタンを押すとDBにデータが登録されます
                }
            }
        }

        // データ読み込み
        private void button3_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source = test.db"))
            {
                // DataTableを生成します。
                var dataTable = new DataTable();

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
