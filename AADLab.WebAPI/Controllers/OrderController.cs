using System;
using System.Threading.Tasks;
using System.Web.Http;
using AADLab.Common;

namespace AADLab.WebAPI.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : ApiController
    {
        private readonly IOrderRepository _repository;

        public OrderController()
        {
            _repository = new InMemoryRepository();
        }

        [Route("{userId}"), HttpGet]
        public async Task<IHttpActionResult> OrdersFor(string userId)
        {
            if (String.IsNullOrEmpty(userId)) return BadRequest("invalid user id");

            var orders = await _repository.OrdersBy(userId);
            return Ok(orders);
        }

        [Route("{userId}"), HttpPost]
        public async Task<IHttpActionResult> CreateFor(string userId, OrderRequest order)
        {
            if (String.IsNullOrEmpty(userId)) return BadRequest("invalid user id");
            if (order.Equals(OrderRequest.Empty)) return BadRequest("invalid order request");

            var orderId = userId.GetHashCode() + "-" + Guid.NewGuid();
            return Ok(await _repository.Save(new Order(orderId, userId)));
        }
    }
}