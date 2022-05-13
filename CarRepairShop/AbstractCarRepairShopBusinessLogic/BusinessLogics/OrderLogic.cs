using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.BusinessLogicsContracts;
using AbstractCarRepairShopContracts.StoragesContracts;
using AbstractCarRepairShopContracts.ViewModels;
using AbstractCarRepairShopContracts.Enums;
using System;
using System.Collections.Generic;

namespace AbstractCarRepairShopBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        public OrderLogic(IOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }
            return _orderStorage.GetFilteredList(model);
        }
        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                RepairId = model.RepairId,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят,
                DateCreate = DateTime.Now,
                ClientId = model.ClientId
            });
        }
        public void DeliveryOrder(ChangeStatusBindingModel model)
        {
            var element = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });

            if (element == null)
            {
                throw new Exception("Заказ не найден");
            }

            if (element.Status != OrderStatus.Готов)
            {
                throw new Exception($"Невозможно выдать заказ, т.к. он не имеет статуса {OrderStatus.Готов}");
            }

            _orderStorage.Update(new OrderBindingModel
            {
                Id = element.Id,
                RepairId = element.RepairId,
                ClientId = element.ClientId,
                ImplementerId = model.ImplementerId,
                Count = element.Count,
                Sum = element.Sum,
                DateCreate = element.DateCreate,
                DateImplement = element.DateImplement,
                Status = OrderStatus.Выдан
            });

        }
        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var element = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });

            if (element == null)
            {
                throw new Exception("Заказ не найден");
            }

            if (element.Status != OrderStatus.Выполняется)
            {
                throw new Exception($"Невозможно завершить заказ, т.к. он не имеет статуса {OrderStatus.Выполняется}");
            }

            _orderStorage.Update(new OrderBindingModel
            {
                Id = element.Id,
                RepairId = element.RepairId,
                ClientId = element.ClientId,
                ImplementerId = model.ImplementerId,
                Count = element.Count,
                Sum = element.Sum,
                DateCreate = element.DateCreate,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Готов
            });
        }
        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var element = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });

            if (element == null)
            {
                throw new Exception("Заказ не найден");
            }

            if (element.Status != OrderStatus.Принят)
            {
                throw new Exception($"Невозможно обработать заказ, т.к. он не имеет статуса {OrderStatus.Принят}");
            }

            _orderStorage.Update(new OrderBindingModel
            {
                Id = element.Id,
                RepairId = element.RepairId,
                Count = element.Count,
                DateCreate = element.DateCreate,
                DateImplement = element.DateImplement,
                Sum = element.Sum,
                Status = OrderStatus.Выполняется,
                ClientId = element.ClientId,
                ImplementerId = model.ImplementerId
            });
        }
    }
}
