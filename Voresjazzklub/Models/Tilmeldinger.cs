using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Voresjazzklub.Models {
    public class Tilmeldinger {
        private char _erTilmeldtChar;
        private char _spiserChar;
        private bool _erTilmeldt;
        private bool _spiser;
        [Key]
        public long arrangementId { get; set; }
        public string brugerId { get; set; }
        [Display(Name = "Oprettet")]
        public DateTime createTs { get; set; }
        [Display(Name = "Tilmeldt")]
        public bool erTilmeldt {
            get { return _erTilmeldt; }
            set { _erTilmeldt = value; _erTilmeldtChar = (_erTilmeldt) ? 'T' : 'F'; }
        }
        [Display(Name = "Tilmeldt")]
        public char erTilmeldtChar {
            get { return _erTilmeldtChar; }
            set { _erTilmeldtChar = value; _erTilmeldt = _erTilmeldtChar == 'T'; }
        }
        [Display(Name = "Spiser")]
        public bool spiser {
            get { return _spiser; }
            set { _spiser = value; _spiserChar = (_spiser) ? 'T' : 'F'; }
        }
        [Display(Name = "Spiser")]
        public char spiserChar {
            get { return _spiserChar; }
            set { _spiserChar = value; _spiser = _spiserChar == 'T'; }
        }



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


        public Tilmeldinger() {

        }
        /*
        public Tilmeldinger(string brugerId) {
            this.brugerId = brugerId;
            this.lastlogon = DateTime.Now;
            this.logonCnt = 0;
        }*/
        public void create() {
            openConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format(
                "insert into Tilmeldinger(arrangementId, brugerId, createTs, erTilmeldt, spiser) " +
                "values({0}, '{1}', current_timestamp, '{2}', '{3}');",
                arrangementId, brugerId, erTilmeldtChar, spiserChar);
            
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();
        }

        public Tilmeldinger read(long arrangementId, string brugerId) {
            openConnection();
            Tilmeldinger tilmeldinger = new Tilmeldinger();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format("select arrangementId, brugerId, createTs, erTilmeldt, spiser from Tilmeldinger where arrangementId={0} and brugerId='{1}'", arrangementId, brugerId);

            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read()) {
                tilmeldinger.arrangementId = reader.GetInt64("arrangementId");
                tilmeldinger.brugerId = reader.GetString("brugerId");
                tilmeldinger.createTs = reader.GetDateTime("createTs");
                tilmeldinger.erTilmeldtChar = reader.GetChar("erTilmeldt");
                tilmeldinger.spiserChar = reader.GetChar("spiser");
            }

            closeConnection();
            return tilmeldinger;
        }
        public List<Tilmeldinger> readList(long arran) {
            List<Tilmeldinger> tilmeldingerList = new List<Tilmeldinger>();
            openConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format("select arrangementId, brugerId, createTs, erTilmeldt, spiser from Tilmeldinger where arrangementId={0}", arran);

            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Tilmeldinger tilmeldinger = new Tilmeldinger();
                tilmeldinger.arrangementId = reader.GetInt64("arrangementId");
                tilmeldinger.brugerId = reader.GetString("brugerId");
                tilmeldinger.createTs = reader.GetDateTime("createTs");
                tilmeldinger.erTilmeldtChar = reader.GetChar("erTilmeldt");
                tilmeldinger.spiserChar = reader.GetChar("spiser");
                tilmeldingerList.Add(tilmeldinger);
            }

            closeConnection();
            return tilmeldingerList;
        }

        public List<Tilmeldinger> readList() {
            List<Tilmeldinger> tilmeldingerList = new List<Tilmeldinger>();
            openConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format("select arrangementId, brugerId, createTs, erTilmeldt, spiser from Tilmeldinger");

            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Tilmeldinger tilmeldinger = new Tilmeldinger();
                tilmeldinger.arrangementId = reader.GetInt64("arrangementId");
                tilmeldinger.brugerId = reader.GetString("brugerId");
                tilmeldinger.createTs = reader.GetDateTime("createTs");
                tilmeldinger.erTilmeldtChar = reader.GetChar("erTilmeldt");
                tilmeldinger.spiserChar = reader.GetChar("spiser");
                tilmeldingerList.Add(tilmeldinger);
            }

            closeConnection();
            return tilmeldingerList;
        }
        public void update() {
            openConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format(
                "update Tilmeldinger set createTs=current_timestamp, erTilmeldt='{0}', spiser='{1}' where arrangementId={2} and brugerId='{3}' ",
                this.erTilmeldtChar, this.spiserChar, this.arrangementId, this.brugerId);
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();
        }
        public void delete(long arrrangementId, string brugerId) {
            openConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format(
                "delete from Tilmeldinger where arrangementId={0} and brugerId='{1}' ", arrrangementId, brugerId);
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();
        }
    }


}


