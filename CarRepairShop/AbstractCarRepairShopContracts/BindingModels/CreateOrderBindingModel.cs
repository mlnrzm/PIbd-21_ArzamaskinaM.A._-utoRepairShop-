using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractCarRepairShopContracts.BindingModels
{
    /// <summary>
    /// Данные от клиента, для создания заказа
    /// </summary>
    public class CreateOrderBindingModel
    {
        public int ClientId { get; set; }
        public int RepairId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
