using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Voresjazzklub.Models {
    public class Hash {
        string getHashSha512(string stringToBeHashed) {
            StringBuilder hashedString = new StringBuilder();
            foreach(byte hashedByte in new SHA512Managed().ComputeHash(Encoding.Unicode.GetBytes(stringToBeHashed))) {
                hashedString.Append(String.Format("{0:x2}", hashedByte));
            }
            return hashedString.ToString();
        }


        string getSalt() {
            StringBuilder salt = new StringBuilder();
            byte[] bytes = new byte[64];
            new RNGCryptoServiceProvider("").GetBytes(bytes);
            foreach(byte b in bytes) {
                salt.Append(String.Format("{0:x2}", b));
            }
            return salt.ToString();
        }

        public string getConstantSalt() { // anvendes i test, derfor public
            //string constantSaltFilename = "../../../Voresjazzklub/App_Data/ConstantSalt"; // c:\Vores..
            //string constantSaltFilename = @"C:\Users\u3249\Documents\Visual Studio 2015\Projects\Voresjazzklub\Voresjazzklub\App_Data\ConstantSalt";
            //string constantSaltFilename = "App_Data/ConstantSalt";
            string constantSaltFilename = @"C:\Users\Claus\Documents\Visual Studio 2017\Projects\Voresjazzklub\Voresjazzklub\App_Data\ConstantSalt";
            try {
                return System.IO.File.ReadAllText(constantSaltFilename);
            } catch(FileNotFoundException) { // så må vi håbe det ikke er fordi nogen har slettet den og ellers må du hellere finde din backup frem
                string salt = getSalt();
                System.IO.File.WriteAllText(constantSaltFilename, salt);
                return salt;
            }
        }

        public void getHashedPasswordAndSalt(string password, out string salt, out string hash) {
            salt = getSalt();
            hash = getHashSha512(getConstantSalt() + salt + password);
        }

        public string getHashedPassword(string password, string salt) {
            return getHashSha512(getConstantSalt() + salt + password);
        }

        bool areHashesIdentical(string hash1, string hash2) {
            int diff = hash1.Length ^ hash2.Length;
            for(int i = 0; i < hash1.Length && i < hash2.Length; i++) {
                diff |= hash1[i] ^ hash2[i];
            }
            return diff == 0;
        }

        public string createUser(string username, string password, string emailAddress) {
            return "Yaiy - det virkede";
            // "Teknisk fejl - prøv i morgen";
            // "Findes i forvejen - prøv at hedde noget andet";
        }

        public string changePassword(string username, string password) {
            return "Password changed";
            // "Det password har du brugt før - find på et andet"
            // "Teknisk fejl - prøv i morgen";
        }

        public string deleteUser(string username) {
            return "User deleted";
            // "Teknisk fejl - prøv i morgen";
        }

        bool areCredentialsValid(string username, string password) {
            return false;
            /*
             * read usercredential
             * if(not found) {
             *    password="";
             *    salt="";
             *    
             *                string hash;
            string newHash;
            getHashedPasswordAndSalt(password, out salt, out hash);
            newHash= getHashedPassword(password, salt);
            return areHashesIdentical(hash, newHash));

             * 
             */
        }

    }

}