using Demo_Product.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo_Product.Controllers
{
    public class SettingsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public SettingsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditViewModel model = new UserEditViewModel();
            model.Name = values.Name;
            model.Surname = values.Surname;
            model.Mail = values.Surname;
            model.Gender = values.Gender;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel userEditView)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.Name = userEditView.Name;
            user.Surname = userEditView.Surname;
            user.Email = userEditView.Mail;
            user.Gender = userEditView.Gender;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userEditView.Password);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Customer");
            }
            else
            {
                //Error Messages
            }
            return View();
        }
    }
}
