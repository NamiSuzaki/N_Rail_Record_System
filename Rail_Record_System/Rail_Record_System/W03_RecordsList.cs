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
    // W03 鉄道乗車記録システム｜乗車記録検索
    // 記録一覧の表示と検索の画面
    public partial class W03_N : Form
    {
        // W05を開く時の重複表示を防ぐ用変数
        private W05_RecordsDetailFromW03 _w05 = null;

        /*
         検索仕様（理想）（いったん後回し）
         あいまい：タイトル、列車名、ナンバー、列番、会社
         完全一致：ID、駅、日時、路線、距離、種別
        */

        // 検索の条件文を作る材料
        // これにnullの所を消したり入ってる所を付けたりしていく
        private string _sqlSearch = "SELECT 乗車記録ID,記録タイトル,列車名,乗車駅,乗車日時,降車駅,降車日時 FROM 乗車記録";
        private const string SQL_WHERE = " WHERE";
        private const string SQL_AND = " AND";
        private bool _containsWhere = false;

        private const string SQL_ID = " 乗車記録ID LIKE @検索ID";
        private const string SQL_TITLE = " 記録タイトル LIKE @検索タイトル";
        private const string SQL_BSTA = " 乗車駅 LIKE @検索乗車駅";
        private const string SQL_BTIME = " 乗車日時 LIKE @検索乗車日時";
        private const string SQL_ESTA = " 降車駅 LIKE @検索降車駅";
        private const string SQL_ETIME = " 降車日時 LIKE @検索降車日時";
        private const string SQL_NAME = " 列車名 LIKE @検索列車名";
        private const string SQL_UNUM = " 列車番号 LIKE @検索列車番号";
        private const string SQL_TNUM = " 乗車車両ナンバー LIKE @検索乗車車両ナンバー";
        private const string SQL_LINES = " 乗車路線 LIKE @検索乗車路線";
        private const string SQL_DIST = " 乗車距離 LIKE @検索乗車距離";
        private const string SQL_COMP = " 鉄道会社 LIKE @検索鉄道会社";
        private const string SQL_CATE = " 鉄道種別 LIKE @検索鉄道種別";

        // テキストボックスに入ってる検索条件を保存する用のハコ（DataGridViewの表示内容を更新する時に使う）
        private string _sop_id;
        private string _sop_title;
        private string _sop_Bsta;
        private string _sop_Btime;
        private string _sop_Esta;
        private string _sop_Etime;
        private string _sop_name;
        private string _sop_Unum;
        private string _sop_Tnum;
        private string _sop_lines;
        private string _sop_dist;
        private string _sop_comp;
        private string _sop_cate;

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

        // さまりー

        // 検索ボタン押下
        private void goW04_Click(object sender, EventArgs e)
        {
            // 検索
            // 検索欄の情報を読み込んで、パラメータを使用してSQLで検索
            // データテーブルを作る
            DataTable searchResult = new DataTable();

            // 接続情報を使ってコネクションを生成
            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                _sqlSearch = "select 乗車記録ID,記録タイトル,列車名,乗車駅,乗車日時,降車駅,降車日時 from 乗車記録";
                _containsWhere = false;

                // 検索するSQL文を作成
                SQL_Edit();

                // SQL文とコネクション、パラメータを設定
                using (SQLiteCommand cmd = new SQLiteCommand(_sqlSearch, con))
                {
                    // パラメータの作成
                    SQLiteParameter searchId = new SQLiteParameter();
                    SQLiteParameter searchTitle = new SQLiteParameter();
                    SQLiteParameter searchBsta = new SQLiteParameter();
                    SQLiteParameter searchBtime = new SQLiteParameter();
                    SQLiteParameter searchEsta = new SQLiteParameter();
                    SQLiteParameter searchEtime = new SQLiteParameter();
                    SQLiteParameter searchName = new SQLiteParameter();
                    SQLiteParameter searchUnum = new SQLiteParameter();
                    SQLiteParameter searchTnum = new SQLiteParameter();
                    SQLiteParameter searchLines = new SQLiteParameter();
                    SQLiteParameter searchDist = new SQLiteParameter();
                    SQLiteParameter searchComp = new SQLiteParameter();
                    SQLiteParameter searchCate = new SQLiteParameter();

                    // パラメータ名の指定
                    searchId.ParameterName = "検索ID";
                    searchTitle.ParameterName = "検索タイトル";
                    searchBsta.ParameterName = "検索乗車駅";
                    searchBtime.ParameterName = "検索乗車日時";
                    searchEsta.ParameterName = "検索降車駅";
                    searchEtime.ParameterName = "検索降車日時";
                    searchName.ParameterName = "検索列車名";
                    searchUnum.ParameterName = "検索列車番号";
                    searchTnum.ParameterName = "検索乗車車両ナンバー";
                    searchLines.ParameterName = "検索乗車路線";
                    searchDist.ParameterName = "検索乗車距離";
                    searchComp.ParameterName = "検索鉄道会社";
                    searchCate.ParameterName = "検索鉄道種別";

                    // テキストボックスから検索条件を読み込み保存
                    _sop_id = W03_id_TB.Text;
                    _sop_title = W03_title_TB.Text;
                    _sop_Bsta = W03_boarding_sta_TB.Text;
                    _sop_Btime = W03_boarding_time_TB.Text;
                    _sop_Esta = W03_exit_sta_TB.Text;
                    _sop_Etime = W03_exit_time_TB.Text;
                    _sop_name = W03_name_TB.Text;
                    _sop_Unum = W03_unit_number_TB.Text;
                    _sop_Tnum = W03_train_number_TB.Text;
                    _sop_lines = W03_lines_TB.Text;
                    _sop_dist = W03_distance_TB.Text;
                    _sop_comp = W03_company_TB.Text;
                    _sop_cate = W03_category_TB.Text;

                    // パラメータの値を設定　テキストボックスから読み込む
                    searchId.Value = _sop_id;
                    searchTitle.Value = _sop_title;
                    searchBsta.Value = _sop_Bsta;
                    searchBtime.Value = _sop_Btime;
                    searchEsta.Value = _sop_Esta;
                    searchEtime.Value = _sop_Etime;
                    searchName.Value = _sop_name;
                    searchUnum.Value = _sop_Unum;
                    searchTnum.Value = _sop_Tnum;
                    searchLines.Value = _sop_lines;
                    searchDist.Value = _sop_dist;
                    searchComp.Value = _sop_comp;
                    searchCate.Value = _sop_cate;

                    // パラメータをコマンドに追加
                    cmd.Parameters.Add(searchId);
                    cmd.Parameters.Add(searchTitle);
                    cmd.Parameters.Add(searchBsta);
                    cmd.Parameters.Add(searchBtime);
                    cmd.Parameters.Add(searchEsta);
                    cmd.Parameters.Add(searchEtime);
                    cmd.Parameters.Add(searchName);
                    cmd.Parameters.Add(searchUnum);
                    cmd.Parameters.Add(searchTnum);
                    cmd.Parameters.Add(searchLines);
                    cmd.Parameters.Add(searchDist);
                    cmd.Parameters.Add(searchComp);
                    cmd.Parameters.Add(searchCate);

                    // SQLiteへの橋渡しのアダプターを設定
                    SQLiteDataAdapter sda = new SQLiteDataAdapter();

                    // SELECTコマンドを設定
                    sda.SelectCommand = cmd;

                    // SELECTの実行及びフェッチ
                    sda.Fill(searchResult);

                    // dataGridViewに表示
                    W03_DateGridView.DataSource = searchResult;
                }
            }
        }

        // 検索欄に入ってないやつは拾わない　入ってるやつは拾う
        private void SQL_Edit()
        {
            // IDが入ってたとき
            if (!String.IsNullOrEmpty(W03_id_TB.Text))
            {
                _sqlSearch = _sqlSearch + SQL_WHERE + SQL_ID;
                _containsWhere = true;
            }

            // タイトルが入ってたとき
            if (!String.IsNullOrEmpty(W03_title_TB.Text))
            {
                // whereが入ってるか入ってないかでSQLの文章が変わるので、whereスイッチでの条件分岐
                switch (_containsWhere)
                {
                    case true:
                        _sqlSearch = _sqlSearch + SQL_AND + SQL_TITLE;
                        break;

                    case false:
                        _sqlSearch = _sqlSearch + SQL_WHERE + SQL_TITLE;
                        _containsWhere = true;
                        break;
                }
            }

            // 乗車駅が入ってたとき
            if (!String.IsNullOrEmpty(W03_boarding_sta_TB.Text))
            {
                // whereが入ってるか入ってないかでSQLの文章が変わるので、whereスイッチでの条件分岐
                switch (_containsWhere)
                {
                    case true:
                        _sqlSearch = _sqlSearch + SQL_AND + SQL_BSTA;
                        break;

                    case false:
                        _sqlSearch = _sqlSearch + SQL_WHERE + SQL_BSTA;
                        _containsWhere = true;
                        break;
                }
            }

            // 乗車日時が入ってたとき
            if (!String.IsNullOrEmpty(W03_boarding_time_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (_containsWhere)
                {
                    case true:
                        _sqlSearch = _sqlSearch + SQL_AND + SQL_BTIME;
                        break;

                    case false:
                        _sqlSearch = _sqlSearch + SQL_WHERE + SQL_BTIME;
                        _containsWhere = true;
                        break;
                }
            }

            // 降車駅が入ってたとき
            if (!String.IsNullOrEmpty(W03_exit_sta_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (_containsWhere)
                {
                    case true:
                        _sqlSearch = _sqlSearch + SQL_AND + SQL_ESTA;
                        break;

                    case false:
                        _sqlSearch = _sqlSearch + SQL_WHERE + SQL_ESTA;
                        _containsWhere = true;
                        break;
                }
            }

            // 降車日時が入ってたとき
            if (!String.IsNullOrEmpty(W03_exit_time_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (_containsWhere)
                {
                    case true:
                        _sqlSearch = _sqlSearch + SQL_AND + SQL_ETIME;
                        break;

                    case false:
                        _sqlSearch = _sqlSearch + SQL_WHERE + SQL_ETIME;
                        _containsWhere = true;
                        break;
                }
            }

            // 列車名が入ってたとき
            if (!String.IsNullOrEmpty(W03_name_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (_containsWhere)
                {
                    case true:
                        _sqlSearch = _sqlSearch + SQL_AND + SQL_NAME;
                        break;

                    case false:
                        _sqlSearch = _sqlSearch + SQL_WHERE + SQL_NAME;
                        _containsWhere = true;
                        break;
                }
            }

            // 列車番号が入ってたとき
            if (!String.IsNullOrEmpty(W03_unit_number_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (_containsWhere)
                {
                    case true:
                        _sqlSearch = _sqlSearch + SQL_AND + SQL_UNUM;
                        break;

                    case false:
                        _sqlSearch = _sqlSearch + SQL_WHERE + SQL_UNUM;
                        _containsWhere = true;
                        break;
                }
            }

            // ナンバーが入ってたとき
            if (!String.IsNullOrEmpty(W03_train_number_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (_containsWhere)
                {
                    case true:
                        _sqlSearch = _sqlSearch + SQL_AND + SQL_TNUM;
                        break;

                    case false:
                        _sqlSearch = _sqlSearch + SQL_WHERE + SQL_TNUM;
                        _containsWhere = true;
                        break;
                }
            }

            // 乗車路線が入ってたとき
            if (!String.IsNullOrEmpty(W03_lines_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (_containsWhere)
                {
                    case true:
                        _sqlSearch = _sqlSearch + SQL_AND + SQL_LINES;
                        break;

                    case false:
                        _sqlSearch = _sqlSearch + SQL_WHERE + SQL_LINES;
                        _containsWhere = true;
                        break;
                }
            }

            // 乗車距離が入ってたとき
            if (!String.IsNullOrEmpty(W03_distance_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (_containsWhere)
                {
                    case true:
                        _sqlSearch = _sqlSearch + SQL_AND + SQL_DIST;
                        break;

                    case false:
                        _sqlSearch = _sqlSearch + SQL_WHERE + SQL_DIST;
                        _containsWhere = true;
                        break;
                }
            }

            // 会社が入ってたとき
            if (!String.IsNullOrEmpty(W03_company_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (_containsWhere)
                {
                    case true:
                        _sqlSearch = _sqlSearch + SQL_AND + SQL_COMP;
                        break;

                    case false:
                        _sqlSearch = _sqlSearch + SQL_WHERE + SQL_COMP;
                        _containsWhere = true;
                        break;
                }
            }

            // 種別が入ってたとき
            if (!String.IsNullOrEmpty(W03_category_TB.Text))
            {
                // whereが入ってるか入ってないかで文章が変わるので、whereスイッチでの条件分岐
                switch (_containsWhere)
                {
                    case true:
                        _sqlSearch = _sqlSearch + SQL_AND + SQL_CATE;
                        break;

                    case false:
                        _sqlSearch = _sqlSearch + SQL_WHERE + SQL_CATE;
                        _containsWhere = true;
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

            // クリックしたセルが駅だった時（3：乗車駅、5：降車駅）
            if (e.ColumnIndex == 3 || e.ColumnIndex == 5)
            {
                MessageBox.Show("駅クリックした！");
            }

            // 駅以外のセルの値をクリックした時
            if (e.ColumnIndex != 3 && e.ColumnIndex != 5)
            {
                // クリックしたデータのIDの値を取得
                search_ID = $"{W03_DateGridView[0, e.RowIndex].Value}";

                // 二重起動防止　既に開かれていた場合は一度閉じて開き直す
                if (this._w05 != null)
                {
                    // フォームを閉じる
                    this._w05.Close();
                }
                // フォームを開く
                if (this._w05 == null || this._w05.IsDisposed)
                {
                    this._w05 = new W05_RecordsDetailFromW03();
                    _w05.Show();
                }
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
                _sqlSearch = "select 乗車記録ID,記録タイトル,列車名,乗車駅,乗車日時,降車駅,降車日時 from 乗車記録";
                _containsWhere = false;

                // SQL文とコネクション、パラメータを設定
                using (SQLiteCommand cmd = new SQLiteCommand(_sqlSearch, con))
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
            // 使い回すのでSQL文やwhereスイッチの初期化はしない

            // 検索
            // 検索欄の情報を読み込んで、パラメータを使用してSQLで検索
            // データテーブルを作る
            DataTable search_result = new DataTable();

            // 接続情報を使ってコネクションを生成
            using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                // これ検索した時にいっかい検索条件保存しておいて、それを更新時もっかい読み直す感じにせんといかんな

                // SQL文とコネクション、パラメータを設定
                using (SQLiteCommand cmd = new SQLiteCommand(_sqlSearch, con))
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
                    s_id.Value = _sop_id;
                    s_title.Value = _sop_title;
                    s_Bsta.Value = _sop_Bsta;
                    s_Btime.Value = _sop_Btime;
                    s_Esta.Value = _sop_Esta;
                    s_Etime.Value = _sop_Etime;
                    s_name.Value = _sop_name;
                    s_Unum.Value = _sop_Unum;
                    s_Tnum.Value = _sop_Tnum;
                    s_lines.Value = _sop_lines;
                    s_dist.Value = _sop_dist;
                    s_comp.Value = _sop_comp;
                    s_cate.Value = _sop_cate;

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
