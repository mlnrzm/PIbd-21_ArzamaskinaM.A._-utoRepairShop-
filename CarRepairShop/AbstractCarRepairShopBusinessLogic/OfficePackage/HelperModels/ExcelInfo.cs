using AbstractCarRepairShopContracts.ViewModels;
using System.Collections.Generic;
namespace AbstractCarRepairShopBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportRepairComponentViewModel> RepairComponents { get; set; }
    }
}

