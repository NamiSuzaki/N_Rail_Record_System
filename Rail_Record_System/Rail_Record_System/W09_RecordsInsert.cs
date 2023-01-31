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
    public partial class W09_RecordsInsert : Form
    {
        public W11_RecordsDetailFromW09 w11 = null;

        // W05フォームを使って登録直後の詳細画面を出すためのID用ハコ
        public static string lastID;

        public W09_RecordsInsert()
        {
            InitializeComponent();
        }

        // 乗車記録一件の新規登録
        private void W09_register_Click(object sender, EventArgs e)
        {
            // 登録処理
            // 駅名登録について、外部キー制約をON　駅情報マスタにある駅名以外弾く
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Rail_records_system_DB.db;Foreign Keys=True"))
            {
                // DBを開く
                con.Open();

                using (SQLiteTransaction trans = con.BeginTransaction())
                {
                    SQLiteCommand cmd = con.CreateCommand();

                    // インサート
                    // @をつけることで後述のParametersでセットした値をプログラム実行時に付加してSQLを実行できる
                    cmd.CommandText =
                            "INSERT INTO 乗車記録 " +
                            "(記録タイトル,乗車駅,乗車日時,降車駅,降車日時,列車名,列車番号,乗車車両ナンバー,乗車路線,乗車距離,鉄道会社,鉄道種別,備考)" +
                            " VALUES (@記録タイトル,@乗車駅,@乗車日時,@降車駅,@降車日時,@列車名,@列車番号,@乗車車両ナンバー,@乗車路線," +
                            "@乗車距離,@鉄道会社,@鉄道種別,@備考)";

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
                        DialogResult result = MessageBox.Show("乗車駅・降車駅を入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    // 乗車駅・降車駅が入ってたらとりあえず登録
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
                        // SQLで外部キー制約に引っかかるか否かで処理を変えるため、try/catch
                        try
                        {
                            // 外部キー制約に引っかかっていない時
                            cmd.ExecuteNonQuery();

                            // コミット　trans.commit();でDBの変更を確定
                            trans.Commit();

                            // 登録完了
                            // ここからはさっき登録した記録のIDを拾う
                            W09_W11_open();

                            // 登録したデータの詳細を表示する
                            // 二重起動防止　既に開かれていた場合は一度閉じて開き直す
                            if (this.w11 != null)
                            {
                                // フォームを閉じる
                                this.w11.Close();
                            }
                            // フォームを開く
                            if (this.w11 == null || this.w11.IsDisposed)
                            {
                                this.w11 = new W11_RecordsDetailFromW09();
                                w11.Show();
                            }
                        }
                        catch(System.Data.SQLite.SQLiteException)
                        {
                            // 外部キー制約に引っかかった時
                            DialogResult result = MessageBox.Show("乗車駅/降車駅の駅名は、右記一覧表から選択して入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
        }

        // さっき登録したばかりの記録のIDを拾うやつ
        private void W09_W11_open()
        {
            // IDが一番大きいものを探す
            string SQL_search2 = "select max(乗車記録ID) from 乗車記録";

            // データテーブルを作る
            DataTable search_result = new DataTable();

            // 接続情報を使ってコネクションを生成
            using (SQLiteConnection con2 = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                // SQL文とコネクション、パラメータを設定
                using (SQLiteCommand cmd = new SQLiteCommand(SQL_search2, con2))
                {
                    // SQLiteへの橋渡しのアダプターを設定
                    SQLiteDataAdapter sda = new SQLiteDataAdapter();

                    // SELECTコマンドを設定
                    sda.SelectCommand = cmd;

                    // SELECTの実行及びフェッチ
                    sda.Fill(search_result);

                    // ID最大値を変数として保存
                    lastID = search_result.Rows[0][0].ToString();
                }
            }
        }

        // 閉じるボタン押下
        private void W09_close_Click(object sender, EventArgs e)
        {
            //画面を閉じる
            this.Close();
        }

        // 新規登録フォームを開いた時
        private void W09_RecordsInsert_Load(object sender, EventArgs e)
        {
            // W09_regist_stations_DataGridViewに駅情報マスタの駅名を表示する
            // 無理だったら考えるけど、『乗車駅』『降車駅』欄を選択してる状態で↑の表の駅名クリックしたらその駅名が自動でその欄に入る、とかにしたい　絶対便利なので

            //W09_regist_stations_DataGridView.ColumnHeadersVisible = false;

            DataTable searchResult = new DataTable();

            // 接続情報を使ってコネクションを生成
            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_Records_System_DB.db"))
            {
                string sqlStationsView = "SELECT 駅ID,駅名 FROM 駅情報";

                using (SQLiteCommand cmd = new SQLiteCommand(sqlStationsView, con))
                {
                    // SQLiteへの橋渡しのアダプターを設定
                    SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter();

                    // SELECTコマンドを設定
                    sqlDataAdapter.SelectCommand = cmd;

                    // SELECTの実行及びフェッチ
                    sqlDataAdapter.Fill(searchResult);

                    // dataGridViewに表示
                    W09_regist_stations_DataGridView.DataSource = searchResult;

                    // 列の幅と行の高さを自動調整
                    W09_regist_stations_DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    W09_regist_stations_DataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                }
            }
        }
    }
}
