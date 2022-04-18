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
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public Guid Id { get; }
        
        public GetOrderByIdQuery(Guid id)
        {
            Id  = id;
        }
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly IRepository<Order> _orderRepository;

        public GetOrderByIdQueryHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = _orderRepository.GetByIdAsync(request.Id);

            if (order == null)
            {
                throw new EntityNotFoundException($"{nameof(Order)} with id '{request.Id}' doesn't exist");
            }

            return order;
        }
    }
}
