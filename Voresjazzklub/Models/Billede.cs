using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Voresjazzklub.Models {
    public class Billede {
        [Key]
        public string billedAdresse { get; set; }
        public long arrangementId { get; set; }
        public string brugerId { get; set; }
        public DateTime createTs { get; set; }
        public string kommentar { get; set; }

        private MySqlConnection connection;
        private void openConnection() {
            if(connection == null) {
                try {
                    connection = new MySqlConnection();
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
                    connection.Open();
                } catch(Exception) {
                }
            }
        }
        private void closeConnection() {
            connection.Close();
            connection = null;
        }

        public List<Billede> read() {
            List<Billede> billedeList = new List<Billede>();
            openConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format("select brugerId, lastLogon, logonCnt from Logon");

            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Billede billede = new Billede();
                billede.arrangementId = reader.GetInt64("arrangementId");
                billede.billedAdresse = reader.GetString("billedAdresse");
                billede.brugerId = reader.GetString("brugerId");
                billede.createTs = reader.GetDateTime("createTs");
                billede.kommentar = reader.GetString("kommentar");
                billedeList.Add(billede);
            }

            closeConnection();
            return billedeList;

        }


    }
}