using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.Core.Domain.Entity
{
    public class OrderItem : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }
    }
}
