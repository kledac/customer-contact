using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;
using System.Text;
using System.Collections;
using System.Linq;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using CustomerContact.Web.Classes;

namespace CustomerContact.Web.Controllers
{
  public class baseController : Controller
  {
    /// <summary>
    /// Contains User state information retrieved from the authentication system
    /// </summary>
    protected AppUserState AppUserState = new AppUserState();

    /// <summary>
    /// ErrorDisplay control that holds page level error information
    /// </summary>


    protected override void Initialize(RequestContext requestContext)
    {
      base.Initialize(requestContext);

      // Grab the user's login information from Identity
      AppUserState appUserState = new AppUserState();
      if (User is ClaimsPrincipal)
      {
        var user = User as ClaimsPrincipal;
        var claims = user.Claims.ToList();

        var userStateString = GetClaim(claims, "userState");
        //var name = GetClaim(claims, ClaimTypes.Name);
        //var id = GetClaim(claims, ClaimTypes.NameIdentifier);

        if (!string.IsNullOrEmpty(userStateString))
          appUserState.FromString(userStateString);
      }
      AppUserState = appUserState;

      ViewData["UserState"] = AppUserState;
    }

    public static string GetClaim(List<Claim> claims, string key)
    {
      var claim = claims.FirstOrDefault(c => c.Type == key);
      if (claim == null)
        return null;

      return claim.Value;
    }


    /// <summary>
    /// Allow external initialization of this controller by explicitly
    /// passing in a request context
    /// </summary>
    /// <param name="requestContext"></param>
    public void InitializeForced(RequestContext requestContext)
    {
      Initialize(requestContext);
    }

    


  }
}