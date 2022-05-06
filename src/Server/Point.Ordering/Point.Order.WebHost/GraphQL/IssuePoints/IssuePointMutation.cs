using HotChocolate;
using Point.Ordering.Core.Domain.Entity;
using Point.Ordering.WebHost.Models;
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
            CreateIssuePoint createPoint)
        {
            var point = new IssuePoint
            {
                Address = createPoint.Address,
                Name = createPoint.Name,
                Phone = createPoint.Phone
            };

            await repository.AddAsync(point);

            return point;
        }
    }
}
