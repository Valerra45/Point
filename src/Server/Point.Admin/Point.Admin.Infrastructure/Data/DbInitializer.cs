using Point.Admin.Infrastructure.Data;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Admin.Infrastructure.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AdminContext _context;

        public DbInitializer(AdminContext context)
        {
            _context = context;
        }

        public void InitializeDb()
        {
            _context.Database.EnsureDeleted();

            _context.Database.EnsureCreated();

            _context.IssuePoints.AddRange(FakeDataFactory.IssuePoints());

            _context.SaveChanges();
        }
    }
}
