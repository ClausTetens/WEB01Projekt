using System;
using MySql.Data.MySqlClient;
using System.Configuration;




namespace Voresjazzklub.Models {
    public class AspNetIdentityModel {
        public int sqlId { get; set; }
        public string sqlScript { get; set; }
        public string sqlResult { get; set; }

        public void runScript() {

        }

        private MySqlConnection connection;
#if DEBUG
        public void selectScript() {
            try {
                getConnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = sqlScript;
                int rows = cmd.ExecuteNonQuery();
                MySqlDataReader reader = cmd.ExecuteReader();
                sqlResult = reader.HasRows ? "selectScript() er kørt, følgende fundet:\n" : "Ingen rækker fundet i selectScript()\n";
                while(reader.Read()) {
                    for(int i = 0; i < reader.FieldCount; i++) {
                        try {
                            sqlResult += (i > 0 ? "\t" : "") + reader.GetString(i);
                        } catch(Exception) { } // primitiv måde at omgå NULLs fra DB
                    }
                    sqlResult += "\n";
                }
            } catch(MySqlException e) {
                sqlResult = "MySqlException " + e;
            } finally {
                connection.Close();
            }
        }
        public void nonResultScript() {
            try {
                getConnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = sqlScript;
                int rows = cmd.ExecuteNonQuery();
                sqlResult = "nonResultScript() er kørt. Antal rows: " + rows;
            } catch(MySqlException e) {
                sqlResult = "MySqlException " + e;
            } finally {
                connection.Close();
            }
        }
#else
        public void selectScript() {}
        public void nonResultScript() {}
#endif

        private void getConnection() {
            connection = new MySqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection.Open(); // kan smide en exception
        }
    }
}