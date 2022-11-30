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
    public partial class W09 : Form
    {
        public W09()
        {
            InitializeComponent();
        }

        // 乗車記録一件の新規登録
        private void W09_register_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Rail_records_system_DB.db"))
            {
                con.Open();

                using (SQLiteTransaction trans = con.BeginTransaction())
                {
                    SQLiteCommand cmd = con.CreateCommand();

                    // インサート
                    // @をつけることで後述のParametersでセットした値をプログラム実行時に付加してSQLを実行できる
                    cmd.CommandText = 
                        "INSERT INTO 乗車記録 " +
                        "(記録タイトル,乗車駅,乗車日時,降車駅,降車日時,列車名,列車番号,乗車車両ナンバー,乗車路線,乗車距離,鉄道会社,鉄道種別,備考)" +
                        " VALUES (@記録タイトル,@乗車駅,@乗車日時,@降車駅,@降車日時,@列車名,@列車番号,@乗車車両ナンバー,@乗車路線,@乗車距離,@鉄道会社,@鉄道種別,@備考)";

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

                    cmd.ExecuteNonQuery();

                    // コミット
                    // trans.commit();でDBの変更を確定
                    trans.Commit();

                    // テキストボックスにデータを入力してデータ追加ボタンを押すとDBにデータが登録されます
                }
            }
        }

        private void W09_close_Click(object sender, EventArgs e)
        {
            //画面を閉じる
            this.Close();
        }

        private void W09_note_TB_TextChanged(object sender, EventArgs e)
        {

        }

        private void W09_category_TB_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
