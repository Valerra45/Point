using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.Core.Domain.Entity
{
    public class IssuePoint : BaseEntity
    {
        public Guid PointId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
