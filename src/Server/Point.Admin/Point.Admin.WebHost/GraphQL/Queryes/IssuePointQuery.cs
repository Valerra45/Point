using HotChocolate;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using Point.Admin.Core.Domain.Entity;
using Point.Admin.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Admin.WebHost.GraphQL.Queryes
{
    public class IssuePointQuery
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<IssuePoint>> Get([Service] AdminContext context) =>
           await context.IssuePoints.ToListAsync();
    }
}
