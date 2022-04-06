using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.SharedKernel.Abstractions
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }

        DateTime Created { get; set; }

        DateTime? Update { get; set; }
    }
}
