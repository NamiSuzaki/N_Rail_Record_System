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
    public partial class W03_delete_this: Form
    {
        public W03_delete_this()
        {
            InitializeComponent();
        }

        private void W03_Load(object sender, EventArgs e)
        {

        }

        private void W03_id_TextChanged(object sender, EventArgs e)
        {

        }

        // 閉じるボタン押下
        private void gobacktoW01_Click(object sender, EventArgs e)
        {
            //フォームを閉じる
            this.Close();
        }

        // 検索ボタン押下
        // 入ってるやつと＝のやつをさがす
        private void goW04_Click(object sender, EventArgs e)
        {
            // 検索CDの数字を読み込んで変数に格納
            // その変数を使ってSQLで検索、って　できる　かぁ……？？
            // どうあれそんな感じのことが出来てくれないと困るんだけどさぁ；；；
            // や　出来ないわけはない　検索に必ずといっていいほど必要だし実装してるシステムはいくつもある　絶対出来ないってことは無いんだから

            // できた！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
            
            DataTable dt = new DataTable();

                //接続情報を使ってコネクションを生成
                using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
                {
                    //SQL文とコネクションを設定
                    using (SQLiteCommand cmd = new SQLiteCommand("select * from 乗車記録 where 乗車記録ID = @検索ID", con))
                    {
                        //パラメータの作成
                        SQLiteParameter prmtr1 = new SQLiteParameter();

                        //パラメータ名は@を除いた名前を指定
                        prmtr1.ParameterName = "検索ID";

                        //パラメータの値を設定
                        //prmtr1.Value = "1";
                        prmtr1.Value = W03_id_TB.Text;

                        //パラメータをコマンドに追加
                        cmd.Parameters.Add(prmtr1);

                        //SQLiteへの橋渡しのアダプターを設定
                        SQLiteDataAdapter sda = new SQLiteDataAdapter();

                        //SELECTコマンドを設定
                        sda.SelectCommand = cmd;

                        //SELECTの実行及びフェッチ
                        sda.Fill(dt);

                        //データグリッドビューに表示
                        dataGridView1.DataSource = dt;
                    }
                }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
