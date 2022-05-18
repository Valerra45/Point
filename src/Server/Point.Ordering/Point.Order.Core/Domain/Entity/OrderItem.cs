using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.Core.Domain.Entity
{
    public class OrderItem : BaseEntity
    {
        public virtual Product Product { get; set; }

        public int Count { get; set; }

        public virtual Order Order { get; set; }
    }
}
