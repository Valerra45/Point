using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.Core.Domain
{
    public class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            Created = DateTime.Now;
        }

        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Update { get; set; }
    }
}
