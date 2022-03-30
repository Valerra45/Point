using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.Core.Domain.Entity
{
    public class Order : BaseEntity
    {
        public string Number { get; set; }

        public string Description { get; set; }

        public Guid OrderId { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public OrderStatus Status { get; set; }

        public virtual Client Client { get; set; }
    }
}
