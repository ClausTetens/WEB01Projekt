using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Voresjazzklub.Models;

namespace Voresjazzklub.Controllers
{
    public class ViewDataUploadFilesResult {
        public string Name { get; set; }
        public int Length { get; set; }
    }

    public class UploadController : Controller {
        private string sessiondUserId() {
            return Session["userId"] == null ? null : Session["userId"].ToString();
        }


        private void theViewBag(long? id) {
            if(id != null) {
                ViewBag.arrangementId = id;
                ViewBag.arrangementBeskrivelse = new BilledeArrang().readMedArranId((long)id).arrangementBeskrivelse;
                //new ArrangementModel().read((long)id).arrangementBeskrivelse;
            }
        }

        // GET: Upload
        public ActionResult Index(long?id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                //return RedirectToAction("logIn", "UsersTableModels");
            }

            BilledeArrang billedeArrang = new BilledeArrang();
            if(id != null)
                billedeArrang.arrangementId = (long)id;

            theViewBag(id);
            //return View(new BilledeArrang().read());
            return View(billedeArrang);
        }


        private void savePostedFile(HttpPostedFileBase file) {
            if(file!=null && file.ContentLength > 0) {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/img/uploads"), fileName);
                file.SaveAs(path);
            }
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile, BilledeArrang billedeArrang, long?id) {  // http://localhost:60902/Upload/Index -- den virker
            if(sessiondUserId() == null || sessiondUserId() == "") {
                //return RedirectToAction("logIn", "UsersTableModels");
            }
            savePostedFile(postedFile);
            
            foreach(string upload in Request.Files) {
                if(!(Request.Files[upload] != null && Request.Files[upload].ContentLength > 0))
                        continue;
                
                if(ModelState.IsValid) {
                    HttpPostedFileBase file = Request.Files[upload];
                    if(file!=null && file.ContentLength > 0) {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/img/Upload"), fileName);
                        file.SaveAs(path);
                        ModelState.Clear();
                        ViewBag.Message = "Billedet er landet";
                        billedeArrang.billedAdresse = "/img/Upload/"+fileName;
                        if(id != null)
                            billedeArrang.arrangementId = (long)id;
                        if(sessiondUserId() != null)
                            billedeArrang.brugerId = sessiondUserId();
                        billedeArrang.create();
                    }
                }
            } 
            theViewBag(id);
            return View(billedeArrang);
            //return RedirectToAction("Index");
        }


        //http://stackoverflow.com/questions/27003305/html-beginform-multipart-form-data-file-upload-form-group-validation
        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file, BilledeArrang billedeArrang) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                //return RedirectToAction("logIn", "UsersTableModels");
            }

            foreach(string upload in Request.Files) {
                if(!(Request.Files[upload] != null && Request.Files[upload].ContentLength > 0))
                    continue;

                int memUsage_baseline_id = 0;
                int timingComp_baseline_id = 0;
                if(upload == "FileUploadMemoryUsage" || upload == "FileUploadResultsComparison") {
                    if(upload == "FileUploadMemoryUsage") {
                        if(Request.Params["memUsage_project"] == null || Request.Params["memUsage_project"] == "") {
                            ModelState.AddModelError("Project", "Please Select Project for Memory Usage");
                        } else {
                            memUsage_baseline_id = int.Parse(Request.Params["memUsage_project"]);
                        }
                    } else {
                        if(Request.Params["resultsComp_project"] == null || Request.Params["resultsComp_project"] == "") {
                            ModelState.AddModelError("Project", "Please Select Project for Timing Comparison");
                        } else {
                            timingComp_baseline_id = int.Parse(Request.Params["resultsComp_project"]);
                        }
                    }
                }

                HttpPostedFileBase file2 = Request.Files[upload];

                if(ModelState.IsValid) {
                    if(file2 == null) {
                        ModelState.AddModelError("File", "Please Upload Your file");
                    } else if(file2.ContentLength > 0) {
                        int MaxContentLength = 1024 * 1024 * 3; //3 MB
                        string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

                        if(!AllowedFileExtensions.Contains(file2.FileName.Substring(file2.FileName.LastIndexOf('.')))) {
                            ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                        } else if(file2.ContentLength > MaxContentLength) {
                            ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                        } else {
                            var fileName = Path.GetFileName(file2.FileName);
                            var path = Path.Combine(Server.MapPath("~/img/Upload"), fileName);
                            file2.SaveAs(path);
                            ModelState.Clear();
                            ViewBag.Message = "File uploaded successfully";
                        }
                    }
                }
            }
            theViewBag(billedeArrang.arrangementId);
            return View();
        }


        public ActionResult UploadFiles() {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                //return RedirectToAction("logIn", "UsersTableModels");
            }

            var r = new List<ViewDataUploadFilesResult>();

            foreach(string file in Request.Files) {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if(hpf.ContentLength == 0)
                    continue;
                string savedFileName = Path.Combine(
                   AppDomain.CurrentDomain.BaseDirectory,
                   Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);

                r.Add(new ViewDataUploadFilesResult() {
                    Name = savedFileName,
                    Length = hpf.ContentLength
                });
            }

            //return View("UploadedFiles", r);
            return View();
        }

    }
}