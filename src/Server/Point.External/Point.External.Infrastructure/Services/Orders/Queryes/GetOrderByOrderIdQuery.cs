using MediatR;
using Point.External.Core.Domain.Entity;
using Point.SharedKernel.Abstractions;
using Point.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Point.External.Infrastructure.Services.Orders.Queryes
{
    public class GetOrderByOrderIdQuery : IRequest<Order>
    {
        public Guid Id { get; }

        public GetOrderByOrderIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetOrderByOrderIdQueryHandler : IRequestHandler<GetOrderByOrderIdQuery, Order>
    {
        private readonly IRepository<Order> _orderRepository;

        public GetOrderByOrderIdQueryHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(GetOrderByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetFirstWhere(x => x.OrderId == request.Id);

            if (order == null)
            {
                throw new EntityNotFoundException($"{nameof(Order)} with OrderId '{request.Id}' doesn't exist");
            }

            return order;
        }
    }
}
