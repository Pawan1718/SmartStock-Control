using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Data;
using SmartInventory.Models;
using SmartInventory.ViewModels;
using System.Security.Claims;
using System.Text;

namespace SmartInventory.Controllers
{
    public class AccessController : Controller
    {
        private readonly imsDbContext context;

        public AccessController(imsDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {          
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(UserRegistration model)
        {
            if (ModelState.IsValid)
            {
                bool isUserData = context.Accesses.Any(x=>x.EmailId == model.EmailId);
                if (isUserData)
                {
                    TempData["warning"] = "Username is already taken. Please choose a different username.";
                }
                else
                {
                    var data = new Access()
                    {
                        Name = model.Name,
                        EmailId = model.EmailId,
                        Mobile = model.Mobile,
                        Password = EncryptPassword(model.Password),
                        ConfirmPassword = model.ConfirmPassword,
                    };
                    context.Accesses.Add(data);
                    context.SaveChanges();
                    TempData["success"] = "Registration successful. Welcome! You can now log in.";
                    return RedirectToAction("Login");
                }
              
            }
            else
            {
                TempData["error"]="Please fill out all required fields before submitting the registration form.";
                return View(model);
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var data = context.Accesses.FirstOrDefault(x => x.EmailId == model.userName);
                if (data != null)
                {
                    bool isValid = (data.EmailId == model.userName && DecryptPassword( data.Password) == model.Password);
                    if (isValid) 
                    {
                        var identity= new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name,model.userName)},
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal=new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
                        HttpContext.Session.SetString("userName",model.userName);
                        TempData["success"] = "Login successful. Welcome!";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["error"] = "Incorrect username or password. Please try again.";
                        return View(model);
                    }
                }
                else
                {
                    TempData["error"] = "Username not found.";
                    return View(model);
                }
            }
            return View();      
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Access");
         
        }




        public static string EncryptPassword(string Password)
        {
            if (string.IsNullOrEmpty(Password))
            {
                return null;
            }
            else
            {
                byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(Password);
                string encryptedPassword = Convert.ToBase64String(storePassword);
                return encryptedPassword;
            }
        }
        public static string DecryptPassword(string Password)
        {
            if (string.IsNullOrEmpty(Password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPassword =  Convert.FromBase64String(Password);
                string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
                return decryptedPassword;
            }

       
        }
    }
}
