using Basket.API.Data.Interfaces;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _context;

        public BasketRepository(IBasketContext basketContext)
        {
            _context = basketContext;
        }

        public async Task<BasketCart> GetBasketCart(string userName)
        {
            var basket = await _context.Redis.StringGetAsync(userName);
            if(basket.IsNullOrEmpty)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<BasketCart>(basket);
        }
        public async Task<bool> DeleteBasket(string userName)
        {
            return await _context.Redis.KeyDeleteAsync(userName);
        }
        public async Task<BasketCart> UpdateBasket(BasketCart basketCart)
        {
            var update = await _context.Redis.StringSetAsync(basketCart.UserName,JsonConvert.SerializeObject(basketCart));
            if(!update)
            {
                return null;
            }

            return await GetBasketCart(basketCart.UserName);
        }
    }
}
