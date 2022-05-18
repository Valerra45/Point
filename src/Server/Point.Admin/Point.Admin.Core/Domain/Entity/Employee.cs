using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Admin.Core.Domain.Entity
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string UserName { get; set; }

    }
}
