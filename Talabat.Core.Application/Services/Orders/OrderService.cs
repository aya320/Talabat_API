using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Orders;
using Talabat.Core.Application.Abstraction.Services.Basket;
using Talabat.Core.Application.Abstraction.Services.Orders;
using Talabat.Core.Application.Common.Exceptions;
using Talabat.Core.Application.Services.Basket;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Core.Domain.Entities.Orders;
using Talabat.Core.Domain.Entities.Products;
using Talabat.Core.Domain.Specifications.Orders;

namespace Talabat.Core.Application.Services.Orders
{
    public class OrderService(IUnitOfWork unitOfWork, IMapper mapper, IBasketService basketService) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
    {
     
        var basket = await basketService.GetCustomerBasketAsync(order.BasketId);
      
        var orderItems = new List<OrderItem>();
        if (basket.Items.Count() > 0)
        {
            var productRepo = unitOfWork.GetRepository<Product, int>();
            foreach (var item in basket.Items)

            {
                var product = await productRepo.GetAsync(item.Id);
                if (product is not null)
                {
                    var productItemOrdered = new ProductItemOrdered()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl ?? "",
                    };
                    var OrderItem = new OrderItem()
                    {
                        Product = productItemOrdered,
                        Price = product.Price,
                        Quantity = item.Quantity
                    };
                    orderItems.Add(OrderItem);
                }
            }
        }
        //3. Calculate SubTotal
        var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
        // 4. Map Address
        var address = mapper.Map<Address>(order.ShippingAddress);

            //5. Get Delivery Method
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(order.DeliveryMethodId);

            //4. Create Order
            var orderToCreate = new Order()
        {
            BuyerEmail = buyerEmail,
            ShippingAddress = address,
                DeliveryMethod = deliveryMethod,
                Items = orderItems,
            Subtotal = subTotal,
        };
        await unitOfWork.GetRepository<Order, int>().AddAsync(orderToCreate);
        //5. Save To Database
        var created = await unitOfWork.CompleteAsync() > 0;
        if (!created) throw new BadRequestException("an error has occured during creating the  order");
        return mapper.Map<OrderToReturnDto>(orderToCreate);
    }
        public async Task<IEnumerable<OrderToReturnDto>> GetOrderForUserAsync(string buyerEmail)
        {
            var orderSpec = new OrderSpecifications(buyerEmail);
            var orders = await unitOfWork.GetRepository<Order, int>().GetAllWithSpecAsync(orderSpec);
            return mapper.Map<IEnumerable<OrderToReturnDto>>(orders);

        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
        {
            var orderSpecs = new OrderSpecifications(buyerEmail, orderId);
            var order = await unitOfWork.GetRepository<Order, int>().GetWithSpecAsync(orderSpecs);
            if (order is null) throw new NotFoundException(nameof(Order), orderId);
            return mapper.Map<OrderToReturnDto>(order);
        }


        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }
    }
}
