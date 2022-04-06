using Point.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Contracts
{
    public class OrderContract
    {
        public string Number { get; set; }

        public string Description { get; set; }

        public Guid OrderId { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public class OrderItem
        {
            public string Name { get; set; }

            public string Description { get; set; }

            public int Count { get; set; }
        }
    }
}
