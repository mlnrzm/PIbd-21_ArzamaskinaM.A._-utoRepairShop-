using System;
using System.Collections.Generic;

namespace AbstractCarRepairShopContracts.ViewModels
{
    public class ReportRepairComponentViewModel
    {
        public string RepairName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> RepairComponents { get; set; }
    }
}
