using System;
using System.Collections.Generic;

namespace AbstractCarRepairShopFileImplement.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string WarehouseName { get; set; }
        public string ResponsibleName { get; set; }
        public DateTime DateCreation { get; set; }
        public Dictionary<int, int> WarehouseComponents { get; set; }
    }
}
