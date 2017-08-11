using CustomerContact.Core.BO;
using CustomerContact.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerContact.BLL
{
    public class CustomerBLL: BaseBLL
    {
        /// <summary>
        /// Return all Customers or If sellerId != 0, than return all customers for that seller
        /// </summary>
        /// <param name="SellerId"></param>
        /// <returns></returns>
        public List<CustomerView> GetAllCustomers(int SellerId = 0)
        {


            dynamic obj = (from customer in context.Customer
                       join city in context.City on customer.CityId equals city.Id into g1
                       join gender in context.Gender on customer.GenderId equals gender.Id into g2
                       join classification in context.Classification on customer.ClassificationId equals classification.Id into g3
                       join seller in context.UserSys on customer.UserId equals seller.Id into g4
                       from joinCity in g1.DefaultIfEmpty()
                       from joinGender in g2.DefaultIfEmpty()
                       from joinClassification in g3.DefaultIfEmpty()
                       from joinSeller in g4.DefaultIfEmpty()
                       where SellerId == 0 || customer.UserId == SellerId
                       select new 
                       {
                           Id = customer.Id,
                           Name=customer.Name,
                           City = joinCity!=null?joinCity.Name:"none",
                           CityId = customer.CityId,
                           Classification = joinClassification!=null ? joinClassification.Name : "none",
                           ClassificationId = customer.ClassificationId,
                           Gender = joinGender !=null?  joinGender.Name : "none",
                           GenderId = customer.GenderId,
                           LastPurchase = customer.LastPurchase,
                           Phone = customer.Phone,
                           RegionId = customer.RegionId,
                           Region = joinCity !=null? joinCity.Region.Name : "none",
                           UserId = customer.UserId,
                           Seller = joinSeller!=null ? joinSeller.Login : "none"

                       }
                       ).AsEnumerable()
                       .Select(c=> new CustomerView
                       {
                           Id = c.Id,
                           Name = c.Name,
                           City = c.City,
                           CityId = c.CityId,
                           Classification = c.Classification,
                           ClassificationId = c.ClassificationId,
                           Gender = c.Gender,
                           GenderId = c.GenderId,
                           LastPurchase = c.LastPurchase.HasValue?c.LastPurchase.Value.ToString("MM/dd/yyyy"):string.Empty,
                           Phone = c.Phone,
                           RegionId = c.RegionId,
                           Region = c.Region,
                           UserId = c.UserId,
                           Seller = c.Seller
                       })
                       .OrderBy(c=>c.Classification).ToList();

            return obj;
        }


    }
}
