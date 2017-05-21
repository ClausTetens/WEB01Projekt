using System;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Collections.Generic;

namespace Voresjazzklub.Models {
    public class UsersTableModel {
        private bool _prim, _erAdmin;
        private char _primChar, _erAdminChar;
        [Key] // presumably not necessary except for Entity Framework
        [Display(Name ="Bruger")]
        [UserIdValidator("Forkert anvendelse af brugernavn")]
        [Required]
        public string brugerId { get; set; }
        [Display(Name = "Oprettet dato og tid")]
        public DateTime createTs { get; set; }
        [Required]
        public string password { get; set; }
        [Display(Name ="Salt (sikring af password)")]
        public string salt { get; set; }
        public string email { get; set; }
        [Display(Name ="Primær email")]
        public bool prim {
            get { return _prim; }
            set { _prim = value; _primChar = (_prim) ? 'T' : 'F'; }
        }
        [Display(Name = "Er den primære email-adresse")]
        public char primChar {
            get { return _primChar; }
            set { _primChar = value; _prim = _primChar == 'T'; }
        }
        [Display(Name = "Er administrator")]
        public bool erAdmin {
            get { return _erAdmin; }
            set { _erAdmin = value; _erAdminChar = (_erAdmin) ? 'T' : 'F'; }
        }
        public char erAdminChar {
            get { return _erAdminChar; }
            set { _erAdminChar = value; _erAdmin = _erAdminChar == 'T'; }
        }

        public UsersTableModel() {

        }


        public void changeAllPasswords() {
            List<UsersTableModel> usersTableModelList = getUserList();
            foreach(UsersTableModel usersTableModel in usersTableModelList) {
                usersTableModel.password = "pass";
                usersTableModel.updateUser();
            }
        }

        public UsersTableModel(string userId, string password, string salt, string email, char primEmail, char isAdmin) {
            this.brugerId = userId;
            this.createTs = DateTime.Now;
            this.password = password;
            this.salt = salt;
            this.email = email;
            this.primChar = primEmail;
            this.erAdminChar = isAdmin;
        }

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

        public void getUser() {
            getUser(brugerId);
        }

        public bool isUserValid() {
            UsersTableModel usersTableModel = new UsersTableModel();
            usersTableModel.getUser(this.brugerId);
            return(this.brugerId == usersTableModel.brugerId && new Hash().getHashedPassword(this.password, usersTableModel.salt) == usersTableModel.password) ;
        }

   
        public void getUser(string brugerId) {
            openConnection();
            
            cmd.CommandText = string.Format("select brugerId, createTs, password, salt, email, prim, erAdmin from users where brugerId = '{0}'", brugerId);
            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read()) {
                this.brugerId = reader.GetString("brugerId");
                this.createTs = reader.GetDateTime("createTs");
                this.password = reader.GetString("password");
                this.salt = reader.GetString("salt");
                this.email = reader.GetString("email");
                this.primChar = reader.GetChar("prim");
                this.erAdminChar = reader.GetChar("erAdmin");
            } else {
                this.brugerId = "";
                // ToDo: clear class properties
            }

            closeConnection();
        }


        public bool userExists() {
            return userExists(brugerId);
        }
        public bool userExists(string brugerId) {
            openConnection();

            cmd.CommandText = string.Format("select brugerId, createTs, password, salt, email, prim, erAdmin from users where brugerId = '{0}'", brugerId);
            MySqlDataReader reader = cmd.ExecuteReader();
            bool exists=reader.Read();
            closeConnection();
            return exists;
        }


        public bool isAdministrator(string brugerId) {
            openConnection();
            cmd.CommandText = string.Format("select '1' from users where erAdmin='T' and brugerId = '{0}'", brugerId);
            MySqlDataReader reader = cmd.ExecuteReader();
            bool erAdmin = reader.Read();
            closeConnection();
            return erAdmin;
        }


