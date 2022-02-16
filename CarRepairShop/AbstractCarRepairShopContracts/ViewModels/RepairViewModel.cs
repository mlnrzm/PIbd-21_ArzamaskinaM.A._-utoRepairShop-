using System.Collections.Generic;
using System.ComponentModel;

namespace AbstractCarRepairShopContracts.ViewModels
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине (ремонт автомобиля)
    /// </summary>
    public class RepairViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название ремонта")]
        public string RepairName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> RepairComponents { get; set; }
    }

}
