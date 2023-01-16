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
                // DBを開く
                con.Open();

                    using (SQLiteTransaction trans = con.BeginTransaction())
                    {
                        SQLiteCommand cmd = con.CreateCommand();
                        //cmd.CommandText = "pragma foreign_keys = true";
                        //cmd.ExecuteNonQuery();

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
                        DialogResult result = MessageBox.Show("乗車駅・降車駅を入力してください","登録内容エラー",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        cmd.ExecuteNonQuery();

                        // コミット　trans.commit();でDBの変更を確定
                        trans.Commit();

                    }
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

        private void W09_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // コネクションを開いてテーブル作成して閉じる
            // Data Source=で指定した名前がDBのファイル名
            using (var con = new SQLiteConnection("Data Source=Rail_records_system_DB.db"))
            {
                con.Open();

                using (SQLiteCommand command = con.CreateCommand())
                {
                    // 記録テーブルを作成
                    command.CommandText =
                    "create table if not exists 乗車記録" +
                    " (乗車記録ID INTEGER primary key autoincrement," +
                    "記録タイトル text default '無題'," +
                    "乗車駅 text not null," +
                    "乗車日時 text default '2000-00-00 00:00'," +
                    "降車駅 text not null," +
                    "降車日時 text default '2000-00-00 00:00'," +
                    "列車名 text default '列車名なし'," +
                    "列車番号 text default '-'," +
                    "乗車車両ナンバー text default '-'," +
                    "乗車路線 text default '-'," +
                    "乗車距離 real default '0'," +
                    "鉄道会社 text," +
                    "鉄道種別 text," +
                    "備考 text)";

                    command.ExecuteNonQuery();
                }
                
                con.Close();
                
                // con.Open();でDBを接続しcon.Close();でDB接続を切断します
                // 使い終わったら必ず接続を切断しましょう
                // command.CommandTextには実行するSQL文を入れます
                // Command.ExecuteNonQuery(); でSQLを実行します
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // コネクションを開いてテーブル削除して閉じる
            using (var con = new SQLiteConnection("Data Source = Rail_records_system_DB.db"))
            {
                con.Open();
                
                try
                {
                    using (SQLiteCommand command = con.CreateCommand())
                    {
                        // テーブルを削除するときはdrop tableを使います
                        command.CommandText = "drop table 乗車記録";
                        command.ExecuteNonQuery();
                    }
                }

                finally
                {
                    con.Close();
                }
            }
        }

        private void w011_Load(object sender, EventArgs e)
        {

        }
    }
}
