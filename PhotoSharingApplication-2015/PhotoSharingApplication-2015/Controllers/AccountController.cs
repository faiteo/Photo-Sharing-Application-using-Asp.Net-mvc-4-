using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PhotoSharingApplication_2015.Models;

namespace PhotoSharingApplication_2015.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
  

       [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
           //check if the model state is valid 
           if (ModelState.IsValid)
            {
               //check user credentials 
               if (Membership.ValidateUser(model.UserName, model.Password))
                {
                   //authenticate user 
                   FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    
                   if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else 
                    {
 
                       return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                   //if the user credentials are incorrect, add a model error tothe Modestate object 
                   ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
                
            }
            return View(model);

        }


       public ActionResult LogOff() 
       {
           FormsAuthentication.SignOut();
           return RedirectToAction("Index", "Home");
       }

       public ActionResult Register()
       {
           return View();
       }

       [HttpPost]
       public ActionResult Register(RegisterModel model)
       {
           if (ModelState.IsValid)
           {
               try
               {

                   MembershipUser NewUser = Membership.CreateUser(model.UserName, model.Password);
                   FormsAuthentication.SetAuthCookie(model.UserName, false);
                   return RedirectToAction("Index", "Home");
               }
               catch (MembershipCreateUserException e)
               {

                   ModelState.AddModelError("Registration Error", "Registration error: " + e.StatusCode.ToString());
               }
           }
           return View(model);
       }

        

    }
}
