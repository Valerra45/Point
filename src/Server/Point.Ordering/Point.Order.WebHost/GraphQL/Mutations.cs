using HotChocolate;
using Point.Ordering.Core.Domain.Entity;
using Point.Ordering.WebHost.Models;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.WebHost.GraphQL
{
    public class IssuePointMutations
    {
        private readonly IRepository<IssuePoint> _repository;

        public IssuePointMutations([Service] IRepository<IssuePoint> repository)
        {
            _repository = repository;
        }

        public async Task<IssuePoint> Add(CreateIssuePoint createPoint)
        {
            var point = new IssuePoint
            {
                Address = createPoint.Address,
                Name = createPoint.Name,
                Phone = createPoint.Phone
            };

            await _repository.AddAsync(point);

            return point;
        }
    }
}
