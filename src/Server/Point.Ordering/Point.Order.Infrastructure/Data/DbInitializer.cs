using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.Infrastructure.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly OrderContext _context;

        public DbInitializer(OrderContext context)
        {
            _context = context;
        }

        public void InitializeDb()
        {
            _context.Database.EnsureCreated();

            _context.IssuePoints.AddRange(FakeDataFactory.IssuePoints());

            _context.SaveChanges();
        }
    }
}
