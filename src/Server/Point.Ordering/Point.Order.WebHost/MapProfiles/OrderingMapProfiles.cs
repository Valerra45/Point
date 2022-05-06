using AutoMapper;
using Point.Ordering.Core.Domain.Entity;
using Point.SharedKernel.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.WebHost.MapProfiles
{
    public class OrderingMapProfiles : Profile
    {
        public OrderingMapProfiles()
        {
            CreateMap<ClientDto, Client>()
                .ReverseMap();

            CreateMap<IssuePointDto, IssuePoint>()
                .ReverseMap();

            CreateMap<OrderDto, Order>()
                .ReverseMap();

            CreateMap<OrderItemDto, OrderItem>()
                 .ForPath(x => x.Product.ProductId, s => s.MapFrom(x => x.ProductId))
                 .ForPath(x => x.Product.Name, s => s.MapFrom(x => x.Name))
                 .ForPath(x => x.Product.Description, s => s.MapFrom(x => x.Description));
        }
    }
}
