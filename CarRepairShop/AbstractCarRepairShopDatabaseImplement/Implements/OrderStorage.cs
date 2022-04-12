using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.StoragesContracts;
using AbstractCarRepairShopContracts.ViewModels;
using AbstractCarRepairShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractCarRepairShopDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using var context = new AbstractCarRepairShopDatabase();
            return context.Orders
            .Include(rec => rec.Repair)
            .Select(CreateModel)
            .ToList();
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AbstractCarRepairShopDatabase();
            return context.Orders
            .Include(rec => rec.Repair)
            .Include(rec => rec.Client)
            .Where(rec => rec.RepairId == model.RepairId ||
               (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo) ||
               model.ClientId.HasValue && rec.ClientId == model.ClientId)
            .Select(CreateModel)
            .ToList();
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AbstractCarRepairShopDatabase();
            var order = context.Orders
            .FirstOrDefault(rec => rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }
        public void Insert(OrderBindingModel model)
        {
            using (var context = new AbstractCarRepairShopDatabase())
            {
                Order order = new Order();
                CreateModel(model, order, context);
                context.Orders.Add(order);
                context.SaveChanges();
            }
        }
        public void Update(OrderBindingModel model)
        {
            using (var context = new AbstractCarRepairShopDatabase())
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (var context = new AbstractCarRepairShopDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Order CreateModel(OrderBindingModel model, Order order, AbstractCarRepairShopDatabase context_)
        {
            order.RepairId = model.RepairId;
            order.ClientId = model.ClientId.Value;
            order.Client = context_.Clients.FirstOrDefault(rec => rec.Id == model.ClientId);
            order.Repair = context_.Repairs.FirstOrDefault(rec => rec.Id == model.RepairId);
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
        private static OrderViewModel CreateModel(Order order)
        {
            using var context = new AbstractCarRepairShopDatabase();
            return new OrderViewModel
            {
                Id = order.Id,
                RepairId = order.RepairId,
                ClientId = order.ClientId,
                ClientName = order.Client.Name,
                RepairName = order.Repair.RepairName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
            };
        }
    }
}