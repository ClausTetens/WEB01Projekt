using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Voresjazzklub.Models.Tests {
    [TestClass()]
    public class HashTests {
        [TestMethod()]
        public void getConstantSaltTest() {
            Assert.IsTrue(new Hash().getConstantSalt().Length==128, "Saltet er ikke den rigtige længde: "+ new Hash().getConstantSalt().Length);
        }
    }
}



