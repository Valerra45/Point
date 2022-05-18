using Point.External.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Infrastructure.Data
{
    public static class FakeDataFactory
    {
        public static IEnumerable<IssuePoint> IssuePoints()
        {
            yield return new IssuePoint
            {
                Id = Guid.Parse("47B9C219-73F4-45EA-9A16-ABB9ADFD308E"),
                PointId = Guid.Parse("D615C738-A9B4-4EA2-B805-69C408347159"),
                Created = DateTime.Now,
                Name = "Points 1",
                Address = "Address 1",
                Phone = "555-77-90"
            };

            yield return new IssuePoint
            {
                Id = Guid.Parse("645B74A4-0A09-4BD1-A497-9838D14DC78E"),
                PointId = Guid.Parse("F8543DAB-CC43-460E-B3E7-79CD342B4EDA"),
                Created = DateTime.Now.AddDays(-1),
                Name = "Points 2",
                Address = "Address 2",
                Phone = "222-33-555"
            };
        }
    }
}
