using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerContact.BLL;
using CustomerContact.DAL;
using CustomerContact.Web;
using CustomerContact.Web.Classes;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using CustomerContact.Core.Models;

namespace CustomerContact.Web.Controllers
{
  public class AccountController : baseController
  {
    // GET: Users
    public ActionResult Login()
    {
      LoginModel model = new LoginModel();
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Login(LoginModel model)
    {
      //if (emailPassword)
      //  return EmailPassword(email);
      if (ModelState.IsValid)
      {
        UserSys user = null;

        using (UserBLL service = new UserBLL())
        {
          user = service.Login(model.email, model.password);
        }


        if (user == null)
        {
          ViewData["ErrorMessage"] = "The e-mail and/or password entered is invalid. Please try again.";
          ModelState.AddModelError("email", "");
          ModelState.AddModelError("password", "");
          return View(model);
        }

        AppUserState appUserState = new AppUserState()
        {
          Email = user.Email,
          Name = user.Login,
          UserId = user.Id,
          IsAdmin = user.Role.IsAdmin
        };

        IdentitySignin(appUserState, null, false);

        //if (!string.IsNullOrEmpty(returnUrl))
        //  return Redirect(returnUrl);

        return RedirectToAction("Index", "Customer", null);
      }

      return View(model);
     
    }

    public ActionResult LogOff()
    {
      IdentitySignout();
      return RedirectToAction("Login");
    }

    #region SignIn and Signout

    /// <summary>
    /// Helper method that adds the Identity cookie to the request output
    /// headers. Assigns the userState to the claims for holding user
    /// data without having to reload the data from disk on each request.
    /// 
    /// AppUserState is read in as part of the baseController class.
    /// </summary>
    /// <param name="appUserState"></param>
    /// <param name="providerKey"></param>
    /// <param name="isPersistent"></param>
    public void IdentitySignin(AppUserState appUserState, string providerKey = null, bool isPersistent = false)
    {
      var claims = new List<Claim>();

      // create *required* claims
      claims.Add(new Claim(ClaimTypes.NameIdentifier, appUserState.UserId.ToString()));
      claims.Add(new Claim(ClaimTypes.Name, appUserState.Name));

      // serialized AppUserState object
      claims.Add(new Claim("userState", appUserState.ToString()));

      var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

      // add to user here!
      AuthenticationManager.SignIn(new AuthenticationProperties()
      {
        AllowRefresh = true,
        IsPersistent = isPersistent,
        ExpiresUtc = DateTime.UtcNow.AddDays(1)
      }, identity);
    }

    public void IdentitySignout()
    {
      AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
          DefaultAuthenticationTypes.ExternalCookie);
    }

    private IAuthenticationManager AuthenticationManager
    {
      get { return HttpContext.GetOwinContext().Authentication; }
    }

    #endregion
  }
}