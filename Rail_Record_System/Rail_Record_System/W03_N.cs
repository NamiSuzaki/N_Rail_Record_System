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
        public FormW05 w05_n = null;

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
        // public string SQL_search_text = "select * from 乗車記録 where 乗車記録ID Like @検索ID";

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

        private void gobacktoW01_Click(object sender, EventArgs e)
        {
            
        }

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
        }

        // 検索欄に入ってないやつは拾わない　入ってるやつは拾う
        private void SQL_Edit()
        {
            /*
             検索仕様
             完全一致：ID、駅名、日時、
             あいまい：タイトル、ナンバー、列番、会社
            */

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

        // クリックしたセルの取得
        // CellContentなので、セルそのものじゃなく『セル内の値』をクリックすると反応する
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //string s1 = $"クリックされた位置 {e.RowIndex}列目 {e.ColumnIndex}行目";
            // 行・列のヘッダーをクリックした場合は流す
            if (e.ColumnIndex == -1 || e.RowIndex == -1)
            {
                return;
            }

            search_ID = $"{W03_DateGridView[0, e.RowIndex].Value}";
            MessageBox.Show(search_ID, "情報",MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 二重起動防止
            // null、または画面が破棄されていたら開く
            if (this.w05_n == null || this.w05_n.IsDisposed)
            {
                this.w05_n = new FormW05();
                w05_n.Show();
            }
        }
    }
}
