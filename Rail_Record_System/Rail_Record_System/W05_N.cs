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
    public partial class W05_N : Form
    {
        public W05_N()
        {
            InitializeComponent();
        }



        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void W03_title_LB_Click(object sender, EventArgs e)
        {

        }

        private void W05_lines_LB_Click(object sender, EventArgs e)
        {

        }

        private void W05_distance_LB_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        // 閉じるボタン押下
        private void CloseW05_Click(object sender, EventArgs e)
        {
            //フォームを閉じる
            this.Close();
        }

        /*
         * // 検索
            // 検索欄の情報を読み込んで、パラメータを使用してSQLで検索
            // データテーブルを作る
            DataTable search_result = new DataTable();

            // 接続情報を使ってコネクションを生成
            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                SQL_search = "select 乗車記録ID,記録タイトル,列車名,乗車駅,乗車日時,降車駅,降車日時 from 乗車記録";
                whereC = false;

                // 検索
                SQL_Edit();

                // SQL文チェック用
                // DialogResult result = MessageBox.Show(SQL_search, "SQL文確認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                // SQL文とコネクション、パラメータを設定
                using (SQLiteCommand cmd = new SQLiteCommand(SQL_search, con))
                {
                    // パラメータの作成
                    SQLiteParameter s_id = new SQLiteParameter();
                    SQLiteParameter s_title = new SQLiteParameter();
                    SQLiteParameter s_Bsta = new SQLiteParameter();
                    SQLiteParameter s_Btime = new SQLiteParameter();
                    SQLiteParameter s_Esta = new SQLiteParameter();
                    SQLiteParameter s_Etime = new SQLiteParameter();
                    SQLiteParameter s_name = new SQLiteParameter();
                    SQLiteParameter s_Unum = new SQLiteParameter();
                    SQLiteParameter s_Tnum = new SQLiteParameter();
                    SQLiteParameter s_lines = new SQLiteParameter();
                    SQLiteParameter s_dist = new SQLiteParameter();
                    SQLiteParameter s_comp = new SQLiteParameter();
                    SQLiteParameter s_cate = new SQLiteParameter();

                    // パラメータ名の指定
                    s_id.ParameterName = "検索ID";
                    s_title.ParameterName = "検索タイトル";
                    s_Bsta.ParameterName = "検索乗車駅";
                    s_Btime.ParameterName = "検索乗車日時";
                    s_Esta.ParameterName = "検索降車駅";
                    s_Etime.ParameterName = "検索降車日時";
                    s_name.ParameterName = "検索列車名";
                    s_Unum.ParameterName = "検索列車番号";
                    s_Tnum.ParameterName = "検索乗車車両ナンバー";
                    s_lines.ParameterName = "検索乗車路線";
                    s_dist.ParameterName = "検索乗車距離";
                    s_comp.ParameterName = "検索鉄道会社";
                    s_cate.ParameterName = "検索鉄道種別";

                    // パラメータの値を設定　テキストボックスから読み込む
                    s_id.Value = W03_id_TB.Text;
                    s_title.Value = W03_title_TB.Text;
                    s_Bsta.Value = W03_boarding_sta_TB.Text;
                    s_Btime.Value = W03_boarding_time_TB.Text;
                    s_Esta.Value = W03_exit_sta_TB.Text;
                    s_Etime.Value = W03_exit_time_TB.Text;
                    s_name.Value = W03_name_TB.Text;
                    s_Unum.Value = W03_unit_number_TB.Text;
                    s_Tnum.Value = W03_train_number_TB.Text;
                    s_lines.Value = W03_lines_TB.Text;
                    s_dist.Value = W03_distance_TB.Text;
                    s_comp.Value = W03_company_TB.Text;
                    s_cate.Value = W03_category_TB.Text;

                    // パラメータをコマンドに追加
                    cmd.Parameters.Add(s_id);
                    cmd.Parameters.Add(s_title);
                    cmd.Parameters.Add(s_Bsta);
                    cmd.Parameters.Add(s_Btime);
                    cmd.Parameters.Add(s_Esta);
                    cmd.Parameters.Add(s_Etime);
                    cmd.Parameters.Add(s_name);
                    cmd.Parameters.Add(s_Unum);
                    cmd.Parameters.Add(s_Tnum);
                    cmd.Parameters.Add(s_lines);
                    cmd.Parameters.Add(s_dist);
                    cmd.Parameters.Add(s_comp);
                    cmd.Parameters.Add(s_cate);

                    // SQLiteへの橋渡しのアダプターを設定
                    SQLiteDataAdapter sda = new SQLiteDataAdapter();

                    // SELECTコマンドを設定
                    sda.SelectCommand = cmd;

                    // SELECTの実行及びフェッチ
                    sda.Fill(search_result);

                    // dataGridViewに表示
                    W03_DateGridView.DataSource = search_result;
                }
            }
         */

        // 修正ボタン押下
        private void goW06_Click(object sender, EventArgs e)
        {
            MessageBox.Show("修正ボタンが押されたよ！", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 削除ボタン押下
        private void goW07_8_Click(object sender, EventArgs e)
        {
            // 削除確認ダイアログの表示
            DialogResult result = MessageBox.Show
                ("この記録を本当に削除しますか？", "削除確認", MessageBoxButtons.YesNo);

            // 『はい』を選択
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // データ削除
            }
        }
    }
}
