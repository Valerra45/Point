using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Core.Domain.Entity
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
