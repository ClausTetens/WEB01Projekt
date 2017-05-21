using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Voresjazzklub.Models;

namespace Voresjazzklub.Tests.Models {
    [TestClass]
    public class ModelsTest {
        static string andersAndEmail = "a.and@andeby.utopia";
        static string andersAndEmailNew = "a.and@andeby.net";
        UsersTableModel rip = new UsersTableModel("R.I.P", "ripPW", "ripSalt", "rip.and@andeby.utopia", 'T', 'F');
        UsersTableModel rap = new UsersTableModel("Rap", "rapPW", "rapSalt", "rap.and@andeby.utopia", 'T', 'F');
        UsersTableModel rup = new UsersTableModel("Rup", "rupPW", "rupSalt", "rup.and@andeby.utopia", 'T', 'T');
        UsersTableModel andersAnd = new UsersTableModel("AndersAnd", "PasswordDerSkalHashes", "medNogetSalt", andersAndEmail , 'T', 'F');

        private void emptyTable() {
            UsersTableModel usersTableModel = new UsersTableModel();
            List<UsersTableModel> utms = usersTableModel.getUserList();
            foreach(UsersTableModel utm in utms) {
                usersTableModel.deleteUser(utm);
            }
        }

        private void isUserListEmpty() {
            UsersTableModel usersTableModel = new UsersTableModel();
            List<UsersTableModel> utms = usersTableModel.getUserList();
            Assert.IsTrue(utms.Count == 0,"Users skulle være tom. Der er "+ utms.Count);
        }

        private void createFourEntries() {
            emptyTable();
            isUserListEmpty();

            rip.createUser();
            rap.createUser();
            rup.createUser();
            andersAnd.createUser();

            UsersTableModel usersTableModel = new UsersTableModel();
            List<UsersTableModel> utms = usersTableModel.getUserList();
            Assert.IsTrue(utms.Count == 4, "Der skulle være 4 personer i Users. Der er " + utms.Count);
        }
        private void readEntry() {
            UsersTableModel usersTableModel = new UsersTableModel();
            string brugerId = "AndersAnd";
            usersTableModel.getUser(brugerId);
            Assert.AreEqual(brugerId, usersTableModel.brugerId, "Kan ikke finde " + brugerId);
        }
        private void readList() {
            UsersTableModel usersTableModel = new UsersTableModel();
            List<UsersTableModel> utms = usersTableModel.getUserList();
            Assert.IsTrue(utms.Count == 4, "Der skulle være 4 personer i Users. Der er " + utms.Count);

            Assert.IsTrue(utms.Contains(rip), "Rip er blevet væk");
            Assert.IsTrue(utms.Contains(rap), "Rap er blevet væk");
            Assert.IsTrue(utms.Contains(rup), "Rup er blevet væk");
        }
        private void updateEmail() {
            UsersTableModel usersTableModel = new UsersTableModel();
            string brugerId = "AndersAnd";
            usersTableModel.getUser(brugerId);
            Assert.AreEqual(brugerId, usersTableModel.brugerId, "Kan ikke finde " + brugerId);

            Assert.AreEqual(andersAndEmail, usersTableModel.email, "Forkert email " + usersTableModel.email);
            usersTableModel.email = andersAndEmailNew;
            usersTableModel.updateUser();
            usersTableModel.email = null;
            usersTableModel.getUser(brugerId);
            Assert.AreEqual(andersAndEmailNew, usersTableModel.email, "Forkert email " + usersTableModel.email);
        }
        private void deleteEntry() {
            UsersTableModel usersTableModel = new UsersTableModel();
            List<UsersTableModel> utms = usersTableModel.getUserList();
            rip.deleteUser();
            utms = usersTableModel.getUserList();
            Assert.IsTrue(utms.Count == 3, "Der skulle være 3 personer i Users. Der er " + utms.Count);
            Assert.IsFalse(utms.Contains(rip), "Rip er ikke smuttet ud");
        }
        [TestMethod]
        public void TestUsersCRUD() {
            createFourEntries();
            readEntry();
            readList();
            updateEmail();
            deleteEntry();
        }

        [TestMethod]
        public void TestIsAdmin() {
            createFourEntries();
            UsersTableModel usersTableModel = new UsersTableModel();
            Assert.IsFalse(usersTableModel.isAdministrator("Rap"), "Ih, altså! Rap skulle ikke have været administrator :(");
            Assert.IsTrue(usersTableModel.isAdministrator("Rup"), "Rup skulle have været administrator. Fix it!");
        }
        [TestMethod]
        public void changePassword() {
            new UsersTableModel().changeAllPasswords();
        }

        /*
        public void TestGetUserList() {
            UsersTableModel usersTableModel = new UsersTableModel();
            List<UsersTableModel> utms = usersTableModel.getUserList();
            Assert.IsTrue(utms.Count==0);
        }

        public void TestCreateUser() {

        }*/

    }
}
