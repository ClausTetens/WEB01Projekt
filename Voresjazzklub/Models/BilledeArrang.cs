using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Voresjazzklub.Models {


    public class BilledeArrang {
        [Key]
        public long billedId { get; set; }
        public string billedAdresse { get; set; }
        public long arrangementId { get; set; }
        public string brugerId { get; set; }
        [Display(Name = "Oprettet")]
        public DateTime createTs { get; set; }
        public string kommentar { get; set; }
        [Display(Name = "Arrangement")]
        public string arrangementBeskrivelse { get; set; }


        private MySqlConnection connection;
        MySqlCommand cmd = new MySqlCommand();
        private void openConnection() {
            if(connection == null) {
                try {
                    connection = new MySqlConnection();
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
                    connection.Open();
                    cmd.Connection = connection;
                } catch(Exception) {
                }
            }
        }

        private void closeConnection() {
            connection.Close();
            connection = null;
        }



        public void update() {
            openConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format(
                "update billeder2 set createTs=current_timestamp, kommentar='{0}' where billedId={1}",
                this.kommentar, this.billedId);
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();
        }
        public void delete(string billedAdresse) {
            openConnection();
            cmd.CommandText = string.Format("delete from billeder2 where  billedAdresse = '{0}'", billedAdresse);
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();
        }
        public void delete() {
            delete(billedId);
        }
        public void delete(long billedId) {
            openConnection();
            cmd.CommandText = string.Format("delete from billeder2 where  billedId = {0}", billedId);
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();
        }

        public void create() {
            openConnection();
            cmd.CommandText = string.Format(
                "insert into billeder2(billedAdresse, arrangementId, brugerId, createTs, kommentar) " +
                "values('{0}', {1}, '{2}', current_timestamp,'{3}');",
                billedAdresse, arrangementId, brugerId, kommentar);
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();
        }
        public override bool Equals(System.Object obj) {
            if(obj == null)
                return false;
            BilledeArrang x = (BilledeArrang)obj;
            if(x == null)
                return false;
            //return (x.billedAdresse == billedAdresse && x.arrangementId == arrangementId && x.brugerId == brugerId && x.createTs == createTs && x.kommentar == kommentar && x.arrangementBeskrivelse == arrangementBeskrivelse);
            //  uden createTs:
            return (x.billedAdresse == billedAdresse && x.arrangementId == arrangementId && x.brugerId == brugerId && x.kommentar == kommentar && x.arrangementBeskrivelse == arrangementBeskrivelse);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public BilledeArrang read(string billedAdresse) {
            openConnection();
            cmd.CommandText = string.Format("select b.billedId, b.billedAdresse, b.arrangementId, b.brugerId, b.createTs, b.kommentar, a.arrangementBeskrivelse from billeder2 b, arrangement a where a.arrangementId = b.arrangementId and b.billedAdresse = '{0}'", billedAdresse);
            BilledeArrang billedeArrang = new BilledeArrang();
            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read()) {
                //billedAdresse, arrangementId, brugerId, createTs, kommentar, arrangementBeskrivelse
                billedeArrang.billedId = reader.GetInt64("billedId");
                billedeArrang.billedAdresse = reader.GetString("billedAdresse");
                billedeArrang.arrangementId = reader.GetInt64("arrangementId");
                billedeArrang.brugerId = reader.GetString("brugerId");
                billedeArrang.createTs = reader.GetDateTime("createTs");
                billedeArrang.kommentar = reader.GetString("kommentar");
                billedeArrang.arrangementBeskrivelse = reader.GetString("arrangementBeskrivelse");
            }
            closeConnection();
            return billedeArrang;
        }

        public BilledeArrang read(long arranId, long billedId) {
            openConnection();
            cmd.CommandText = string.Format("select b.billedId, b.billedAdresse, b.arrangementId, b.brugerId, b.createTs, b.kommentar, a.arrangementBeskrivelse from billeder2 b, arrangement a where a.arrangementId = b.arrangementId and a.arrangementId = {0} and b.billedId={1}", arranId, billedId);
            BilledeArrang billedeArrang = new BilledeArrang();
            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read()) {
                //billedAdresse, arrangementId, brugerId, createTs, kommentar, arrangementBeskrivelse
                billedeArrang.billedId = reader.GetInt64("billedId");
                billedeArrang.billedAdresse = reader.GetString("billedAdresse");
                billedeArrang.arrangementId = reader.GetInt64("arrangementId");
                billedeArrang.brugerId = reader.GetString("brugerId");
                billedeArrang.createTs = reader.GetDateTime("createTs");
                billedeArrang.kommentar = reader.GetString("kommentar");
                billedeArrang.arrangementBeskrivelse = reader.GetString("arrangementBeskrivelse");
            }
            closeConnection();
            return billedeArrang;
        }
        public BilledeArrang readMedArranId(long arranId) {
            openConnection();
            cmd.CommandText = string.Format("select b.billedId, b.billedAdresse, b.arrangementId, b.brugerId, b.createTs, b.kommentar, a.arrangementBeskrivelse from billeder2 b, arrangement a where a.arrangementId = b.arrangementId and a.arrangementId = {0}", arranId);
            BilledeArrang billedeArrang = new BilledeArrang();
            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read()) {
                //billedAdresse, arrangementId, brugerId, createTs, kommentar, arrangementBeskrivelse
                billedeArrang.billedId = reader.GetInt64("billedId");
                billedeArrang.billedAdresse = reader.GetString("billedAdresse");
                billedeArrang.arrangementId = reader.GetInt64("arrangementId");
                billedeArrang.brugerId = reader.GetString("brugerId");
                billedeArrang.createTs = reader.GetDateTime("createTs");
                billedeArrang.kommentar = reader.GetString("kommentar");
                billedeArrang.arrangementBeskrivelse = reader.GetString("arrangementBeskrivelse");
            }
            closeConnection();
            return billedeArrang;
        }
        public BilledeArrang readMedBilledId(long billedId) {
            openConnection();
            cmd.CommandText = string.Format("select b.billedId, b.billedAdresse, b.arrangementId, b.brugerId, b.createTs, b.kommentar, a.arrangementBeskrivelse from billeder2 b, arrangement a where a.arrangementId = b.arrangementId and b.billedId = '{0}'", billedId);
            BilledeArrang billedeArrang = new BilledeArrang();
            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read()) {
                //billedAdresse, arrangementId, brugerId, createTs, kommentar, arrangementBeskrivelse
                billedeArrang.billedId = reader.GetInt64("billedId");
                billedeArrang.billedAdresse = reader.GetString("billedAdresse");
                billedeArrang.arrangementId = reader.GetInt64("arrangementId");
                billedeArrang.brugerId = reader.GetString("brugerId");
                billedeArrang.createTs = reader.GetDateTime("createTs");
                billedeArrang.kommentar = reader.GetString("kommentar");
                billedeArrang.arrangementBeskrivelse = reader.GetString("arrangementBeskrivelse");
            }
            closeConnection();
            return billedeArrang;
        }

        public List<BilledeArrang> readList(long arrangementId) {
            return readList(arrangementId, null);
        }

        public List<BilledeArrang> readList(string brugerId) {
            return readList(null, brugerId);
        }
        public List<BilledeArrang> readList() {
            return readList(null, null);
        }

        public List<BilledeArrang> readList(long? arrangementId, string brugerId) {
            List<BilledeArrang> billedeArrangList = new List<BilledeArrang>();

            openConnection();

            cmd.CommandText = 
                "select b.billedId, b.billedAdresse, b.arrangementId, b.brugerId, b.createTs, b.kommentar, a.arrangementBeskrivelse " +
                "from billeder2 b, arrangement a " +
                "where a.arrangementId = b.arrangementId " +
                ((arrangementId == null) ? "" : "and b.arrangementId=" + arrangementId) + 
                ((brugerId == null) ? "" : " and b.brugerId=='" + brugerId + "'");

            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read()) {
                BilledeArrang billedeArrang = new BilledeArrang();
                billedeArrang.billedId = reader.GetInt64("billedId");
                billedeArrang.billedAdresse = reader.GetString("billedAdresse");
                billedeArrang.arrangementId = reader.GetInt64("arrangementId");
                billedeArrang.brugerId = reader.GetString("brugerId");
                billedeArrang.createTs = reader.GetDateTime("createTs");
                billedeArrang.kommentar = reader.GetString("kommentar");
                billedeArrang.arrangementBeskrivelse = reader.GetString("arrangementBeskrivelse");
                billedeArrangList.Add(billedeArrang);
            }
            closeConnection();
            return billedeArrangList;
        }

        /**
        public BilledeArrang read() {
            List<BilledeArrang> billedeArrangList = new List<BilledeArrang>();
            BilledeArrang a = new BilledeArrang();
            a.arrangementBeskrivelse = "BeskrivA";
            a.arrangementId = 1;
            //a.billedAdresse = @"C:\Users\u3249\Documents\Visual Studio 2015\Projects\Voresjazzklub\Voresjazzklub\App_Data\Uploadthen-a-miracle-occurs-cartoon.png";
            a.billedAdresse = "address A";
            a.brugerId = "Rip";
            a.createTs = DateTime.Now;
            a.kommentar="Et fint billedeA";

            BilledeArrang b = new BilledeArrang();
            b.arrangementBeskrivelse = "BeskrivB";
            b.arrangementId = 1;
            //b.billedAdresse = @"C:\Users\u3249\Documents\Visual Studio 2015\Projects\Voresjazzklub\Voresjazzklub\App_Data\deviant-code-1000px-da.png";
            b.billedAdresse = "address B";
            b.brugerId = "Rap";
            b.createTs = DateTime.Now;
            b.kommentar = "Et fint billedeB";

            billedeArrangList.Add(a);
            billedeArrangList.Add(b);
            return a;
        }
        /**/


            /*
        public List<BilledeArrang> readList(long id) {
            List<BilledeArrang> billedeArrangList = new List<BilledeArrang>();
            string sql = string.Format("Select ... from ... where arrangementId={0}", id);
            BilledeArrang a = new BilledeArrang();
            a.arrangementBeskrivelse = "Beskrivelse for billedet";
            a.arrangementId = 1;
            a.billedAdresse = "/img/Upload/then-a-miracle-occurs-cartoon.png";
            a.brugerId = "RippidiRappediRup";
            a.createTs = DateTime.Now;
            a.kommentar = "Et fint billede :)";
            billedeArrangList.Add(a);
            return billedeArrangList;
        }


        public List<BilledeArrang> readList() {
            List<BilledeArrang> billedeArrangList = new List<BilledeArrang>();
            BilledeArrang a = new BilledeArrang();
            a.arrangementBeskrivelse = "BeskrivA";
            a.arrangementId = 1;
            a.billedAdresse = @"/img/Upload/then-a-miracle-occurs-cartoon.png";
            a.brugerId = "Rip";
            a.createTs = DateTime.Now;
            a.kommentar = "Et fint billedeA";

            BilledeArrang b = new BilledeArrang();
            b.arrangementBeskrivelse = "BeskrivA";
            b.arrangementId = 1;
            b.billedAdresse = @"/img/upload/deviant-code-1000px-da.png";
            b.brugerId = "Rap";
            b.createTs = DateTime.Now;
            b.kommentar = "Et fint billedeB";

            billedeArrangList.Add(a);
            billedeArrangList.Add(b);
            return billedeArrangList;
        }
        /*  */

    }
}