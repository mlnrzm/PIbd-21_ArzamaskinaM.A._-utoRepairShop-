using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractCarRepairShopContracts.BindingModels
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class ClientBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
