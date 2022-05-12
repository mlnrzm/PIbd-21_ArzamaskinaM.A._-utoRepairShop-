using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.BusinessLogicsContracts;
using AbstractCarRepairShopContracts.StoragesContracts;
using AbstractCarRepairShopContracts.ViewModels;
using AbstractCarRepairShopContracts.Enums;
using System;
using System.Collections.Generic;
using AbstractCarRepairShopBusinessLogic.MailWorker;

namespace AbstractCarRepairShopBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        private readonly AbstractMailWorker _mailWorker;
        private readonly IClientStorage _clientStorage;
        public OrderLogic(IOrderStorage orderStorage, AbstractMailWorker mailWorker, IClientStorage clientStorage)
        {
            _orderStorage = orderStorage;
            _mailWorker = mailWorker;
            _clientStorage = clientStorage;
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
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel { Id = model.ClientId })?.Login,
                Subject = "Создан новый заказ",
                Text = $"Дата заказа: {DateTime.Now}, сумма заказа: {model.Sum}"
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
                Status = element.Status
            });
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel { Id = element.ClientId })?.Login,
                Subject = $"Смена статуса заказа№ {element.Id}",
                Text = $"Статус изменен на: {OrderStatus.Выдан}"
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
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel { Id = element.ClientId })?.Login,
                Subject = $"Смена статуса заказа№ {element.Id}",
                Text = $"Статус изменен на: {OrderStatus.Готов}"
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
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel { Id = element.ClientId })?.Login,
                Subject = $"Смена статуса заказа№ {element.Id}",
                Text = $"Статус изменен на: {OrderStatus.Выполняется}"
            });
        }
    }
}
