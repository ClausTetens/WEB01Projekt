using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Voresjazzklub.Models {
    public class LogonModel {
        [Key]
        public string brugerId { get; set; }
        public DateTime lastlogon { get; set; }
        public int logonCnt { get; set; }

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


        public LogonModel() {

        }
        public LogonModel(string brugerId) {
            this.brugerId = brugerId;
            this.lastlogon = DateTime.Now;
            this.logonCnt = 0;
        }

        public void create(string userId) {
            new LogonModel(userId).create();
        }
        public void create() {
            openConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format(
                "insert into Logon(brugerId, lastLogon, logonCnt) " +
                "values('{0}', current_timestamp, 0);",
                brugerId);
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();
        }


        public LogonModel read(string id) {
            openConnection();
            LogonModel logonModel = new LogonModel();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format("select brugerId, lastLogon, logonCnt from Logon where brugerId='{0}'", id);

            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read()) {
                logonModel.brugerId= reader.GetString("brugerId");
                logonModel.lastlogon = reader.GetDateTime("lastlogon");
                logonModel.logonCnt = reader.GetInt32("logonCnt");
            }

            closeConnection();
            return logonModel;
        }
        public List<LogonModel> readList() {
            List<LogonModel> logonModelList = new List<LogonModel>();
            openConnection();
            
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format("select brugerId, lastLogon, logonCnt from Logon");

            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read()) {
                LogonModel logonModel = new LogonModel();
                logonModel.brugerId = reader.GetString("brugerId");
                logonModel.lastlogon = reader.GetDateTime("lastlogon");
                logonModel.logonCnt = reader.GetInt32("logonCnt");
                logonModelList.Add(logonModel);
            }

            closeConnection();
            return logonModelList;

        }
        public void update() {
            openConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format(
                "update Logon set lastLogon=current_timestamp, logonCnt={0} where brugerId='{1}' ",
                this.logonCnt, this.brugerId);
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();
        }
        public void delete(string id) {
            openConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format(
                "delete from Logon where  brugerId='{0}' ", id);
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();
        }
    }
}