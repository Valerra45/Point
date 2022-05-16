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

namespace Point.External.Infrastructure.Services.Orders.Commands
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public DeleteOrderCommand(Guid id)
        {
            Id = id;
        }

        public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
        {
            private readonly IRepository<Order> _pointRepository;

            public DeleteOrderCommandHandler(IRepository<Order> pointRepository)
            {
                _pointRepository = pointRepository;
            }

            public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _pointRepository.GetByIdAsync(request.Id);

                if (order == null)
                {
                    throw new EntityNotFoundException($"{nameof(Order)} with id '{request.Id}' doesn't exist");
                }

                await _pointRepository.DeleteAsync(order);

                return true;
            }
        }
    }
}
