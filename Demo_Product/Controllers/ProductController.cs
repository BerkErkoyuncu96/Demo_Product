using BuisnessLayer.Concrete;
using BuisnessLayer.FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Demo_Product.Controllers
{
    public class ProductController : Controller
    {
        ProductManager productManager = new ProductManager(new EntityFrameworkProductDal());
        public IActionResult Index()
        {
            var values = productManager.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult addProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addProduct(Product product) 
        {
            ProductValidator validationRules = new ProductValidator();
            FluentValidation.Results.ValidationResult validationResult = validationRules.Validate(product);
            if(validationResult.IsValid)
            {
                productManager.TInsert(product);
                return RedirectToAction("Index");
            }
            else
            {
                foreach(var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();

        }
        public IActionResult deleteProduct(int id)
        {
            var value = productManager.TGetById(id);
            productManager.TDelete(value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult updateProduct(int Id) 
        {
            var value = productManager.TGetById(Id);
            return View(value);
        }
        [HttpPost]
        public IActionResult updateProduct(Product product)
        {
            productManager.TUpdate(product);
            return RedirectToAction("Index");
        }
    }
}
