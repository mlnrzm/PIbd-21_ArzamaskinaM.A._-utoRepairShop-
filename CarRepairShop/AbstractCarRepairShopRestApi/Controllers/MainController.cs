using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.BusinessLogicsContracts;
using AbstractCarRepairShopContracts.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbstractCarRepairShopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IRepairLogic _repair;
        public MainController(IOrderLogic order, IRepairLogic repair)
        {
            _order = order;
            _repair = repair;
        }
        [HttpGet]
        public List<RepairViewModel> GetRepairList() => _repair.Read(null)?.ToList();
        [HttpGet]
        public RepairViewModel GetRepair(int repairId) => _repair.Read(new RepairBindingModel
        { Id = repairId })?[0];
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _order.CreateOrder(model);
    }
}
