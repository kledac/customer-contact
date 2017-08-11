using CustomerContact.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerContact.Web.Classes
{
  public class AppUserState
  {
    public string Name = string.Empty;
    public string Email = string.Empty;
    public string Theme = "visualstudio";
    public int UserId = 0;
    public bool IsAdmin = false;


    /// <summary>
    /// Exports a short string list of Id, Email, Name separated by |
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return string.Join("|", new string[] { this.UserId.ToString(), this.Name, this.IsAdmin.ToString(), this.Theme });
    }


    /// <summary>
    /// Imports Id, Email and Name from a | separated string
    /// </summary>
    /// <param name="itemString"></param>
    public bool FromString(string itemString)
    {
      if (string.IsNullOrEmpty(itemString))
        return false;

      string[] strings = itemString.Split('|');
      if (strings.Length < 3)
        return false;

      UserId = Convert.ToInt32(strings[0]);
      Name = strings[1];
      IsAdmin = strings[2] == "True";
      if (strings.Length > 3)
        Theme = strings[3];

      return true;
    }

    /// <summary>
    /// Populates the AppUserState properties from a
    /// User instance
    /// </summary>
    /// <param name="user"></param>
    public void FromUser(UserSys user)
    {
      UserId = user.Id;
      Name = user.Login;
      Email = user.Email;
      IsAdmin = user.Role.IsAdmin;
    }

    /// <summary>
    /// Determines if the user is logged in
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
      if (this.UserId!=0)
        return true;

      return false;
    }
  }
}