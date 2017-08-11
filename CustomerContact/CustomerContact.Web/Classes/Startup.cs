using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;

namespace CustomerContact.Web.Classes
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      ConfigureAuth(app);
    }

    // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
    public void ConfigureAuth(IAppBuilder app)
    {
      // Enable the application to use a cookie to store information for the signed in user
      app.UseCookieAuthentication(new CookieAuthenticationOptions
      {
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        LoginPath = new PathString("/Account/Login")
      });


      app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

      // App.Secrets is application specific and holds values in CodePasteKeys.json
      // Values are NOT included in repro – auto-created on first load
     
      AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
    }
  }
}