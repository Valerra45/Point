using Point.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Core.Domain.Entity
{
    [BsonCollection("Orders")]
    public class Order : BaseEntity
    {
        public string Number { get; set; }

        public string Description { get; set; }

        public Guid OrderId { get; set; }

        public Client Client { get; set; }

        public IssuePoint IssuePoint { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public OrderStatus Status { get; set; }
    }
}
