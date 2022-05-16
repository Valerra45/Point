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
    public class UpdateOrderCommand : IRequest<Guid>
    {
        public Order Order { get; }

        public UpdateOrderCommand(Order order)
        {
            Order = order;
        }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Guid>
    {
        private readonly IRepository<Order> _orderRepository;

        public UpdateOrderCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderRepository.UpdateAsync(request.Order);

            return request.Order.Id; 
        }
    }
}
