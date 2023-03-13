using AutoMapper;
using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public class CartDetailRepo : ICartDetail
    {
        private readonly StoreDbContex _contex;
        private readonly IMapper _mapper;

        public CartDetailRepo(StoreDbContex contex , IMapper mapper){
            _contex = contex;
            _mapper = mapper;
        }
        public Task<int> AddCartDetailAsync(int idCart, int idProduct, CartDetail cartDetail)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCartDetailAsync(int idCart, int idProduct)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartDetail>> GetAllCartDetailAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<CartDetailModel>> GetCartDetailAsync(int id)
        {
            var cartdetail =  _contex.CartDetails!.Join(_contex.Products!,c=>c.IdProduct, p => p.Id , (c,p)=> new {c,p}
            ).Select(c=> new CartDetailModel {
                Id = c.p.Id,
                IdCart = c.c.IdCart,
                Name = c.p.Name,
                Price = c.c.Price,
                Photo = c.p.Photo,
                Quantity = c.c.Quantity
            }).Where(c=>c.IdCart == id);
            return cartdetail.ToList();
        }

        public Task UpdateCartDetailAsync(CartDetail cartDetail)
        {
            throw new NotImplementedException();
        }
    }
}