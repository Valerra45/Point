using MassTransit;
using Point.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.Infrastructure.Consumers
{
    public class CreateOrderConsumer : IConsumer<IOrderContract>
    {
        public Task Consume(ConsumeContext<IOrderContract> ctx)
        {
            throw new NotImplementedException();
        }
    }
}
