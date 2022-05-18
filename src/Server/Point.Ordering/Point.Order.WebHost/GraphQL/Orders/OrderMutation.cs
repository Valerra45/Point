using HotChocolate;
using MassTransit;
using Point.Contracts;
using Point.Ordering.Core.Domain.Entity;
using Point.Ordering.WebHost.Models;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Ordering.WebHost.GraphQL.Orders
{
    public class OrderMutation
    {
        public async Task<Order> Update([Service] IRepository<Order> repository,
            [Service] IPublishEndpoint publishEndpoint,
            UpdateOrderStatus updateOrder)
        {
            var order = await repository.GetByIdAsync(updateOrder.Id);

            order.Status = updateOrder.Status;  

            await repository.UpdateAsync(order);
            
            await publishEndpoint.Publish<IOrderStatusContract>(new 
            {
                OrderId = order.OrderId,

                Status = order.Status
            });

            return order;
        } 
    }
}
