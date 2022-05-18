using MassTransit;
using MediatR;
using Point.Contracts;
using Point.External.Core.Domain.Entity;
using Point.External.Infrastructure.Services.Points.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Infrastructure.Consumers
{
    public class ExternalCreateIssuePointConsumer : IConsumer<IIssuePointContract>
    {
        private readonly IMediator _mediatr;

        public ExternalCreateIssuePointConsumer(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task Consume(ConsumeContext<IIssuePointContract> context)
        {
            var point = new IssuePoint() 
            {
                PointId = context.Message.Id,
                Address = context.Message.Address,
                Name = context.Message.Name,
                Phone = context.Message.Phone,
                Created = DateTime.Now
            };

            await _mediatr.Send(new CreateIssuePointCommand(point));
        }
    }
}
