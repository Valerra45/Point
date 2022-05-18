using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Point.Contracts;
using Point.External.Core.Domain.Entity;
using Point.External.Infrastructure.Services.Orders.Commands;
using Point.External.Infrastructure.Services.Orders.Queryes;
using Point.SharedKernel.DtoModels;
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
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderController(IMediator mediatr,
            IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _mediatr = mediatr;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllAsync()
        {
            var response = await _mediatr.Send(new GetAllOrdersQuery());

            return Ok(_mapper.Map<IEnumerable<OrderDto>>(response));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDto>> GetAsync(Guid id)
        {
            var respons = await _mediatr.Send(new GetOrderByIdQuery(id));

            return Ok(_mapper.Map<OrderDto>(respons));
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateAsync(OrderDto request)
        {
            var order = _mapper.Map<Order>(request);

            var id = await _mediatr.Send(new CreateOrderCommand(order));

            await _publishEndpoint.Publish<IOrderContract>(new
            {
                Message = JsonConvert.SerializeObject(request)
            });

            return CreatedAtAction(nameof(GetAsync), new { id = id }, null);
        }
    }
}
