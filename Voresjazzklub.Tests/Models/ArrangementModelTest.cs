using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voresjazzklub.Models;
using System.Collections.Generic;

namespace Voresjazzklub.Tests.Models {
    [TestClass]
    public class ArrangementModelTest {
        
        public void emptyTable() {
            List<ArrangementModel> arrangementer = new ArrangementModel().readList();
            foreach(ArrangementModel arrangement in arrangementer) {
                new ArrangementModel().delete(arrangement.arrangementId);
            }
            Assert.IsTrue(new ArrangementModel().readList().Count == 0, "Tabellen er ikke tom");
        }
        
        public void createEntries() {
            new ArrangementModel("Vinterjazz", "http://jazz.dk/vinterjazz-2017/program/", DateTime.Parse("2017-02-03"), DateTime.Parse("21:00:00"), "Sporvejene", DateTime.Parse("17:00:00")).create();
            new ArrangementModel("Vinterjazz", "se avisen", DateTime.Parse("2017-02-27"), DateTime.Parse("19:00:00"), "Sporvejene", DateTime.Parse("13:30:00")).create();
        }

        
        public void checkLast(string web) {
            List<ArrangementModel> arrangementer = new ArrangementModel().readList();
            long id = 0;
            foreach(ArrangementModel arrang in arrangementer) {
                id=arrang.arrangementId;
            }
            ArrangementModel arrangement = new ArrangementModel().read(id);
            Assert.IsTrue(arrangement.arrangementWeb == web, "Forkert info " +arrangement.arrangementWeb);
        }
        
        public void readEntry() {
            checkLast("se avisen");
        }

        
        public void updateEntry() {
            string after = "http://jazz.dk/cphjazz/forside/";
            List<ArrangementModel> arrangementer = new ArrangementModel().readList();
            long id = 0;
            foreach(ArrangementModel arrang in arrangementer) {
                id = arrang.arrangementId;
            }
            ArrangementModel arrangement = new ArrangementModel().read(id);
            arrangement.arrangementWeb = after;
            arrangement.update();
            checkLast(after);
        }

        [TestMethod]
        public void TestArrangementCRUD() {
            emptyTable();
            createEntries();
            readEntry();
            updateEntry();
        }
    }
}