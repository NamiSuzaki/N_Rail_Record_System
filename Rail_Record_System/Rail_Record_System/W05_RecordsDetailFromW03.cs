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
    public partial class W05_RecordsDetailFromW03 : Form
    {
        public W05_RecordsDetailFromW03()
        {
            InitializeComponent();
        }

        public W06_RecordsUpdate w06 = null;

        // IDをW03_Nから受け取るためのハコ
        private string ToGetID;

        // IDをW06に受渡すためのハコ
        public static string IDW06_W05;

        // CanW06を読み取るためのハコ
        private bool CanOpenW06_05;

        // W05からW06を開いたことを伝えるためのやつ
        public static bool FW05;

        // 起動時
        private void FormW05_Load_1(object sender, EventArgs e)
        {
            // ここで検索するためのIDをW03_Nから受け取る
            ToGetID = W03_N.search_ID;

            // 検索
            Search_OnlyOne();
        }

        private void Search_OnlyOne()
        {
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

        // 閉じるボタン押下
        private void CloseW05_Click(object sender, EventArgs e)
        {
            //フォームを閉じる
            this.Close();
        }

        // 修正ボタン押下
        private void goW06_Click(object sender, EventArgs e)
        {
            CanOpenW06_05 = W06_RecordsUpdate.CanW06;

            // ちょっと分かりづらいんだけど、変数がfalse,nullの時はW06を開く
            // trueの時は開かない
            if(CanOpenW06_05 != true)
            {
                FW05 = true;
                IDW06_W05 = ToGetID;

                // 二重起動防止　既に開かれている場合は開かない
                // フォームを開く
                if (this.w06 == null || this.w06.IsDisposed)
                {
                    this.w06 = new W06_RecordsUpdate();
                    w06.Show();
                }
            }
            else
            {
                // ダイアログの表示
                DialogResult result = MessageBox.Show
                    ("既に別ウィンドウで修正画面が開かれています。" +
                    "現在開かれている修正画面を閉じた後、再度開き直してください。", "アプリケーション", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 削除ボタン押下
        private void goW07_8_Click(object sender, EventArgs e)
        {
            // 削除確認ダイアログの表示
            DialogResult result = MessageBox.Show
                ("この記録を本当に削除しますか？", "削除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            // 『はい』を選択
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                DeleteR();
                MessageBox.Show("記録ID：" + ToGetID + "は削除されました", "削除", MessageBoxButtons.OK);
            }
        }

        private void DeleteR()
        {
            // データ削除
            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                con.Open();

                using (SQLiteTransaction trans = con.BeginTransaction())
                {
                    SQLiteCommand cmd = con.CreateCommand();

                    cmd.CommandText = "delete from 乗車記録 where 乗車記録ID Like @検索ID";

                    // パラメータの作成
                    SQLiteParameter s_id = new SQLiteParameter();

                    // パラメータ名の指定
                    s_id.ParameterName = "検索ID";

                    // パラメータの値を設定（変数を読み込む）
                    s_id.Value = ToGetID;

                    // パラメータをコマンドに追加
                    cmd.Parameters.Add(s_id);

                    // データ削除
                    cmd.ExecuteNonQuery();

                    // コミット
                    trans.Commit();
                }

                //フォームを閉じる
                this.Close();

                // もし修正画面開いてたら閉じる
                if (this.w06 == null || this.w06.IsDisposed)
                {
                    return;
                }
                else
                {
                    // 修正フォームを閉じる
                    this.w06.Close();
                }

                
            }
        }
    }
}