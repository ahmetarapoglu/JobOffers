using JobOffers.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public object ApplayData { get; private set; }

        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);
            if (job==null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;
            return View(job);
        }

        [Authorize]
        public ActionResult Applay()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Applay(string Message)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];
            var check = db.ApplayForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();
            if (check.Count<1)
            {
                var job = new ApplayForJob();

                job.UserId = UserId;
                job.JobId = JobId;
                job.Message = Message;
                job.ApplayData = DateTime.Now;
                db.ApplayForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "The job has been successfully applied for it.";
            }else
            {
                ViewBag.Result = "You cannot apply for a job twice";
            }


            return View();
        }
        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplayForJobs.Where(a =>a.UserId==UserId);
            return View(Jobs.ToList());
        }
        [Authorize]
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplayForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            
            return View(job);
        }
        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserID = User.Identity.GetUserId();
            var Jobs = from app in db.ApplayForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserID
                       select app;
            var grouped = from j in Jobs
                          group j by j.job.JobTitle
                          into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr
                          };

            return View(grouped.ToList());

        }

        //Git :
        public ActionResult Edit(int id)
        {
            var job = db.ApplayForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        //post:
        [HttpPost]
        public ActionResult Edit(ApplayForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplayData = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);
        }

        // GET: /Delete/5
        public ActionResult Delete(int id)
        {
            var job = db.ApplayForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST:Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplayForJob job)
        {

                // TODO: Add delete logic here
                var myJob = db.ApplayForJobs.Find(job.Id);
                db.ApplayForJobs.Remove(myJob);
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}