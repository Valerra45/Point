﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Api.Models
{
    public class ClientDto
    {
        public Guid ClientId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
