using AutoMapper;
using MassTransit;
using Newtonsoft.Json;
using Point.Contracts;
using Point.Ordering.Core.Domain.Entity;
using Point.SharedKernel.Abstractions;
using Point.SharedKernel.DtoModels;
using Point.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.Infrastructure.Consumers
{
    public class CreateOrderConsumer : IConsumer<IOrderContract>
    {
        private readonly IRepository<IssuePoint> _pointRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly IMapper _mapper;

        public CreateOrderConsumer(IRepository<IssuePoint> pointRepository,
            IRepository<Order> orderRepository,
            IRepository<Product> productRepository,
            IRepository<Client> clientRepository,
            IMapper mapper)
        {
            _pointRepository = pointRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IOrderContract> ctx)
        {
            var orderDto = JsonConvert.DeserializeObject<OrderDto>(ctx.Message.Message);

            var order = _mapper.Map<Order>(orderDto);

            order.IssuePoint = await _pointRepository.GetFirstWhere(x => x.PointId == order.IssuePoint.PointId);

            order.Client = await GetOrCreateClient(order.Client);

            foreach (var item in order.OrderItems)
            {
                item.Product = await GetOrCreateProduct(item.Product);
            }

            order.Status = OrderStatus.Loaded;

            await _orderRepository.AddAsync(order);
        }

        private async Task<Client> GetOrCreateClient(Client clent)
        {
            var result = await _clientRepository.GetFirstWhere(x => x.ClientId == clent.ClientId);

            if (result != null)
            {
                return result;
            }

            await _clientRepository.AddAsync(clent);

            return clent;
        }

        private async Task<Product> GetOrCreateProduct(Product product)
        {
            var result = await _productRepository.GetFirstWhere(x => x.ProductId == product.ProductId);

            if (result != null)
            {
                return result;
            }

            await _productRepository.AddAsync(product); 

            return product;
        }
    }
}
