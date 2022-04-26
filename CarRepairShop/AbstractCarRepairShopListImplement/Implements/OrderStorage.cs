using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.StoragesContracts;
using AbstractCarRepairShopContracts.ViewModels;
using AbstractCarRepairShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractCarRepairShopListImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly DataListSingleton source;
        public OrderStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<OrderViewModel> GetFullList()
        {
            var result = new List<OrderViewModel>();
            foreach (var repair in source.Orders)
            {
                result.Add(CreateModel(repair));
            }
            return result;
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<OrderViewModel>();
            foreach (var order in source.Orders)
            {
                if ((!model.DateFrom.HasValue && !model.DateTo.HasValue &&
                    order.DateCreate.Date == model.DateCreate.Date) ||
                     (model.DateFrom.HasValue && model.DateTo.HasValue && order.DateCreate.Date
                    >= model.DateFrom.Value.Date && order.DateCreate.Date <= model.DateTo.Value.Date) ||
                     (model.ClientId.HasValue && order.ClientId == model.ClientId) ||
                    (model.SearchStatus.HasValue && model.SearchStatus.Value == order.Status) ||
                    (model.ImplementerId.HasValue && order.ImplementerId == model.ImplementerId && model.Status == order.Status))
                {
                    result.Add(CreateModel(order));
                }
            }
            return result;
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id)
                {
                    return CreateModel(order);
                }
            }
            return null;
        }
        public void Insert(OrderBindingModel model)
        {
            var tempOrder = new Order
            {
                Id = 1
            };
            foreach (var order in source.Orders)
            {
                if (order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = order.Id + 1;
                }
            }
            source.Orders.Add(CreateModel(model, tempOrder));
        }
        public void Update(OrderBindingModel model)
        {
            Order tempOrder = null;
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id)
                {
                    tempOrder = order;
                }
            }
            if (tempOrder == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempOrder);
        }
        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private static Order CreateModel(OrderBindingModel model, Order order)
        {
            order.RepairId = model.RepairId;
            order.ClientId = (int)model.ClientId;
            order.ImplementerId = (int)model.ImplementerId;
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
            string clientName = "";
            string implementerName = "";

            for (int i = 0; i < source.Implementers.Count; ++i)
            {
                if (source.Implementers[i].Id == order.ImplementerId)
                {
                    implementerName = source.Implementers[i].Name;
                    break;
                }
            }

            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].Id == order.ClientId)
                {
                    clientName = source.Clients[i].Name;
                    break;
                }
            }

            for (int i = 0; i < source.Repairs.Count; ++i)
            {
                if (source.Repairs[i].Id == order.RepairId)
                {
                    repairName = source.Repairs[i].RepairName;
                    break;
                }
            }

            return new OrderViewModel
            {
                Id = order.Id,
                RepairId = order.RepairId,
                ClientId = order.ClientId,
                ImplementerId = order.ImplementerId,
                ImplementerName = implementerName,
                ClientName = clientName,
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
