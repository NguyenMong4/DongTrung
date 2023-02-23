using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories.CartClient
{
    public interface ICartClient
    {
        public Task<int> AddCartFromClientAsync(CartModel cartModel);
    }
}