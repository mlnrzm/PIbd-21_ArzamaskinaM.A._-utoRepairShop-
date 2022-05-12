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
                .Include(rec => rec.Client)
                .Include(rec => rec.Implementer)
                .ToList()
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
                .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue
                    && rec.DateCreate.Date == model.DateCreate.Date) ||
                    (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate.Date >= model.DateFrom.Value.Date
                    && rec.DateCreate.Date <= model.DateTo.Value.Date) ||
                    (model.ClientId.HasValue && rec.ClientId == model.ClientId) ||
                    (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId) ||
                    (model.SearchStatus.HasValue && model.SearchStatus.Value == rec.Status))
                .Include(rec => rec.Repair)
                .Include(rec => rec.Client)
                .Include(rec => rec.Implementer)
                .Select(CreateModel).ToList();
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AbstractCarRepairShopDatabase();
            var order = context.Orders
            .Include(rec => rec.Repair)
            .Include(rec => rec.Client)
            .Include(rec => rec.Implementer)
            .FirstOrDefault(rec => rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }
        public void Insert(OrderBindingModel model)
        {
            using var context = new AbstractCarRepairShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Order order = new Order();
                CreateModel(model, order, context);
                context.Orders.Add(order);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(OrderBindingModel model)
        {
            using var context = new AbstractCarRepairShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
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
            if (model.ImplementerId != null)
            {
                order.Implementer = context_.Implementers.FirstOrDefault(rec => rec.Id == model.ImplementerId);
                order.ImplementerId = model.ImplementerId.Value;
            }

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
            using (var context = new AbstractCarRepairShopDatabase())
            {
                string ImplementerName = "";
                if (order.Implementer != null)
                    ImplementerName = order.Implementer.Name;

                string repairName = context.Repairs.FirstOrDefault(rec => rec.Id == order.RepairId).RepairName;
                string ClientName = context.Clients.FirstOrDefault(rec => rec.Id == order.ClientId)?.Name;
                
                return new OrderViewModel
                {
                    Id = order.Id,
                    RepairId = order.RepairId,
                    RepairName = repairName,
                    ClientId = order.ClientId,
                    ClientName = ClientName,
                    ImplementerId = order.ImplementerId,
                    ImplementerName = ImplementerName,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                };
            }
        }
    }
}