using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voresjazzklub.Models;
using System.Collections.Generic;

namespace Voresjazzklub.Tests.Models {
    [TestClass]
    public class LogonModelTest {
        public void emptyTable() {
            List<LogonModel> logonModeller = new LogonModel().readList();
            foreach(LogonModel logonModel in logonModeller) {
                new LogonModel().delete(logonModel.brugerId);
            }
            Assert.IsTrue(new LogonModel().readList().Count == 0, "Tabellen er ikke tom");
        }

        string id1 = "AndersAnd";
        string id2 = "Andersine";
        public void createEntries() {
            new LogonModel(id1).create();
            new LogonModel(id2).create();
        }

        [TestMethod]
        public void create() {
            emptyTable();
            createEntries();
        }
        [TestMethod]
        public void read() {
            create();
            LogonModel logonModel = new LogonModel().read(id1);
            Assert.AreEqual(id1, logonModel.brugerId, "LogonModel skulle finde " + id1 + " men fandt " + logonModel.brugerId);
        }
        [TestMethod]
        public void readList() {
            create();
            List<LogonModel> logonModelListe = new LogonModel().readList();
            Assert.IsTrue(logonModelListe.Count == 2, "LogonModel readList skulle finde 2, men fik " + logonModelListe.Count);
        }
        [TestMethod]
        public void update() {
            create();
            LogonModel logonModel = new LogonModel(id2);
            logonModel.logonCnt = 37;
            logonModel.update();
            LogonModel logonModelNew = logonModel.read(id2);
            Assert.IsTrue(logonModel.logonCnt == logonModelNew.logonCnt, "LogonModel fejl ved update - " + logonModelNew.logonCnt);
        }
        [TestMethod]
        public void delete() {
            create();
            new LogonModel().delete(id1);
            List<LogonModel> logonModelListe = new LogonModel().readList();
            Assert.IsTrue(logonModelListe.Count == 1, "LogonModel " + logonModelListe.Count);

        }
    }
}
