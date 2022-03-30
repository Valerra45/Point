using HotChocolate;
using HotChocolate.Data;
using Point.Ordering.Core.Domain.Entity;
using Point.Ordering.Infrastructure.Data;
using Point.Ordering.Infrastructure.Repositories;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.WebHost.GraphQL
{
    public class IssuePointQueries
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<IssuePoint>> Get([Service] IRepository<IssuePoint> repository) =>
           await repository.GetAllAsync();
    }
}
