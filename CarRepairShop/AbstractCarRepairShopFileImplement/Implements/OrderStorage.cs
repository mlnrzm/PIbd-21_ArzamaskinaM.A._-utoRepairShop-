using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.StoragesContracts;
using AbstractCarRepairShopContracts.ViewModels;
using AbstractCarRepairShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractCarRepairShopFileImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly FileDataListSingleton source;
        public OrderStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<OrderViewModel> GetFullList()
        {
            var result = from order in source.Orders
                         select CreateModel(order);
            return result.ToList();
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = from order in source.Orders
                         where order.Id == model.Id && 
                               order.DateCreate >= model.DateFrom && 
                               order.DateCreate <= model.DateTo
                         select CreateModel(order);
            return result.ToList();
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = from order in source.Orders
                         where order.Id == model.Id
                         select CreateModel(order);
            return (OrderViewModel)result.FirstOrDefault();
        }
        public void Insert(OrderBindingModel model)
        {
            var maxId = from order in source.Orders
                         select order.Id;
            var element = new Order { Id = maxId.Max() + 1 };
            source.Orders.Add(CreateModel(model, element));
        }
        public void Update(OrderBindingModel model)
        {
            var tempOrder = from order in source.Orders
                            where order.Id == model.Id
                            select order;
            if (tempOrder == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, (Order)tempOrder.FirstOrDefault()); 
        }
        public void Delete(OrderBindingModel model)
        {
            var tempOrder = from order in source.Orders
                            where order.Id == model.Id
                            select order;
            if (tempOrder != null)  source.Orders.Remove((Order)tempOrder.FirstOrDefault());
            else throw new Exception("Элемент не найден");
            
        }
        private static Order CreateModel(OrderBindingModel model, Order order)
        {
            order.RepairId = model.RepairId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;

            return order;
        }
        private OrderViewModel CreateModel(Order order)
        {
            string repairName = "";

            var tempOrder = from repair in source.Repairs
                            where repair.Id == order.RepairId
                            select repair;
            repairName = ((Repair)tempOrder.FirstOrDefault()).RepairName;

            return new OrderViewModel
            {
                Id = order.Id,
                RepairId = order.RepairId,
                RepairName = repairName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
