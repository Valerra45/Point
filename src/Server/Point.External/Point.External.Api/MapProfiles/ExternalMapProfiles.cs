using AutoMapper;
using Point.External.Api.Models;
using Point.External.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Api.MapProfiles
{
    public class ExternalMapProfiles : Profile
    {
        public ExternalMapProfiles()
        {
            CreateMap<Client, ClientDto>()
                .ReverseMap();
            CreateMap<IssuePoint, IssuePointDto>()
                .ReverseMap();
            CreateMap<Order, OrderDto>()
                .ReverseMap();
        }
    }
}
