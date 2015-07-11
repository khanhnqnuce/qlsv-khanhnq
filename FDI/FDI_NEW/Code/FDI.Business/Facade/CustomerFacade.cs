using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Business.Abstraction;
using FDI.DA.EndUser.Implementation;
using FDI.Memcached;
using FDI.Utils;

namespace FDI.Business.Facade
{
    public class CustomerFacade
    {
        public CustomerAbstraction customerDal;
        public CacheController cache;

        public CustomerFacade()
        {
            this.customerDal = new CustomerAbstraction(new CustomerImplement());
            this.cache = ConfigCache.EnableCache == 1 ? CacheController.GetInstance() : null;
        }

        static CustomerFacade() { }

        static readonly CustomerFacade uniqueInstance = new CustomerFacade();

        public static CustomerFacade InstanceCustomer
        {
            get { return uniqueInstance; }
        }
    }
}
