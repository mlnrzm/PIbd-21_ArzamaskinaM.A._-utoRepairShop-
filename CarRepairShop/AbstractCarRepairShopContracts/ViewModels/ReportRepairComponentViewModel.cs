using System;
using System.Collections.Generic;
namespace AbstractCarRepairShopContracts.ViewModels
{
    public class ReportRepairComponentViewModel
    {
        public string ComponentName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Repairs { get; set; }
    }
}
