using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CustomerContact.Core.BO
{
    public class CustomerView
    {

        public int Id { get; set; }

       
        public string Name { get; set; }

       
        public string Phone { get; set; }

        public int GenderId { get; set; }
        public string Gender { get; set; }
        public int? CityId { get; set; }
        public string City { get; set; }

        public int? RegionId { get; set; }
        public string Region { get; set; }

        public string LastPurchase { get; set; }

        public int? ClassificationId { get; set; }
        public string Classification { get; set; }

        public int? UserId { get; set; }
        public string Seller { get; set; }
    }
}
