using CustomerContact.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerContact.BLL
{
    public class ClassificationBLL : BaseBLL
    {
        public List<Classification> GetAllClassifications()
        {
            return context.Classification.OrderBy(c => c.Name).ToList();
        }
    }
}
