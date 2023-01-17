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
    public partial class FormW05 : Form
    {
        public FormW05()
        {
            InitializeComponent();
        }

        // IDをW03_Nから受け取るためのハコ
        private static string ToGetID;

        // 起動時
        private void FormW05_Load_1(object sender, EventArgs e)
        {
            // ここで検索するためのIDをW03_Nから受け取る
            ToGetID = W03_N.search_ID;

            // 検索
            // ↑のIDをパラメータに入れ込んでSQLで検索
            // データテーブルを作る
            DataTable search_result = new DataTable();

            // 接続情報を使ってコネクションを生成
            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                string SQL_search = "select * from 乗車記録 where 乗車記録ID Like @検索ID";

                // SQL文とコネクション、パラメータを設定
                using (SQLiteCommand cmd = new SQLiteCommand(SQL_search, con))
                {
                    // パラメータの作成
                    SQLiteParameter s_id = new SQLiteParameter();

                    // パラメータ名の指定
                    s_id.ParameterName = "検索ID";

                    // パラメータの値を設定　テキストボックスから読み込む
                    s_id.Value = ToGetID;

                    // パラメータをコマンドに追加
                    cmd.Parameters.Add(s_id);

                    // SQLiteへの橋渡しのアダプターを設定
                    SQLiteDataAdapter sda = new SQLiteDataAdapter();

                    // SELECTコマンドを設定
                    sda.SelectCommand = cmd;

                    // SELECTの実行及びフェッチ
                    sda.Fill(search_result);

                    var D_result = search_result;

                    // 結果をそれぞれのLabelに表示していく
                    // ID
                    W05_id_D.Text = D_result.Rows[0][0].ToString();
                    //タイトル
                    W05_title_D.Text = D_result.Rows[0][1].ToString();
                    //乗車駅
                    W05_boarding_sta_D.Text = D_result.Rows[0][2].ToString();
                    //乗車日時
                    W05_boarding_time_D.Text = D_result.Rows[0][4].ToString();
                    //降車駅
                    W05_exit_sta_D.Text = D_result.Rows[0][3].ToString();
                    //降車日時
                    W05_exit_time_D.Text = D_result.Rows[0][5].ToString();
                    //列車名
                    W05_name_D.Text = D_result.Rows[0][8].ToString();
                    //列車番号
                    W05_unit_number_D.Text = D_result.Rows[0][10].ToString();
                    //乗車路線
                    W05_lines_D.Text = D_result.Rows[0][6].ToString();
                    //乗車車両ナンバー
                    W05_train_number_D.Text = D_result.Rows[0][9].ToString();
                    //乗車距離
                    W05_distance_D.Text = D_result.Rows[0][7].ToString();
                    //鉄道会社
                    W05_company_D.Text = D_result.Rows[0][11].ToString();
                    //鉄道種別
                    W05_category_D.Text = D_result.Rows[0][12].ToString();
                    //備考
                    W05_note_D.Text = D_result.Rows[0][13].ToString();
                }
            }
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

        private void W05_ID_dayo_Click(object sender, EventArgs e)
        {

        }
    }
}
