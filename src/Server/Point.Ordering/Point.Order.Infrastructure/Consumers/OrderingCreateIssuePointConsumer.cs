using MassTransit;
using Point.Contracts;
using Point.Ordering.Core.Domain.Entity;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.Infrastructure.Consumers
{
    public class OrderingCreateIssuePointConsumer : IConsumer<IIssuePointContract>
    {
        private readonly IRepository<IssuePoint> _pointRepository;

        public OrderingCreateIssuePointConsumer(IRepository<IssuePoint> pointRepository)
        {
            _pointRepository = pointRepository;
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

            await _pointRepository.AddAsync(point);   
        }
    }
}
