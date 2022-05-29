using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.Core.Domain.Entity
{
    public class Client : BaseEntity
    {
        public Guid ClientId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
