using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using WebDongTrung.Datas;
using WebDongTrung.DTO.Cart;

namespace WebDongTrung.Repositories
{
    public class CartRepo : ICart
    {
        private readonly StoreDbContex _contex;
        private readonly IMapper _mapper;
        public static int PageSize { get; set; } = 10;

        public CartRepo(StoreDbContex contex, IMapper mapper)
        {
            _contex = contex;
            _mapper = mapper;
        }
        public async Task<int> AddCartAsync(CartCreateDto cartCreate, string? username)
        {
            var cart = _mapper.Map<Cart>(cartCreate);
            cart.CreateId = username;
            cart.CreateAt = DateTime.Now;
            await _contex.Carts!.AddAsync(cart);
            await _contex.SaveChangesAsync();
            var idCart = cart.Id;
            foreach (var item in cartCreate.CartDetailModel)
            {
                var cartDetail = _mapper.Map<CartDetail>(item);
                cartDetail.IdCart = idCart;
                cartDetail.CreateId = username;
                cartDetail.CreateAt = DateTime.Now;
                await _contex.CartDetails!.AddAsync(cartDetail);
            }
            await _contex.SaveChangesAsync();
            return cart.Id;
        }

        public async Task DeleteCartAsync(int id)
        {
            var cart = _contex.Carts!.SingleOrDefault(c => c.Id == id);
            if (cart != null)
            {
                _contex.Carts!.Remove(cart);
                await _contex.SaveChangesAsync();
            }
        }

        public Task<GetAllCartDto> GetAllCartAsync(string? search, int? page = 1)
        {
            var allCart = _contex.Carts!.AsQueryable();
            //searching
            if (!string.IsNullOrEmpty(search))
            {
                allCart = allCart.Where(p => p.Phone!.Contains(search));
            }
            allCart = allCart.OrderByDescending(p => p.CreateAt);

            int totalCart = allCart.Count();
            int max_page = totalCart / PageSize;
            int remainder = totalCart % PageSize;
            max_page = max_page < 1 ? 1 : (remainder == 0 ? max_page : max_page + 1);

            if (page != null)
            {
                allCart = allCart.Skip((int)((page - 1) * PageSize)).Take(PageSize);
            }

            var result = new GetAllCartDto
            {
                MaxPage = max_page,
                Carts = allCart.Select(p => new Cart
                {
                    Id = p.Id,
                    TotalPrice = p.TotalPrice,
                    Status = p.Status,
                    Phone = p.Phone,
                    Email = p.Email,
                    Address = p.Address,
                    Payment = p.Payment,
                    PersonName = p.PersonName,
                    ReceivedDate = p.ReceivedDate
                }).ToList()
            };

            return Task.FromResult(result);
        }

        public async Task<Cart?> GetCartAsync(int id)
        {
            return await _contex.Carts!.FindAsync(id);
        }

        public async Task UpDateCartAsync(int id, CartCreateDto cartModel, string? username)
        {
            var cart = _mapper.Map<Cart>(cartModel);
            cart.Id = id;
            cart.UpdateAt = DateTime.Now;
            cart.UpdateId = username;
            _contex.Carts!.Update(cart);
            foreach (var item in cartModel.CartDetailModel)
            {
                var product = _contex.Products!.SingleOrDefault(p => p.Id == item.IdProduct);
                if (product != null)
                {
                    if (product.Discount != 0)
                    {
                        item.Quantity += item.Quantity / product.Discount;
                    }

                    if (cart.Status == 4) //trạng thái : đang giao hàng
                    {
                        product.RealityQuantity -= item.Quantity;
                    }
                    else if (cart.Status == 2) // trạng thái: giao hàng thành công
                    {
                        product.SystemQuantity -= item.Quantity;
                    }
                    else if (cart.Status == 3) //trạng thái : Hoàn đơn
                    {
                        product.RealityQuantity += item.Quantity;
                    }
                    _contex.Products!.Update(product);
                }
            }
            await _contex.SaveChangesAsync();
        }
        public async Task<Cart> UpdateStatusAsync(int id, string? userName, UpdateStatusDto status)
        {
            var cartQuery = await GetCartAsync(id);
            if (cartQuery == null)
            {
                return cartQuery;
            }
            cartQuery.UpdateId = userName;
            cartQuery.UpdateAt = DateTime.Now;
            JsonPatchDocument cartDocument = new();
            cartDocument.Operations.Add(new Operation
            {
                op = "replace",
                path = "Status",
                value = status.Status
            });
            cartDocument.ApplyTo(cartQuery);
            await _contex.SaveChangesAsync();
            return cartQuery;
        }
    }
}