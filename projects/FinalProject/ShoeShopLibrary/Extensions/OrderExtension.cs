using ShoeShopLibrary.DTOs;
using ShoeShopLibrary.Models;

namespace ShoeShopLibrary.Extensions
{
    public static class OrderExtension
    {
        extension(Order order)
        {
            public OrderDto? ToDto()
            => order is null ? null : new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                IsFinished = order.IsFinished,
            };
        }

        extension(IEnumerable<Order> orders)
        {
            public IEnumerable<OrderDto?> ToDtos()
            => orders.Select(ToDto);
        }
    }
}
