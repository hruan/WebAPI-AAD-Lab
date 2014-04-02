using System.Web.Http;
using AADLab.Common;

namespace AADLab.WebAPI.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : ApiController
    {
        [Route("{userId}"), HttpGet]
        public IHttpActionResult OrdersFor(string userId)
        {
            return Ok(Util.GenerateOrderFor(userId, 15));
        }

        [Route("{userId}"), HttpPost]
        public IHttpActionResult CreateFor(OrderRequest order)
        {
            return Ok();
        }
    }
}