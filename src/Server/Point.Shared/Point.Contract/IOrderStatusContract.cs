using Point.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Contracts
{
    public interface IOrderStatusContract
    {
        public Guid OrderId { get; set; }

        public OrderStatus Status { get; set; }
    }
}
