using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerContact.DAL;

namespace CustomerContact.BLL
{
    public class BaseBLL : IDisposable
    {
        protected DataModel context;
        
        public BaseBLL()
        {
            context = new DataModel();
        }

        protected virtual void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
