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
            using (var con = new SQLiteConnection("Data Source=Rail_records_system_DB.db"))
            {
                con.Open();

                using (SQLiteCommand command = con.CreateCommand())
                {
                    // 記録テーブルを作成
                    //command.CommandText =
                    //    "create table if not exists 乗車記録" +
                    //    " (乗車記録ID integer primary key autoincrement," +
                    //      "記録タイトル text default '無題'," +
                    //      "乗車駅 text not null," +
                    //      "乗車日時 text default '2000-00-00 00:00'," +
                    //      "降車駅 text not null," +
                    //      "降車日時 text default '2000-00-00 00:00'," +
                    //      "列車名 text default '列車名なし'," +
                    //      "列車番号 text default '-'," +
                    //      "乗車車両ナンバー text default '-'," +
                    //      "乗車路線 text default '-'," +
                    //      "乗車距離 real default '0'," +
                    //      "鉄道会社 text," +
                    //      "鉄道種別 text," +
                    //      "備考 text)";
                    //command.ExecuteNonQuery();

                    // 記録テーブルを作成
                    command.CommandText = "";
                                               
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
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Rail_records_system_DB.db"))
            {
                con.Open();

                using (SQLiteTransaction trans = con.BeginTransaction())
                {
                    SQLiteCommand cmd = con.CreateCommand();

                    // インサート
                    // @をつけることで後述のParametersでセットした値をプログラム実行時に付加してSQLを実行できます
                    cmd.CommandText = "INSERT INTO 乗車記録 (記録タイトル, price) VALUES (@Product, @Price)";

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
            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                // DataTableを生成します。
                var dataTable = new DataTable();

                // SQLの実行
                //var adapter = new SQLiteDataAdapter("SELECT * FROM 乗車記録", con);
                var adapter = new SQLiteDataAdapter("SELECT * FROM 駅情報", con);
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // データ更新
        private void button4_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Rail_records_system_DB.db"))
            {
                con.Open();

                using (SQLiteTransaction trans = con.BeginTransaction())
                {
                    SQLiteCommand cmd = con.CreateCommand();

                    // UPDATE文を作ることもできます
                    // インサート
                    cmd.CommandText = "UPDATE 乗車記録 SET productname = @Product, price = @Price WHERE CD = @Cd";

                    // パラメータセット
                    cmd.Parameters.Add("Product", System.Data.DbType.String);
                    cmd.Parameters.Add("Price", System.Data.DbType.Int64);
                    cmd.Parameters.Add("Cd", System.Data.DbType.Int64);

                    // データ追加
                    cmd.Parameters["Cd"].Value = int.Parse(textBox3.Text);
                    cmd.Parameters["Product"].Value = textBox4.Text;
                    cmd.Parameters["Price"].Value = int.Parse(textBox5.Text);

                    cmd.ExecuteNonQuery();

                    // コミット
                    trans.Commit();
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        // データ検索
        private void button5_Click(object sender, EventArgs e)
        {
            // 検索CDの数字を読み込んで変数に格納
            // その変数を使ってSQLで検索、って　できる　かぁ……？？
            // どうあれそんな感じのことが出来てくれないと困るんだけどさぁ；；；
            // や　出来ないわけはない　検索に必ずといっていいほど必要だし実装してるシステムはいくつもある　絶対出来ないってことは無いんだから

            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                con.Open();

                {
                    SQLiteCommand cmd = con.CreateCommand();

                    // DataTableを生成します。
                    var dataTable = new DataTable();

                    //SqlParameter param = con.CreateParameter();
                    //param.ParameterName = "@name";
                    //param.SqlDbType = SqlDbType.NChar;
                    //param.Direction = ParameterDirection.Input;
                    //param.Value = "Penguin";
                    //com.Parameters.Add(param);

                    // SQLの実行
                    var adapter = new SQLiteDataAdapter("SELECT * FROM t_product WHERE Cd = @Cd", con);

                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }

            /*
             * // UPDATE文を作ることもできます
                    // インサート
                    cmd.CommandText = "UPDATE t_product SET productname = @Product, price = @Price WHERE CD = @Cd";

                    // パラメータセット
                    cmd.Parameters.Add("Product", System.Data.DbType.String);
                    cmd.Parameters.Add("Price", System.Data.DbType.Int64);
                    cmd.Parameters.Add("Cd", System.Data.DbType.Int64);

                    // データ追加
                    cmd.Parameters["Cd"].Value = int.Parse(textBox3.Text);
                    cmd.Parameters["Product"].Value = textBox4.Text;
                    cmd.Parameters["Price"].Value = int.Parse(textBox5.Text);
             */

            /*
            using (SQLiteConnection con = new SQLiteConnection("Data Source = test.db"))
            {
                con.Open();

                SQLiteCommand cmd = con.CreateCommand();

                // DataTableの生成
                var dataTable = new DataTable();

                // SQLの実行
                cmd.Parameters.Add("Cd", System.Data.DbType.Int64);
                cmd.Parameters["Cd"].Value = int.Parse(textBox6.Text);

                var adapter = new SQLiteDataAdapter("SELECT * FROM t_product WHERE Cd = @Cd", con);
                // adapter.Fill(dataTable);
       
                con.Close();

                dataGridView1.DataSource = dataTable;
            }
            */
        }

        // データ削除
        private void button6_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                con.Open();

                using (SQLiteTransaction trans = con.BeginTransaction())
                {
                    SQLiteCommand cmd = con.CreateCommand();

                    // DELETE文を作ることもできます
                    // インサート
                    cmd.CommandText = "DELETE FROM t_product WHERE CD = @Cd";

                    // パラメータセット
                    cmd.Parameters.Add("Cd", System.Data.DbType.Int64);

                    // データ削除
                    cmd.Parameters["Cd"].Value = int.Parse(textBox6.Text);
                    cmd.ExecuteNonQuery();

                    // コミット
                    trans.Commit();
                }
            }
        }

        // テーブル削除
        private void button7_Click(object sender, EventArgs e)
        {
            // コネクションを開いてテーブル削除して閉じる
            using(var con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                con.Open();
                
                using (SQLiteCommand command = con.CreateCommand())
                {
                    // テーブルを削除するときはdrop tableを使います
                    command.CommandText = "drop table 乗車記録";
                    command.ExecuteNonQuery();
                }

                con.Close();
            }
        }
    }
}
