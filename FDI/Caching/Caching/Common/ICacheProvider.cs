using System;
using System.Collections;
using System.Linq;
using System.Runtime.Caching;
using Caching.Models;

namespace Caching.Common
{
    public interface ICacheProvider
    {
        object Get(string key);
        void Set(string key, object data, int cacheTime);
        bool IsSet(string key);
        void Invalidate(string key);
    }

    public class DefaultCacheProvider : ICacheProvider
    {
        private static ObjectCache Cache
        {
            get { return MemoryCache.Default; }
        }

        public object Get(string key)
        {
            return Cache[key];
        }

        public void Set(string key, object data, int cacheTime)
        {
            var policy = new CacheItemPolicy {AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime)};

            Cache.Add(new CacheItem(key, data), policy);
        }

        public bool IsSet(string key)
        {
            return (Cache[key] != null);
        }

        public void Invalidate(string key)
        {
            Cache.Remove(key);
        }
    }

    public interface IVehicleRepository
    {
        void ClearCache();
        IEnumerable GetVehicles();
    }

    public class VehicleRepository : IVehicleRepository
    {
        private CachingDemoEntities DataContext { get; set; }

        private ICacheProvider Cache { get; set; }

        public VehicleRepository()
            : this(new DefaultCacheProvider())
        {
        }

        public VehicleRepository(ICacheProvider cacheProvider)
        {
            this.DataContext = new CachingDemoEntities();
            this.Cache = cacheProvider;
        }

        public IEnumerable GetVehicles()
        {
            // First, check the cache
            var vehicleData = Cache.Get("vehicles") as IEnumerable;

            // If it's not in the cache, we need to read it from the repository
            if (vehicleData == null)
            {
                // Get the repository data
                vehicleData = DataContext.Vehicles.OrderBy(v => v.Name).ToList();

                //if (vehicleData.Any())
                //{
                // Put this data into the cache for 30 minutes
                Cache.Set("vehicles", vehicleData, 30);
                //}
            }

            return vehicleData;
        }

        public void ClearCache()
        {
            Cache.Invalidate("vehicles");
        }
    }
}