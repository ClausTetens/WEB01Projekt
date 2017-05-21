using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VoresJazzklub.Controllers;
using System.Web.Mvc;
using Voresjazzklub.Models;

namespace Voresjazzklub.Tests.Controllers {
    [TestClass]
    public class SQLControllerTest {

        public static String GetTimestamp(DateTime value) {
            //return value.ToString("yyyyMMddHHmmssffff");
            return value.ToString("dd-MM-yyyy HH:mm:ss");
        }

        [TestMethod]
        public void hulIgennemTilDB() {
            string before = "selectScript() er kørt, følgende fundet:\n" + GetTimestamp(DateTime.Now) + "\n";
            SQLController controller = new SQLController();
            SQLmodel sqlModel = new SQLmodel();
            sqlModel.sqlId = 42;
            sqlModel.sqlResult = "";
            sqlModel.sqlScript = "select current_timestamp";
            ViewResult result = controller.Index(sqlModel) as ViewResult;
            Assert.IsNotNull(result);
            string res = ((SQLmodel)(result.Model)).sqlResult;
#if DEBUG
            string after = "selectScript() er kørt, følgende fundet:\n" + GetTimestamp(DateTime.Now) + "\n";
            int c0 = before.CompareTo(res);
            int c1 = res.CompareTo(after);
            Assert.IsTrue(c0 <= 0, "c0 fejl " + c0 + "" + before + " " + after);
            Assert.IsTrue(c1 <= 0, "c1 fejl " + c1 + " " + res + " " + after);
#else 
            Assert.IsTrue(res.Length == 0,"Not debug. Result>>"+result+"<<");
#endif
        }
    }
}
