using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voresjazzklub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voresjazzklub.Models.Tests {
    [TestClass()]
    public class HashTests {
        [TestMethod()]
        public void getConstantSaltTest() {
            
            Assert.IsTrue(new Hash().getConstantSalt().Length==128, "Saltet er ikke den rigtige længde: "+ new Hash().getConstantSalt().Length);
        }

        [TestMethod()]
        public void createUserTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void changePasswordTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void deleteUserTest() {
            Assert.Fail();
        }
    }
}