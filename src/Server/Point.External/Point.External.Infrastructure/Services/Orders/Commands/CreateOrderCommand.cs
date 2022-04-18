using MediatR;
using Point.External.Core.Domain.Entity;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Point.External.Infrastructure.Services.Orders.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Order Order { get; set; }

        public CreateOrderCommand(Order order)
        {
            Order = order;
        }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IRepository<Order> _orderRepository;

        public CreateOrderCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderRepository.AddAsync(request.Order);

            return request.Order.Id;
        }
    }
}
