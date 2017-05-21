using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Voresjazzklub.Models;


namespace VoresJazzklub.Controllers {
    public class SQLController : Controller {
        // GET: SQL
        public ActionResult Index() {
            return View();
        }

        /*
        private ActionResult sqlSelect(SQLmodel sqlModel) {
            sqlModel.selectScript();
            ViewBag.sql = sqlModel.sqlScript;
            ViewBag.res = sqlModel.sqlResult;
            ModelState.Clear();  // http://stackoverflow.com/questions/26062359/mvc-4-textbox-not-updating-on-postback
            return View(sqlModel);
        }
        private ActionResult sqlModify(SQLmodel sqlModel) {
            sqlModel.nonResultScript();
            ViewBag.sql = sqlModel.sqlScript;
            ViewBag.res = sqlModel.sqlResult;
            ModelState.Clear();  // http://stackoverflow.com/questions/26062359/mvc-4-textbox-not-updating-on-postback
            return View(sqlModel);
        }
        */

        [HttpPost]
        public ActionResult Index(SQLmodel sqlModel) {
            //return (sqlModel.sqlScript.StartsWith("select")) ? sqlSelect(sqlModel) : sqlModify(sqlModel);
            if(sqlModel.sqlScript.StartsWith("select"))
                sqlModel.selectScript();
            else
                sqlModel.nonResultScript();
            ViewBag.sql = sqlModel.sqlScript;
            ViewBag.res = sqlModel.sqlResult;
            sqlModel.sqlId++;
            ModelState.Clear();  // http://stackoverflow.com/questions/26062359/mvc-4-textbox-not-updating-on-postback
            return View(sqlModel);
        }
    }
}