using AutoMapper;
using HotChocolate;
using MassTransit;
using Point.Admin.Core.Domain.Entity;
using Point.Admin.Infrastructure.Data;
using Point.Admin.WebHost.Models;
using Point.Contracts;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.WebHost.GraphQL.IssuePoints
{
    public class IssuePointMutation
    {
        public async Task<IssuePoint> Add([Service] IRepository<IssuePoint> repository,
            [Service] IMapper mapper,
            [Service] IPublishEndpoint publishEndpoint,
            CreateIssuePoint request)
        {
            var point = mapper.Map<IssuePoint>(request);

            await repository.AddAsync(point);

            await publishEndpoint.Publish<IIssuePointContract>(new 
            { 
                Id = point.Id,
                Name = point.Name,
                Address = point.Address,
                Phone = point.Phone
            });

            return point;
        }
    }
}
