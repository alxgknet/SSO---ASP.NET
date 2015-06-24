using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SsincCommon.Models;
using System.Web.Security;

namespace SsincCommon.Controllers
{
    public class LoginController : Controller
    {
        //GET /Login/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return View();
            }

        }



    // POST: /Login/Login
		[AllowAnonymous]
		[HttpPost]
		public ActionResult Login(LoginModel model, string returnUrl)
		{
            try
            {
                if (ModelState.IsValid)
                {
                    //if (Membership.ValidateUser(model.UserName, model.Password))
                    if ((model.UserName.ToLower() == "user")&&(model.Password == "password"))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe.GetValueOrDefault());
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

			return View(model);
		}

        public ActionResult Logoff(string returnUrl)
        {
            FormsAuthentication.SignOut();
            if (returnUrl != null)
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }

    }

}