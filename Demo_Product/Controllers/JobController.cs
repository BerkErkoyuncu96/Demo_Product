using BuisnessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Demo_Product.Controllers
{
    public class JobController : Controller
    {
        JobManager jobManager = new JobManager(new EntityFrameworkJobDal());
        public IActionResult Index()
        {
            var values = jobManager.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult addJob()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addJob(Jobs job) 
        {
            jobManager.TInsert(job);
            return RedirectToAction("Index");
        }

        public IActionResult deleteJob(int Id) 
        {
            var value = jobManager.TGetById(Id);
            jobManager.TDelete(value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult updateJob(int Id) 
        {
            var value = jobManager.TGetById(Id);
            return View(value);
        }
        [HttpPost]
        public IActionResult updateJob(Jobs job)
        {
            jobManager.TUpdate(job);
            return RedirectToAction("Index");
        }
    }
}
