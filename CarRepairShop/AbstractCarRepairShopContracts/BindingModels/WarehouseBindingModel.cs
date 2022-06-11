﻿using System;
using System.Collections.Generic;

namespace AbstractCarRepairShopContracts.BindingModels
{
    public class WarehouseBindingModel
    {
        public int? Id { get; set; }
        public string WarehouseName { get; set; }
        public string ResponsibleName { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> WarehouseComponents { get; set; }
    }
}