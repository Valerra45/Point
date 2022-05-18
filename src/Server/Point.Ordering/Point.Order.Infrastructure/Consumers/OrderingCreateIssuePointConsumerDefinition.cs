using MassTransit.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.Infrastructure.Consumers
{
    public class OrderingCreateIssuePointConsumerDefinition : ConsumerDefinition<OrderingCreateIssuePointConsumer>
    {
        public OrderingCreateIssuePointConsumerDefinition()
        {
            EndpointName = "CreateIssuePointOrdering";
        }
    }
}
