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

namespace Point.Ordering.WebHost.GraphQL.Mutations
{
    public class IssuePointMutation
    {
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public IssuePointMutation(IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IssuePoint> Add([Service] AdminContext context,
           
            CreateIssuePoint request)
        {
            var point = _mapper.Map<IssuePoint>(request);

            context.IssuePoints.Add(point);

            await context.SaveChangesAsync();

            await _publishEndpoint.Publish<IIssuePointContract>(new 
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
