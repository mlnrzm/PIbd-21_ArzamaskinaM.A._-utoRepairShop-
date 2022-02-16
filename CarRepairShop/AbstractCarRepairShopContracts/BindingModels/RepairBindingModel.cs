using System.Collections.Generic;

namespace AbstractCarRepairShopContracts.BindingModels
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине (ремонт)
    /// </summary>
    public class RepairBindingModel
    {
        public int? Id { get; set; }
        public string RepairName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> RepairComponents { get; set; }
    }
}
