using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Models;
using System.Text;

namespace ShoppingPlanApi.Cache
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisCacheController : ControllerBase
    {
        private readonly IDistributedCache distributedCache;
        private readonly IShoppingPlan<ShoppingList> _shoppingPlan;
        public RedisCacheController( IDistributedCache distributedCache,IShoppingPlan<ShoppingList> shoppingPlan)
        {
           
            this.distributedCache = distributedCache;
            _shoppingPlan = shoppingPlan;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<ShoppingList>> GetRedis([FromQuery] int id)
        {
            string cacheKey = id.ToString();
            IEnumerable<ShoppingList> cities;
            string json;

            var citiesFromCache = await distributedCache.GetAsync(id.ToString());
            if (citiesFromCache != null)
            {
                json = Encoding.UTF8.GetString(citiesFromCache);
                cities = JsonConvert.DeserializeObject<List<ShoppingList>>(json);
                return cities;
            }
            else
            {
                List<ShoppingList> tempList = _shoppingPlan.GetAll().ToList();

                json = JsonConvert.SerializeObject(tempList);
                citiesFromCache = Encoding.UTF8.GetBytes(json);
                var options = new DistributedCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromDays(1)) // belirli bir süre erişilmemiş ise expire eder
                        .SetAbsoluteExpiration(DateTime.Now.AddMonths(1)); // belirli bir süre sonra expire eder.
                await distributedCache.SetAsync(cacheKey, citiesFromCache, options);
                return tempList;
            }
        }


        [HttpPost]
        [Route("DeleteRedisCashe")]
        public ActionResult DeleteCache(int id)
        {
            // remove cashe
            distributedCache.Remove(id.ToString());
            return Ok();
        }
    }
}
