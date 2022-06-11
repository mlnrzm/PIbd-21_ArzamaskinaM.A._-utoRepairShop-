using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AbstractCarRepairShopContracts.ViewModels
{
    public class WarehouseViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название склада")]
        public string WarehouseName { get; set; }
        [DisplayName("Сотрудник")]
        public string ResponsibleName { get; set; }
        [DisplayName("Дата создания склада")]
        public DateTime DateCreation { get; set; }
        public Dictionary<int, (string, int)> WarehouseComponents { get; set; }
    }
}
