using MassTransit.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Infrastructure.Consumers
{
    public class ExternalCreateIssuePointConsumerDefinition : ConsumerDefinition<ExternalCreateIssuePointConsumer>
    {
        public ExternalCreateIssuePointConsumerDefinition()
        {
            EndpointName = "CreateIssuePointExternal";
        }
    }
}
