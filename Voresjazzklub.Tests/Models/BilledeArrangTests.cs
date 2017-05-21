using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voresjazzklub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voresjazzklub.Models.Tests {
    [TestClass()]
    public class BilledeArrangTests {
        [TestMethod()]
        public void createTest() {
            ArranModel arranModel = new ArranModel();
            arranModel.create();
            long arrId = 0;
            foreach(ArranModel am in arranModel.readList()) {
                if(am.arrangementId > arrId)
                    arrId = am.arrangementId;
            }

            BilledeArrang billedeArrang = new BilledeArrang();
            billedeArrang.arrangementBeskrivelse = "Et dejligt arrangement";
            billedeArrang.arrangementId = arrId; // skal være et rigtigt arrangement nu
            billedeArrang.billedAdresse = "HundenMarius.png";
            billedeArrang.brugerId = "Rip";
            billedeArrang.createTs = DateTime.Now;
            billedeArrang.kommentar = "Jæs, det kører";

            


            billedeArrang.delete(billedeArrang.billedAdresse);
            billedeArrang.create();
            BilledeArrang billedeArrangRead = billedeArrang.read(billedeArrang.billedAdresse);

            Assert.IsTrue(billedeArrang.arrangementId == billedeArrangRead.arrangementId, "forskel på det indsatte og det læste");
        }


    }
}