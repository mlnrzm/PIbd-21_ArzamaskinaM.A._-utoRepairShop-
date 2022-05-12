﻿using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.BusinessLogicsContracts;
using AbstractCarRepairShopContracts.Enums;
using AbstractCarRepairShopContracts.ViewModels;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace AbstractCarRepairShopBusinessLogic.BusinessLogics
{
    public class WorkModeling : IWorkProcess
    {
        private IOrderLogic orderLogic;
        private readonly Random rnd;

        public WorkModeling()
        {
            rnd = new Random(1000);
        }

        public void DoWork(IImplementerLogic implementerLogic, IOrderLogic _orderLogic)
        {
            orderLogic = _orderLogic;
            var implementers = implementerLogic.Read(null);
            ConcurrentBag<OrderViewModel> orders = new(orderLogic.Read(new OrderBindingModel
            { SearchStatus = OrderStatus.Принят }));
            foreach (var implementer in implementers)
            {
                Task.Run(async () => await WorkerWorkAsync(implementer, orders));
            }
        }

        private async Task WorkerWorkAsync(ImplementerViewModel implementer, ConcurrentBag<OrderViewModel> orders)
        {
            var runOrders = await Task.Run(() => orderLogic.Read(new OrderBindingModel
            {
                ImplementerId = implementer.Id,
                Status = OrderStatus.Выполняется
            }));

            foreach (var order in runOrders)
            {
                Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count * 1000);
                orderLogic.FinishOrder(new ChangeStatusBindingModel
                {
                    OrderId = order.Id
                });
                Thread.Sleep(implementer.PauseTime * 1000);
            }
            await Task.Run(() =>
            {
                while (!orders.IsEmpty)
                {
                    if (orders.TryTake(out OrderViewModel order))
                    {
                        orderLogic.TakeOrderInWork(new ChangeStatusBindingModel
                        { OrderId = order.Id, ImplementerId = implementer.Id });

                        Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count * 1000);
                        orderLogic.FinishOrder(new ChangeStatusBindingModel
                        { OrderId = order.Id });
                        Thread.Sleep(implementer.PauseTime * 1000);
                    }
                }
            });
        }
    }

}
