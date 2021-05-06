using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class BasketCart
    {
        public BasketCart()
        {

        }
        public BasketCart(string username)
        {
            this.UserName = username;
        }
        public string UserName { get; set; }
        public List<BasketCartItem> Items { get; set; } = new List<BasketCartItem>();

        //calculate total price
        public decimal TotalPrice { 
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice = item.Price * item.Quantity;
                }
                return totalprice;
            }
        }

    }
}
