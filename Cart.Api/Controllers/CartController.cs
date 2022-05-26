using Microsoft.AspNetCore.Mvc;
using Cart.Api.Models;
using Cart.Api.SQL;

namespace Cart.Api.Controllers
{
    [ApiController]
    [Route(template: "Cart")]
    public class CartController : ControllerBase
    {

        private readonly ILogger<CartController> _logger;
        private readonly ICartDataProvider _cartDataProvider;

        public CartController(ILogger<CartController> logger, ICartDataProvider cartDataProvider)
        {
            _cartDataProvider = cartDataProvider;
            _logger = logger;
        }

        [HttpGet(template: "{Id}")]
        public IEnumerable<CartDto> GetByUserId(int Id)
        {
            return _cartDataProvider.GetByUserId(Id);
        }

        [HttpPut(template: "Add")]
        public void Add(CartDto cart)
        {
            if (_cartDataProvider.CheckExist(cart))
                _cartDataProvider.Update(cart);
            else
                _cartDataProvider.Add(cart);
        }

        [HttpDelete(template:"Delete/{userId}/{productId}")]
        public void Delete(int userId, int productId)
        {
            _cartDataProvider.Delete(userId,productId);
        }

        [HttpGet(template:"List")]
        public IEnumerable<CartDto> GetList(int id)
        {
            
                return _cartDataProvider.GetByUserId(id);

            
        }
    }
}