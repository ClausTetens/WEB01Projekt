using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Voresjazzklub.Models {
    public class ArrangementModel {
        [Key]
        [Required]
        public long arrangementId { get; set; }
        [Required(ErrorMessage ="Man skal da vide, hvad arrangementet går ud på")]
        public string arrangementBeskrivelse { get; set; }
        public DateTime createTs { get; set; }
        public string arrangementWeb { get; set; }
        public DateTime arrangementDt { get; set; }
        public DateTime arrangemnetTid { get; set; }
        public string spiseSted { get; set; }
        public DateTime spiseTid { get; set; }

        private MySqlConnection connection;
        /**/
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

        public void create() {
            create(this);
        }

        public void create(ArrangementModel arrangement) {
            openConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format(
                "insert into Arrangement(arrangementBeskrivelse, createTs, arrangementWeb, arrangementDt, arrangemnetTid, spiseSted, spiseTid) " +
                "values('{0}', current_timestamp,'{1}','{2}','{3}','{4}','{5}');",
                arrangementBeskrivelse, arrangementWeb, arrangementDt.ToString("yyyy-MM-dd"), arrangemnetTid.ToString("HH:mm:ss"), spiseSted, spiseTid.ToString("HH:mm:ss"));
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();

        }
        public ArrangementModel read(long id) {
            openConnection();
            ArrangementModel arrangement = new ArrangementModel();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format("select arrangementId, arrangementBeskrivelse, createTs, arrangementWeb, arrangementDt, arrangemnetTid, spiseSted, spiseTid from Arrangement where arrangementId={0}",id);

            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read()) {
                arrangement.arrangementId = reader.GetInt64("arrangementId");
                arrangement.arrangementBeskrivelse = reader.GetString("arrangementBeskrivelse");
                arrangement.createTs = reader.GetDateTime("createTs");
                arrangement.arrangementWeb = reader.GetString("arrangementWeb");
                arrangement.arrangementDt = reader.GetDateTime("arrangementDt");
                arrangement.arrangemnetTid = DateTime.Parse(reader.GetString("arrangemnetTid"));
                arrangement.spiseSted = reader.GetString("spiseSted");
                arrangement.spiseTid = DateTime.Parse(reader.GetString("spiseTid"));
            }

            closeConnection();
            return arrangement;
        }

        public List<ArrangementModel> readList() {
            List<ArrangementModel> arrangementer = new List<ArrangementModel>();
            openConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format("select arrangementId, arrangementBeskrivelse, createTs, arrangementWeb, arrangementDt, arrangemnetTid, spiseSted, spiseTid from Arrangement");

            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read()) {
                ArrangementModel arrangement = new ArrangementModel();
                arrangement.arrangementId = reader.GetInt64("arrangementId");
                arrangement.arrangementBeskrivelse = reader.GetString("arrangementBeskrivelse");
                arrangement.createTs = reader.GetDateTime("createTs");
                arrangement.arrangementWeb = reader.GetString("arrangementWeb");
                arrangement.arrangementDt = reader.GetDateTime("arrangementDt");
                arrangement.arrangemnetTid = DateTime.Parse( reader.GetString("arrangemnetTid"));
                arrangement.spiseSted = reader.GetString("spiseSted");
                arrangement.spiseTid = DateTime.Parse(reader.GetString("spiseTid"));
                arrangementer.Add(arrangement);
            }

            closeConnection();
            return arrangementer;
        }
        public ArrangementModel() {
 
        }

        public ArrangementModel(string arrangementBeskrivelse, string arrangementWeb, DateTime arrangementDt, DateTime arrangemnetTid, string spiseSted, DateTime spiseTid) {
            this.arrangementId = 0;
            this.arrangementBeskrivelse = arrangementBeskrivelse;
            this.createTs = DateTime.Now;
            this.arrangementWeb = arrangementWeb;
            this.arrangementDt = arrangementDt;
            this.arrangemnetTid = arrangemnetTid;
            this.spiseSted = spiseSted;
            this.spiseTid = spiseTid;
        }

        public void update(ArrangementModel arrangement) {
            openConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format(
                "update Arrangement set arrangementBeskrivelse='{0}', arrangementWeb='{1}', arrangementDt='{2}', arrangemnetTid='{3}', spiseSted='{4}', spiseTid='{5}' where arrangementId={6} ",
                arrangementBeskrivelse, arrangementWeb, arrangementDt.ToString("yyyy-MM-dd"), arrangemnetTid.ToString("HH:mm:ss"), spiseSted, spiseTid.ToString("HH:mm:ss"), arrangementId);
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();
        }
        public void update() {
            update(this);
        }

        public void delete(long id) {

            openConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format("delete from Arrangement where ArrangementId = '{0}'", id);
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();

        }
        /**/
    }
    
}






