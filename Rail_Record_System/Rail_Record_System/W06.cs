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
    public partial class W06 : Form
    {
        public W06()
        {
            InitializeComponent();
        }

        // 二重起動を防ぐためのフラグ
        public static bool CanW06;

        // W05とW11のどっちから開いたのかを判別するためのやつ
        private bool FromW05;
        private bool FromW11;

        // IDを受け取る為のハコ
        private string ReadW06ID;

        private void W06_Load(object sender, EventArgs e)
        {
            // 既に開いているのでtrueにする
            CanW06 = true;

            // それぞれのフラグを読む
            // フラグのON/OFFに応じて処理を変える
            FromW05 = W05.FW05;
            FromW11 = W11.FW11;

            // W05から開いた時
            if(FromW05 == true && FromW11 != true)
            {
                // IDを読む
                ReadW06ID = W05.IDW06_W05;
            }

            // W11から開いた時
            if(FromW11 == true && FromW05 != true)
            {
                // IDを読む
                ReadW06ID = W11.IDW06_W11;
            }

            MessageBox.Show(ReadW06ID);

            // 検索
            Search_ToUpd();

            FromW05 = false;
            FromW11 = false;
            W05.FW05 = FromW05;
            W11.FW11 = FromW11;

            /*
                ・フラグで同時に修正画面を開かないように管理する
                W06にpublicな変数aiを用意して、それをW11でもW05でもまず開く前に読むようにする

                nullもしくはfalseなら→普通に開く処理
                trueなら→開かない　『別ウィンドウで既に修正画面が開かれています』のダイアログ表示

                『閉じる』ボタン、もしくは『右上のバツ印』で画面を閉じたとき、W06上で変数をfalseへ直す
            */
        }

        // 更新ボタン押下で更新
        private void W09_register_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Rail_records_system_DB.db"))
            {
                con.Open();

                using (SQLiteTransaction trans = con.BeginTransaction())
                {
                    SQLiteCommand cmd = con.CreateCommand();

                    cmd.CommandText = "update 乗車記録 set productname = @Product, price = @Price where 乗車記録ID = @検索ID";

                    // テキストボックスたちの名前変更する
                    // 更新処理のやつぱっぱっとやる

                    // パラメータセット
                    // Parametersに各項目を追加
                    cmd.Parameters.Add("記録タイトル", System.Data.DbType.String);
                    cmd.Parameters.Add("乗車駅", System.Data.DbType.String);
                    cmd.Parameters.Add("乗車日時", System.Data.DbType.String);
                    cmd.Parameters.Add("降車駅", System.Data.DbType.String);
                    cmd.Parameters.Add("降車日時", System.Data.DbType.String);
                    cmd.Parameters.Add("列車名", System.Data.DbType.String);
                    cmd.Parameters.Add("列車番号", System.Data.DbType.String);
                    cmd.Parameters.Add("乗車車両ナンバー", System.Data.DbType.String);
                    cmd.Parameters.Add("乗車路線", System.Data.DbType.String);
                    cmd.Parameters.Add("乗車距離", System.Data.DbType.String);
                    cmd.Parameters.Add("鉄道会社", System.Data.DbType.String);
                    cmd.Parameters.Add("鉄道種別", System.Data.DbType.String);
                    cmd.Parameters.Add("備考", System.Data.DbType.String);

                    // データ追加
                    // textBoxに入力されている文字列をParametersに設定
                    cmd.Parameters["記録タイトル"].Value = W09_title_TB.Text;
                    cmd.Parameters["乗車駅"].Value = W09_boarding_sta_TB.Text;
                    cmd.Parameters["乗車日時"].Value = W09_boarding_time_TB.Text;
                    cmd.Parameters["降車駅"].Value = W09_exit_sta_TB.Text;
                    cmd.Parameters["降車日時"].Value = W09_exit_time_TB.Text;
                    cmd.Parameters["列車名"].Value = W09_name_TB.Text;
                    cmd.Parameters["列車番号"].Value = W09_unit_number_TB.Text;
                    cmd.Parameters["乗車車両ナンバー"].Value = W09_train_number_TB.Text;
                    cmd.Parameters["乗車路線"].Value = W09_lines_TB.Text;
                    cmd.Parameters["乗車距離"].Value = W09_distance_TB.Text;
                    cmd.Parameters["鉄道会社"].Value = W09_company_TB.Text;
                    cmd.Parameters["鉄道種別"].Value = W09_category_TB.Text;
                    cmd.Parameters["備考"].Value = W09_note_TB.Text;

                    // 最低限　乗車駅・降車駅が入ってなかったら登録を弾く
                    if (String.IsNullOrEmpty(W09_boarding_sta_TB.Text) || String.IsNullOrEmpty(W09_exit_sta_TB.Text))
                    {
                        // 登録時エラーポップアップの表示
                        DialogResult result = MessageBox.Show("乗車駅・降車駅を入力してください", "登録内容エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    // 乗車駅・降車駅が入ってたら更新
                    else
                    {
                        // 未入力の所の登録内容を置き換える

                        // ・記録タイトル「無題」
                        if (String.IsNullOrEmpty(W09_title_TB.Text))
                        {
                            cmd.Parameters["記録タイトル"].Value = "無題";
                        }

                        // ・乗車時刻「2000-00-00 00:00」
                        if (String.IsNullOrEmpty(W09_boarding_time_TB.Text))
                        {
                            cmd.Parameters["乗車日時"].Value = "2000-00-00 00:00";
                        }

                        // ・降車時刻「2000-00-00 00:00」
                        if (String.IsNullOrEmpty(W09_exit_time_TB.Text))
                        {
                            cmd.Parameters["降車日時"].Value = "2000-00-00 00:00";
                        }

                        // ・乗車路線「-」
                        if (String.IsNullOrEmpty(W09_lines_TB.Text))
                        {
                            cmd.Parameters["乗車路線"].Value = "-";
                        }

                        // ・乗車距離「0」
                        if (String.IsNullOrEmpty(W09_distance_TB.Text))
                        {
                            cmd.Parameters["乗車距離"].Value = 0;
                        }

                        // ・列車名「列車名なし」
                        if (String.IsNullOrEmpty(W09_name_TB.Text))
                        {
                            cmd.Parameters["列車名"].Value = "列車名なし";
                        }

                        // ・乗車車両ナンバー「-」
                        if (String.IsNullOrEmpty(W09_train_number_TB.Text))
                        {
                            cmd.Parameters["乗車車両ナンバー"].Value = "-";
                        }

                        // ・列車番号「-」
                        if (String.IsNullOrEmpty(W09_unit_number_TB.Text))
                        {
                            cmd.Parameters["列車番号"].Value = "-";
                        }

                        // ・鉄道会社「-」
                        if (String.IsNullOrEmpty(W09_company_TB.Text))
                        {
                            cmd.Parameters["鉄道会社"].Value = "-";
                        }

                        // ・鉄道種別「-」
                        if (String.IsNullOrEmpty(W09_category_TB.Text))
                        {
                            cmd.Parameters["鉄道種別"].Value = "-";
                        }

                        // 実行
                        /*
                        //タイトル
                        W09_title_TB.Text = D_result.Rows[0][1].ToString();
                        //乗車駅
                        W09_boarding_sta_TB.Text = D_result.Rows[0][2].ToString();
                        //乗車日時
                        W09_boarding_time_TB.Text = D_result.Rows[0][4].ToString();
                        //降車駅
                        W09_exit_sta_TB.Text = D_result.Rows[0][3].ToString();
                        //降車日時
                        W09_exit_time_TB.Text = D_result.Rows[0][5].ToString();
                        //列車名
                        W09_name_TB.Text = D_result.Rows[0][8].ToString();
                        //列車番号
                        W09_unit_number_TB.Text = D_result.Rows[0][10].ToString();
                        //乗車路線
                        W09_lines_TB.Text = D_result.Rows[0][6].ToString();
                        //乗車車両ナンバー
                        W09_train_number_TB.Text = D_result.Rows[0][9].ToString();
                        //乗車距離
                        W09_distance_TB.Text = D_result.Rows[0][7].ToString();
                        //鉄道会社
                        W09_company_TB.Text = D_result.Rows[0][11].ToString();
                        //鉄道種別
                        W09_category_TB.Text = D_result.Rows[0][12].ToString();
                        //備考
                        W09_note_TB.Text = D_result.Rows[0][13].ToString();
                         */

                        // 実行
                        cmd.ExecuteNonQuery();

                        // コミット
                        trans.Commit();
                    }
                }
            }
        }

        // 画面のテキストボックスたちにレコードを読ませて入れていく
        // 読んだIDを使って検索
        private void Search_ToUpd()
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
                    s_id.Value = ReadW06ID;

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
                    //タイトル
                    W09_title_TB.Text = D_result.Rows[0][1].ToString();
                    //乗車駅
                    W09_boarding_sta_TB.Text = D_result.Rows[0][2].ToString();
                    //乗車日時
                    W09_boarding_time_TB.Text = D_result.Rows[0][4].ToString();
                    //降車駅
                    W09_exit_sta_TB.Text = D_result.Rows[0][3].ToString();
                    //降車日時
                    W09_exit_time_TB.Text = D_result.Rows[0][5].ToString();
                    //列車名
                    W09_name_TB.Text = D_result.Rows[0][8].ToString();
                    //列車番号
                    W09_unit_number_TB.Text = D_result.Rows[0][10].ToString();
                    //乗車路線
                    W09_lines_TB.Text = D_result.Rows[0][6].ToString();
                    //乗車車両ナンバー
                    W09_train_number_TB.Text = D_result.Rows[0][9].ToString();
                    //乗車距離
                    W09_distance_TB.Text = D_result.Rows[0][7].ToString();
                    //鉄道会社
                    W09_company_TB.Text = D_result.Rows[0][11].ToString();
                    //鉄道種別
                    W09_category_TB.Text = D_result.Rows[0][12].ToString();
                    //備考
                    W09_note_TB.Text = D_result.Rows[0][13].ToString();
                }
            }
        }

        // フォームW06が閉じられたらCanW06をfalseにする　これでW06が開けるようになる
        private void W06_FormClosing(object sender, FormClosingEventArgs e)
        {
            CanW06 = false;
        }

        // 閉じるボタン押下
        private void W06_close_Click(object sender, EventArgs e)
        {
            //フォームを閉じる
            this.Close();
        }
    }
}
