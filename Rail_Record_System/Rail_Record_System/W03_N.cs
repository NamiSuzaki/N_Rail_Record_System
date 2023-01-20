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
    public partial class W03_N : Form
    {
        public W05 w05_n = null;

        // 検索の条件文
        // これにnullの所を消したり入ってる所を付けたりしていく
        public string SQL_search = "select 乗車記録ID,記録タイトル,列車名,乗車駅,乗車日時,降車駅,降車日時 from 乗車記録";
        private string SQL_search_where = " where";
        private string SQL_search_and = " and";
        private bool whereC = false;

        /*
         検索仕様（理想）（いったん後回し）
         あいまい：タイトル、列車名、ナンバー、列番、会社
         完全一致：ID、駅、日時、路線、距離、種別
        */

        private string SQL_search_id = " 乗車記録ID Like @検索ID";
        private string SQL_search_title = " 記録タイトル Like @検索タイトル";
        private string SQL_search_Bsta = " 乗車駅 Like @検索乗車駅";
        private string SQL_search_Btime = " 乗車日時 Like @検索乗車日時";
        private string SQL_search_Esta = " 降車駅 Like @検索降車駅";
        private string SQL_search_Etime = " 降車日時 Like @検索降車日時";
        private string SQL_search_name = " 列車名 Like @検索列車名";
        private string SQL_search_Unum = " 列車番号 Like @検索列車番号";
        private string SQL_search_Tnum = " 乗車車両ナンバー Like @検索乗車車両ナンバー";
        private string SQL_search_lines = " 乗車路線 Like @検索乗車路線";
        private string SQL_search_dist = " 乗車距離 Like @検索乗車距離";
        private string SQL_search_comp = " 鉄道会社 Like @検索鉄道会社";
        private string SQL_search_cate = " 鉄道種別 Like @検索鉄道種別";

        // テキストボックスに入ってる検索条件を保存する用のハコ
        private string S_op_id;
        private string S_op_title;
        private string S_op_Bsta;
        private string S_op_Btime;
        private string S_op_Esta;
        private string S_op_Etime;
        private string S_op_name;
        private string S_op_Unum;
        private string S_op_Tnum;
        private string S_op_lines;
        private string S_op_dist;
        private string S_op_comp;
        private string S_op_cate;

        // W05でのID検索用変数
        public static string search_ID;

        public W03_N()
        {
            InitializeComponent();
        }

        // 閉じるボタン押下
        private void gobacktoW01_Click_1(object sender, EventArgs e)
        {
            //フォームを閉じる
            this.Close();
        }

        // 検索ボタン押下
        private void goW04_Click(object sender, EventArgs e)
        {
            // 検索
            // 検索欄の情報を読み込んで、パラメータを使用してSQLで検索
            // データテーブルを作る
            DataTable search_result = new DataTable();

            // 接続情報を使ってコネクションを生成
            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                SQL_search = "select 乗車記録ID,記録タイトル,列車名,乗車駅,乗車日時,降車駅,降車日時 from 乗車記録";
                whereC = false;

                // 検索するSQL文を作成
                SQL_Edit();

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

                    // テキストボックスから検索条件を読み込み保存
                    S_op_id = W03_id_TB.Text;
                    S_op_title = W03_title_TB.Text;
                    S_op_Bsta = W03_boarding_sta_TB.Text;
                    S_op_Btime = W03_boarding_time_TB.Text;
                    S_op_Esta = W03_exit_sta_TB.Text;
                    S_op_Etime = W03_exit_time_TB.Text;
                    S_op_name = W03_name_TB.Text;
                    S_op_Unum = W03_unit_number_TB.Text;
                    S_op_Tnum = W03_train_number_TB.Text;
                    S_op_lines = W03_lines_TB.Text;
                    S_op_dist = W03_distance_TB.Text;
                    S_op_comp = W03_company_TB.Text;
                    S_op_cate = W03_category_TB.Text;

                    // パラメータの値を設定　テキストボックスから読み込む
                    s_id.Value = S_op_id;
                    s_title.Value = S_op_title;
                    s_Bsta.Value = S_op_Bsta;
                    s_Btime.Value = S_op_Btime;
                    s_Esta.Value = S_op_Esta;
                    s_Etime.Value = S_op_Etime;
                    s_name.Value = S_op_name;
                    s_Unum.Value = S_op_Unum;
                    s_Tnum.Value = S_op_Tnum;
                    s_lines.Value = S_op_lines;
                    s_dist.Value = S_op_dist;
                    s_comp.Value = S_op_comp;
                    s_cate.Value = S_op_cate;

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
        }

        // 検索欄に入ってないやつは拾わない　入ってるやつは拾う
        private void SQL_Edit()
        {
            // IDが入ってたとき
            if (!String.IsNullOrEmpty(W03_id_TB.Text))
            {
                SQL_search = SQL_search + SQL_search_where + SQL_search_id;
                whereC = true;
            }

            // タイトルが入ってたとき
            if (!String.IsNullOrEmpty(W03_title_TB.Text))
            {
                // whereが入ってるか入ってないかでSQLの文章が変わるので、whereスイッチでの条件分岐
                switch (whereC)
                {
                    case true:
                        SQL_search = SQL_search + SQL_search_and + SQL_search_title;
                        break;

                    case false:
                        SQL_search = SQL_search + SQL_search_where + SQL_search_title;
                        whereC = true;
                        break;
                }
            }

            // 乗車駅が入ってたとき
            if (!String.IsNullOrEmpty(W03_boarding_sta_TB.Text))
            {
                // whereが入ってるか入ってないかでSQLの文章が変わるので、whereスイッチでの条件分岐
                switch (whereC)
                {
                    case true:
                        SQL_search = SQL_search + SQL_search_and + SQL_search_Bsta;
                        break;

                    case false:
                        SQL_search = SQL_search + SQL_search_where + SQL_search_Bsta;
                        whereC = true;
                        break;
                }
            }

            // 乗車日時が入ってたとき
            if (!String.IsNullOrEmpty(W03_boarding_time_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (whereC)
                {
                    case true:
                        SQL_search = SQL_search + SQL_search_and + SQL_search_Btime;
                        break;

                    case false:
                        SQL_search = SQL_search + SQL_search_where + SQL_search_Btime;
                        whereC = true;
                        break;
                }
            }

            // 降車駅が入ってたとき
            if (!String.IsNullOrEmpty(W03_exit_sta_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (whereC)
                {
                    case true:
                        SQL_search = SQL_search + SQL_search_and + SQL_search_Esta;
                        break;

                    case false:
                        SQL_search = SQL_search + SQL_search_where + SQL_search_Esta;
                        whereC = true;
                        break;
                }
            }

            // 降車日時が入ってたとき
            if (!String.IsNullOrEmpty(W03_exit_time_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (whereC)
                {
                    case true:
                        SQL_search = SQL_search + SQL_search_and + SQL_search_Etime;
                        break;

                    case false:
                        SQL_search = SQL_search + SQL_search_where + SQL_search_Etime;
                        whereC = true;
                        break;
                }
            }

            // 列車名が入ってたとき
            if (!String.IsNullOrEmpty(W03_name_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (whereC)
                {
                    case true:
                        SQL_search = SQL_search + SQL_search_and + SQL_search_name;
                        break;

                    case false:
                        SQL_search = SQL_search + SQL_search_where + SQL_search_name;
                        whereC = true;
                        break;
                }
            }

            // 列車番号が入ってたとき
            if (!String.IsNullOrEmpty(W03_unit_number_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (whereC)
                {
                    case true:
                        SQL_search = SQL_search + SQL_search_and + SQL_search_Unum;
                        break;

                    case false:
                        SQL_search = SQL_search + SQL_search_where + SQL_search_Unum;
                        whereC = true;
                        break;
                }
            }

            // ナンバーが入ってたとき
            if (!String.IsNullOrEmpty(W03_train_number_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (whereC)
                {
                    case true:
                        SQL_search = SQL_search + SQL_search_and + SQL_search_Tnum;
                        break;

                    case false:
                        SQL_search = SQL_search + SQL_search_where + SQL_search_Tnum;
                        whereC = true;
                        break;
                }
            }

            // 乗車路線が入ってたとき
            if (!String.IsNullOrEmpty(W03_lines_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (whereC)
                {
                    case true:
                        SQL_search = SQL_search + SQL_search_and + SQL_search_lines;
                        break;

                    case false:
                        SQL_search = SQL_search + SQL_search_where + SQL_search_lines;
                        whereC = true;
                        break;
                }
            }

            // 乗車距離が入ってたとき
            if (!String.IsNullOrEmpty(W03_distance_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (whereC)
                {
                    case true:
                        SQL_search = SQL_search + SQL_search_and + SQL_search_dist;
                        break;

                    case false:
                        SQL_search = SQL_search + SQL_search_where + SQL_search_dist;
                        whereC = true;
                        break;
                }
            }

            // 会社が入ってたとき
            if (!String.IsNullOrEmpty(W03_company_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (whereC)
                {
                    case true:
                        SQL_search = SQL_search + SQL_search_and + SQL_search_comp;
                        break;

                    case false:
                        SQL_search = SQL_search + SQL_search_where + SQL_search_comp;
                        whereC = true;
                        break;
                }
            }

            // 種別が入ってたとき
            if (!String.IsNullOrEmpty(W03_category_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (whereC)
                {
                    case true:
                        SQL_search = SQL_search + SQL_search_and + SQL_search_cate;
                        break;

                    case false:
                        SQL_search = SQL_search + SQL_search_where + SQL_search_cate;
                        whereC = true;
                        break;
                }
            }
        }

        // DateGridViewの中（セル）をクリックした時、そのデータの詳細ウィンドウを表示する
        // CellContentなので、セルそのものじゃなく『セル内の値』をクリックすると反応する
        private void W03_DateGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 行・列のヘッダーをクリックした場合は流す
            if (e.ColumnIndex == -1 || e.RowIndex == -1)
            {
                return;
            }

            // クリックしたデータのIDの値を取得
            search_ID = $"{W03_DateGridView[0, e.RowIndex].Value}";

            // 二重起動防止　既に開かれていた場合は一度閉じて開き直す
            if (this.w05_n != null)
            {
                // フォームを閉じる
                this.w05_n.Close();
            }
            // フォームを開く
            if (this.w05_n == null || this.w05_n.IsDisposed)
            {
                this.w05_n = new W05();
                w05_n.Show();
            }
        }

        // 最初開いた時の全記録一覧表示
        private void W03_N_Load(object sender, EventArgs e)
        {
            // データテーブルを作る
            DataTable search_result = new DataTable();

            // 接続情報を使ってコネクションを生成
            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                SQL_search = "select 乗車記録ID,記録タイトル,列車名,乗車駅,乗車日時,降車駅,降車日時 from 乗車記録";
                whereC = false;

                // SQL文とコネクション、パラメータを設定
                using (SQLiteCommand cmd = new SQLiteCommand(SQL_search, con))
                {
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
        }

        // 閉じるボタン押下
        private void gobacktoW01_Click(object sender, EventArgs e)
        {
            //フォームを閉じる
            this.Close();
        }

        // クリアボタン押下
        private void W03_clear_Click(object sender, EventArgs e)
        {
            // 全検索条件の入力内容をクリア
            W03_id_TB.Text = "";
            W03_title_TB.Text = "";
            W03_boarding_sta_TB.Text = "";
            W03_boarding_time_TB.Text = "";
            W03_exit_sta_TB.Text = "";
            W03_exit_time_TB.Text = "";
            W03_name_TB.Text = "";
            W03_unit_number_TB.Text = "";
            W03_train_number_TB.Text = "";
            W03_lines_TB.Text = "";
            W03_train_number_TB.Text = "";
            W03_distance_TB.Text = "";
            W03_company_TB.Text = "";
            W03_category_TB.Text = "";
        }

        // 更新ボタン押下
        private void W03_upd_Click(object sender, EventArgs e)
        {
            // さっき使った検索条件文を使って再び検索

            // 検索
            // 検索欄の情報を読み込んで、パラメータを使用してSQLで検索
            // データテーブルを作る
            DataTable search_result = new DataTable();

            // 接続情報を使ってコネクションを生成
            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                // これ検索した時にいっかい検索条件保存しておいて、それを更新時もっかい読み直す感じにせんといかんな

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

                    // パラメータの値を設定　最直近の検索条件を読み込んで入れる
                    s_id.Value = S_op_id;
                    s_title.Value = S_op_title;
                    s_Bsta.Value = S_op_Bsta;
                    s_Btime.Value = S_op_Btime;
                    s_Esta.Value = S_op_Esta;
                    s_Etime.Value = S_op_Etime;
                    s_name.Value = S_op_name;
                    s_Unum.Value = S_op_Unum;
                    s_Tnum.Value = S_op_Tnum;
                    s_lines.Value = S_op_lines;
                    s_dist.Value = S_op_dist;
                    s_comp.Value = S_op_comp;
                    s_cate.Value = S_op_cate;

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
        }
    }
}
