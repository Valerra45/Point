using MediatR;
using Point.External.Core.Domain.Entity;
using Point.SharedKernel.Abstractions;
using Point.SharedKernel.Enums;
using Point.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Point.External.Infrastructure.Services.Orders.Commands
{
    public class UpdateOrderStatusCommand : IRequest<Guid>
    {
        public Guid Id { get; }

        public OrderStatus Status { get; }
        
        public UpdateOrderStatusCommand(Guid id, OrderStatus status)
        {
            Id = id;
            Status = status;
        }

    }

    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Guid>
    {
        private readonly IRepository<Order> _orderRepository;

        public UpdateOrderStatusCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetFirstWhere(x => x.OrderId == request.Id);

            if (order == null)
            {
                throw new EntityNotFoundException($"{nameof(Order)} with OrderId '{request.Id}' doesn't exist");
            }

            order.Status = request.Status;

            await _orderRepository.UpdateAsync(order);

            return order.Id;
        }
    }

}
