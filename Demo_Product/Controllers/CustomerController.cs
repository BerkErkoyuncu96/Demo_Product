using BuisnessLayer.Concrete;
using BuisnessLayer.FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo_Product.Controllers
{
    public class CustomerController : Controller
    {
        CustomerManager manager = new CustomerManager(new EntityFrameworkCustomerDal());
        public IActionResult Index()
        {
            var values = manager.GetCustomersListWithJob();
            return View(values);
        }
        [HttpGet]
        public IActionResult addCustomer()
        {
            JobManager jobManager = new JobManager(new EntityFrameworkJobDal());
            List<SelectListItem> values = (from x in jobManager.TGetList()
                                           select new SelectListItem
                                           {
                                               Text = x.JobName,
                                               Value = x.JobId.ToString()
                                           }).ToList();
            ViewBag.v= values;
            return View();
        }   
        [HttpPost]
        public IActionResult addCustomer(Customer customer)
        {
            CustomerValidator validationRules = new CustomerValidator();
            FluentValidation.Results.ValidationResult validationResult = validationRules.Validate(customer);
            if (validationResult.IsValid)
            {

                manager.TInsert(customer);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public IActionResult deleteCustomer(int Id)
        {
            var value = manager.TGetById(Id);
            manager.TDelete(value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult updateCustomer(int Id)
        {
            JobManager jobManager = new JobManager(new EntityFrameworkJobDal());
            List<SelectListItem> values = (from x in jobManager.TGetList()
                                           select new SelectListItem
                                           {
                                               Text = x.JobName,
                                               Value = x.JobId.ToString()
                                           }).ToList();
            ViewBag.v = values;
            var value = manager.TGetById(Id);
            return View(value);
        }
        [HttpPost]
        public IActionResult updateCustomer(Customer customer)
        {
            manager.TUpdate(customer);
            return RedirectToAction("Index");
        }
    }
}
