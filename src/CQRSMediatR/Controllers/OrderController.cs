using CQRSMediatR.Commands;
using CQRSMediatR.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CQRSMediatR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateOrderRequestModel requestModel)
        {
            var response =await _mediator.Send(requestModel);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetOrderByIdRequestModel requestModel)
        {
            var response = await _mediator.Send(requestModel);
            return Ok(response);
        }
    }
}