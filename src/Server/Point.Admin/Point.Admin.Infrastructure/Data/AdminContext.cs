using Microsoft.EntityFrameworkCore;
using Point.Admin.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Admin.Infrastructure.Data
{
    public class AdminContext : DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options)
            : base(options)
        {

        }

        public DbSet<IssuePoint> IssuePoints { get; set; }
        
        public DbSet<Employee> Employees { get; set; }
    }
}
