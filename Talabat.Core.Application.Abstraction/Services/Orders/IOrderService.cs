using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Orders;

namespace Talabat.Core.Application.Abstraction.Services.Orders
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order);
        Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId);
        Task<IEnumerable<OrderToReturnDto>> GetOrderForUserAsync(string buyerEmail);
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
    }
}
