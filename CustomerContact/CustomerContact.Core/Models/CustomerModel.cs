using CustomerContact.Core.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CustomerContact.Core.Models
{
    public class CustomerModel
    {

        public List<CustomerView> customers { get; set; }

      

        public int genderIdFilter { get; set; }
        public SelectList genderFilter { get; set; }


        public int cityIdFilter { get; set; }
        public SelectList cityFilter { get; set; }

        public int regionIdFlter { get; set; }
        public SelectList regionFilter { get; set; }

        public int classificationIdFilter { get; set; }
        public SelectList classificationFilter { get; set; }

        public int sellerIdFilter { get; set; }
        public SelectList sellerFilter { get; set; }

    }
}
