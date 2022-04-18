using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Point.External.Api.Models;
using Point.External.Core.Domain.Entity;
using Point.External.Infrastructure.Services.Orders.Commands;
using Point.External.Infrastructure.Services.Orders.Queryes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController
        : ControllerBase
    {
        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;

        public OrderController(IMediator mediatr,
            IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllAsync()
        {
            var response = await _mediatr.Send(new GetAllOrdersQuery());

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDto>> GetAsync(Guid id)
        {
            var respons = await _mediatr.Send(new GetOrderByIdQuery(id));

            return Ok(respons);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateAsync(OrderDto request)
        {
            var order = _mapper.Map<Order>(request);

            var id = await _mediatr.Send(new CreateOrderCommand(order));

            return CreatedAtAction(nameof(GetAsync), new { id = id }, null);
        }
    }
}
