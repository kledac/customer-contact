using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerContact.BLL;
using CustomerContact.DAL;
using CustomerContact.Core.Models;
using CustomerContact.Core.BO;

namespace CustomerContact.Web.Controllers
{
  public class CustomerController : baseController
  {
    [Authorize]
    // GET: Customer
    public ActionResult Index()
    {

      CustomerModel model = new CustomerModel();

 
      using (CustomerBLL customerService = new CustomerBLL())
      {

        model.customers = customerService.GetAllCustomers(this.AppUserState.IsAdmin ? 0 : this.AppUserState.UserId);
        var all = new { Id = 0, Name = "All" };
        var genders = model.customers.Select(a => new { Id = a.GenderId, Name = a.Gender }).Distinct().ToList();
        genders.Insert(0, all);
        model.genderFilter = new SelectList(genders, "Id", "Name");

        var sellers = model.customers.Where(a=>a.UserId.HasValue).Select(a => new { Id = a.UserId.Value, Name = a.Seller }).Distinct().ToList();
        sellers.Insert(0, all);
        model.sellerFilter = new SelectList(sellers, "Id", "Name");

        var classification = model.customers.Where(a=>a.ClassificationId.HasValue).Select(a => new { Id = a.ClassificationId.Value, Name = a.Classification }).Distinct().ToList();
        classification.Insert(0, all);
        model.classificationFilter = new SelectList(classification, "Id", "Name");
      }

      /*Region was done not chaining the Customer data just to show that is possible to created filter independently
      of the returned data.
        Ideally this should be done on the Core Level for bigger databases, opening the connection only once
      */
      using (RegionBLL service = new RegionBLL())
      {
        var regions = service.GetAllRegions();
        regions.Insert(0, new Region() { Id = 0, Name = "All" });
        model.regionFilter = new SelectList(regions, "Id", "Name");
      }







      model.cityFilter = new SelectList(new[] { new { name = "All", value = "" } });



      return View(model);
    }

    [HttpPost]
    public JsonResult GetCity(int regionId)
    {

      List<CityView> cities = new List<CityView>();
      cities.Add(new CityView() { Id = 0, Name = "All", RegionId = 0 });
      
      if (regionId != 0)
      {
        using (CityBLL service = new CityBLL())
        {
          cities.AddRange(service.GetAllCities(regionId).OrderBy(a=>a.Name).Select(a => new CityView() { Id = a.Id, Name = a.Name, RegionId = a.RegionId }));
        }
      }
      return Json(cities);
    }
  }
}