﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Api.Models
{
    public class OrderDto
    {
        public string Number { get; set; }

        public string Description { get; set; }

        public Guid OrderId { get; set; }

        public ClientDto Client { get; set; }

        public IssuePointDto IssuePoint { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}
