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
            // パラメータを使用してSQLで検索
            
            DataTable dt = new DataTable();

                //接続情報を使ってコネクションを生成
                using (SQLiteConnection con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
                {
                    //SQL文とコネクションを設定
                    using (SQLiteCommand cmd = new SQLiteCommand("select * from 乗車記録 where 乗車記録ID Like @検索ID and 乗車駅 Like %@検索乗車駅%", con))

                    //記録タイトル,乗車駅,乗車日時,降車駅,降車日時,列車名,列車番号,乗車車両ナンバー,乗車路線,乗車距離,鉄道会社,鉄道種別,備考
                    {
                        //パラメータの作成
                        SQLiteParameter s_id   = new SQLiteParameter();
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

                        //パラメータ名は@を除いた名前を指定
                        s_id.ParameterName      = "検索ID";
                        s_title.ParameterName   = "検索タイトル";
                        s_Bsta.ParameterName    = "検索乗車駅";
                        s_Btime.ParameterName   = "検索乗車日時";
                        s_Esta.ParameterName    = "検索降車駅";
                        s_Etime.ParameterName   = "検索降車日時";
                        s_name.ParameterName    = "検索列車名";
                        s_Unum.ParameterName    = "検索列車番号";
                        s_Tnum.ParameterName    = "検索乗車車両ナンバー";
                        s_lines.ParameterName   = "検索乗車路線";
                        s_dist.ParameterName    = "検索乗車距離";
                        s_comp.ParameterName    = "検索鉄道会社";
                        s_cate.ParameterName    = "検索鉄道種別";

                        //パラメータの値を設定
                        s_id.Value = W03_id_TB.Text;
                        s_title.Value = W03_title_TB;
                        s_Bsta.Value = W03_boarding_sta_TB;
                        s_Btime.Value = W03_boarding_time_TB;
                        s_Esta.Value = W03_exit_sta_TB;
                        s_Etime.Value = W03_exit_time_TB;
                        s_name.Value = W03_name_TB;
                        s_Unum.Value = W03_unit_number_TB;
                        s_Tnum.Value = W03_train_number_TB;
                        s_lines.Value = W03_lines_TB;
                        s_dist.Value = W03_distance_TB;
                        s_comp.Value = W03_company_TB;
                        s_cate.Value = W03_category_TB;

                        //パラメータをコマンドに追加
                        cmd.Parameters.Add(s_id);

                        //SQLiteへの橋渡しのアダプターを設定
                        SQLiteDataAdapter sda = new SQLiteDataAdapter();

                        //SELECTコマンドを設定
                        sda.SelectCommand = cmd;

                        //SELECTの実行及びフェッチ
                        sda.Fill(dt);

                        //dataGridViewに表示
                        dataGridView1.DataSource = dt;
                    }
                }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
