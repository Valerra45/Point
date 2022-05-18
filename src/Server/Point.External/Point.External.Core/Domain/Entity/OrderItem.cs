using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Core.Domain.Entity
{
    [BsonCollection("OrderItems")]
    public class OrderItem : BaseEntity
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }
    }
}
