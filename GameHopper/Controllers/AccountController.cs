using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GameHopper.Models;
using GameHopper.ViewModels;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Identity.Client;

namespace GameHopper.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                User user;

            if (model.Role == "GameMaster")
            {
                user = new GameMaster { UserName = model.Email, Email = model.Email };
            }
            else
            {
                user = new Player { UserName = model.Email, Email = model.Email };
            }

                if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        model.ProfilePicture.CopyTo(ms);
                        user.ProfilePicture = ms.ToArray();
                    }
                }

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
