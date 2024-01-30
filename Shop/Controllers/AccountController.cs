using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;
using Shop.DBModel;

namespace Shop.Controllers
{
    public class AccountController : Controller
    {
        ShopEntities objUserDBEntities1 = new ShopEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            UserModel objUserModel = new UserModel();
            return View(objUserModel);
        }

        [HttpPost]
        public ActionResult Register(UserModel objUserModel)
        {
            if (ModelState.IsValid)
            {
                if (!objUserDbEntities.Users.Any(m => m.Email == objUserModel.Email))
                {
                    User objUser = new DBModel.User();
                    objUser.CreatedOn = DateTime.Now;
                    objUser.Email = objUserModel.Email;
                    objUser.FirstName = objUserModel.FirstName;
                    objUser.LastName = objUserModel.LastName;
                    objUser.Password = objUserModel.Password;
                    objUser.Role = "Admin";
                    objUserDbEntities.Users.Add(objUser);
                    objUserDbEntities.SaveChanges();
                    objUserModel = new UserModel();
                    objUserModel.SuccessMessage = "User is Successfully Added!";
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("Error", "Email is Alredy exists!");
                    return View();
                }

            }
            return View();
        }

        public ActionResult Login()
        {
            LoginModel objLoginModel = new LoginModel();
            return View(objLoginModel);
        }

        [HttpPost]
        public ActionResult Login(LoginModel objLoginModel)
        {
            if (ModelState.IsValid)
            {
                if (objUserDbEntities.Users.Where(m => m.Email == objLoginModel.Email && m.Password == objLoginModel.Password).FirstOrDefault() == null)
                {
                    ModelState.AddModelError("Error", "Wrong Email or Password.");
                    return View();
                }
                else
                {
                    Session["Email"] = objLoginModel.Email;
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}