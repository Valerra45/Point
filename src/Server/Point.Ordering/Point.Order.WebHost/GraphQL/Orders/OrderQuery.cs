using HotChocolate;
using HotChocolate.Data;
using Point.Ordering.Core.Domain.Entity;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.WebHost.GraphQL.Orders
{
    public class OrderQuery
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<Order>> Get([Service] IRepository<Order> repository) =>
            await repository.GetAllAsync();
    }
}
