using Point.Admin.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Admin.Infrastructure.Data
{
    public static class FakeDataFactory
    {
        public static IEnumerable<IssuePoint> IssuePoints()
        {
            yield return new IssuePoint
            {
                Id = Guid.Parse("D615C738-A9B4-4EA2-B805-69C408347159"),
                Created = DateTime.Now,
                Name = "Points 1",
                Address = "Address 1",
                Phone = "555-77-90"
            };

            yield return new IssuePoint
            {
                Id = Guid.Parse("F8543DAB-CC43-460E-B3E7-79CD342B4EDA"),
                Created = DateTime.Now.AddDays(-1),
                Name = "Points 2",
                Address = "Address 2",
                Phone = "222-33-555"
            };
        }
    }
}
