using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerContact.DAL;

namespace CustomerContact.BLL
{
    public class CityBLL : BaseBLL
    {
        /// <summary>
        /// Return all Cities if RegionId is NOT defined, if RegionId is defined then filter by this Region
        /// </summary>
        /// <param name="RegionId"></param>
        /// <returns></returns>
        public List<City> GetAllCities(int RegionId=0)
        {
            return context.City.Where(c=> (RegionId==0 || c.RegionId==RegionId)).OrderBy(c => c.Name).ToList();
        }
    }
}
