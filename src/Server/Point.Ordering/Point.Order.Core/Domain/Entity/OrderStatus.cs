using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.Core.Domain.Entity
{
    public enum OrderStatus
    {
        Loaded,       // загружен
        Received,     // получен
        Issued        // выдан
    }
}
