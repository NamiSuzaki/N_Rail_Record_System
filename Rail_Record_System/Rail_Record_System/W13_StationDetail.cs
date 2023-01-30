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
    public partial class W13_StationDetail : Form
    {
        public W13_StationDetail()
        {
            InitializeComponent();
        }

        // 駅名をW03から受け取るためのハコ
        private string ToGetSta;

        // 起動時
        private void W13_StationDetail_Load(object sender, EventArgs e)
        {
            // ここで検索するための駅名をW03から受け取る
            ToGetSta = W03_RecordsList.detail_STA;

            //MessageBox.Show(ToGetSta);

            // 検索
            Search_Station();
        }

        private void Search_Station()
        {
            // ↑のIDをパラメータに入れ込んでSQLで検索
            // データテーブルを作る
            DataTable search_result = new DataTable();

            // 接続情報を使ってコネクションを生成
            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                string SQL_search = "select * from 駅情報 where 駅名 Like @検索駅名";

                // SQL文とコネクション、パラメータを設定
                using (SQLiteCommand cmd = new SQLiteCommand(SQL_search, con))
                {
                    // パラメータの作成
                    SQLiteParameter sta_name = new SQLiteParameter();

                    // パラメータ名の指定
                    sta_name.ParameterName = "検索駅名";

                    // パラメータの値を設定　運んできた駅名を入れる
                    sta_name.Value = ToGetSta;

                    // パラメータをコマンドに追加
                    cmd.Parameters.Add(sta_name);

                    // SQLiteへの橋渡しのアダプターを設定
                    SQLiteDataAdapter sda = new SQLiteDataAdapter();

                    // SELECTコマンドを設定
                    sda.SelectCommand = cmd;

                    // SELECTの実行及びフェッチ
                    sda.Fill(search_result);

                    var D_result = search_result;

                    // 結果をそれぞれのLabelに表示していく
                    // 駅ID
                    W13_sta_id_D.Text = D_result.Rows[0][0].ToString();
                    // 駅名
                    W13_sta_name_D.Text = D_result.Rows[0][1].ToString();
                    // 駅ナンバリング
                    W13_sta_number_D.Text = D_result.Rows[0][2].ToString();
                    // 利用回数（死にステ…！）
                    W13_sta_count_D.Text = D_result.Rows[0][3].ToString();
                    // 所属路線
                    W13_sta_line_D.Text = D_result.Rows[0][4].ToString();
                    // 鉄道会社
                    W13_sta_company_D.Text = D_result.Rows[0][5].ToString();
                    // 鉄道種別
                    W13_sta_category_D.Text = D_result.Rows[0][6].ToString();
                }
            }
        }

        // 閉じるボタン押下
        private void CloseW13_Click(object sender, EventArgs e)
        {
            //フォームを閉じる
            this.Close();
        }
    }
}
