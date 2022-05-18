using Point.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.WebHost.Models
{
    public class UpdateOrderStatus
    {
        public Guid Id { get; set; }

        public OrderStatus Status { get; set; }
    }
}
