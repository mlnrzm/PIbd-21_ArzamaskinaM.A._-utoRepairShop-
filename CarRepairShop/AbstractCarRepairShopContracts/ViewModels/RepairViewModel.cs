using System.Collections.Generic;
using AbstractCarRepairShopContracts.Attributes;

namespace AbstractCarRepairShopContracts.ViewModels
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине (ремонт автомобиля)
    /// </summary>
    public class RepairViewModel
    {
        [Column(title: "Номер", width: 50, visible: false)]
        public int Id { get; set; }
        [Column(title: "Название ремонта", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string RepairName { get; set; }
        [Column(title: "Цена", width: 100)]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> RepairComponents { get; set; }
    }

}
