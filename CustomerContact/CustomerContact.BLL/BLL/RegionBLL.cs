using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerContact.DAL;

namespace CustomerContact.BLL
{
    public class RegionBLL: BaseBLL
    {
        public List<Region> GetAllRegions()
        {
            return context.Region.OrderBy(r => r.Name).ToList();
        }
    }
}
