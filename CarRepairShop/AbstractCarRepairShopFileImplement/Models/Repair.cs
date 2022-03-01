using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractCarRepairShopFileImplement.Models
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине (ремонт авто)
    /// </summary>
    public class Repair
    {
        public int Id { get; set; }
        public string RepairName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> RepairComponents { get; set; }
    }

}