        public List<UsersTableModel> getUserList() {
            List<UsersTableModel> utms = new List<UsersTableModel>();

            openConnection();
            cmd.CommandText = string.Format("select brugerId, createTs, password, salt, email, prim, erAdmin from users");
            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read()) {
                UsersTableModel utm = new UsersTableModel();
                utm.brugerId = reader.GetString("brugerId");
                utm.createTs = reader.GetDateTime("createTs");
                utm.password = reader.GetString("password");
                utm.salt = reader.GetString("salt");
                utm.email = reader.GetString("email");
                utm.primChar = reader.GetChar("prim");
                utm.erAdminChar = reader.GetChar("erAdmin");
                utms.Add(utm);
            }
            closeConnection();
            return utms;
        }


        public void createUser() {
            createUser(this);
        }
        public void createUser(UsersTableModel utm) {
            openConnection();

            string hash, salt;
            new Hash().getHashedPasswordAndSalt(utm.password, out salt, out hash);

            cmd.CommandText = string.Format(
                "insert into users(brugerId, createTs, password, salt, email, prim, erAdmin) " +
                "values('{0}', current_timestamp,'{1}','{2}','{3}','{4}','{5}');",
                utm.brugerId, hash, salt, utm.email, utm.primChar, utm.erAdminChar);
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();
        }
        public void deleteUser() {
            deleteUser(this);
        }

        public void updateUser(UsersTableModel utm) {
            openConnection();
            string hash, salt;
            new Hash().getHashedPasswordAndSalt(utm.password, out salt, out hash);

            cmd.CommandText = string.Format(
                "Update users " +
                "set password='{0}', salt='{1}', email='{2}', prim='{3}', erAdmin='{4}' " +
                "where brugerId='{5}'",
                hash, salt, utm.email, utm.primChar, utm.erAdminChar, utm.brugerId);
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();
        }

        public void updateUser() {
            updateUser(this);
        }

        public void deleteUser(string userId) {
            openConnection();

            cmd.CommandText = string.Format("delete from users where brugerId = '{0}'", userId);
            int cnt = cmd.ExecuteNonQuery();
            closeConnection();

        }
        public void deleteUser(UsersTableModel utm) {
            deleteUser(utm.brugerId);
        }

        public static implicit operator UsersTableModel(List<UsersTableModel> v) {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj) {
            if(obj == null)
                return false;
            UsersTableModel objAsUsersTableModel = obj as UsersTableModel;
            if(objAsUsersTableModel == null)
                return false;
            else
                return brugerId == objAsUsersTableModel.brugerId;

                    ;
        }

        public override int GetHashCode() {
            return 10;
        }

    }





    public class MySQLConnection : DbContext {
        public DbSet<UsersTableModel> UsersTableModel {
            get {
                DbSet<UsersTableModel> dbSet=new UsersDbSet<UsersTableModel>();
                /*
                UsersTableModel utm = new Models.UsersTableModel();
                utm.brugerId = "BrugerId";
                utm.email = "bruger@dk.dk";
                utm.erAdmin = true;
                dbSet.Add(utm);
                */
                //dbSet.Add(new Models.UsersTableModel());
                return dbSet;
            }
                 set {

                var x = value;
            }
        }

        public static implicit operator MySQLConnection(MySqlConnection v) {
            throw new NotImplementedException();
        }

        public System.Data.Entity.DbSet<Voresjazzklub.Models.ArranModel> Arrans { get; set; }

        public System.Data.Entity.DbSet<Voresjazzklub.Models.LogonModel> LogonModels { get; set; }

        public System.Data.Entity.DbSet<Voresjazzklub.Models.Tilmeldinger> Tilmeldingers { get; set; }

        public System.Data.Entity.DbSet<Voresjazzklub.Models.Billede> Billedes { get; set; }

        public System.Data.Entity.DbSet<Voresjazzklub.Models.BilledeArrang> BilledeArrangs { get; set; }
    }

    internal class UsersDbSet<T> : DbSet<UsersTableModel> {
    }
}