using MassTransit;
using MediatR;
using Point.Contracts;
using Point.External.Core.Domain.Entity;
using Point.External.Infrastructure.Services.Orders.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Infrastructure.Consumers
{
    public class ExternalUpdateOrderStatusConsumer : IConsumer<IOrderStatusContract>
    {
        private readonly IMediator _mediatr;

        public ExternalUpdateOrderStatusConsumer(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task Consume(ConsumeContext<IOrderStatusContract> context)
        {
            await _mediatr.Send(new UpdateOrderStatusCommand(context.Message.OrderId, context.Message.Status));       
        }
    }
}
