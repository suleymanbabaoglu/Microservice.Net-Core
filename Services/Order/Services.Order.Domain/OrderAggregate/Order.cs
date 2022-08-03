using Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Order.Domain.OrderAggregate
{
    //EF Core Features
    //--Owned Types
    //--Shadow Property
    //--Backing Field
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; private set; }

        public Address Address { get; private set; }

        public string BuyerId { get; private set; }


        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;


        public Order(Address address, string buyerId)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }

        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            var existProduct = _orderItems.Any(p => p.ProductId == productId);
            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(newOrderItem);
            }

        }

        public decimal GetTotalPrice => _orderItems.Sum(p => p.Price);
    }
}
