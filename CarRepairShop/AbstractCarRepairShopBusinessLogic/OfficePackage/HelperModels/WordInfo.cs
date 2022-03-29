using AbstractCarRepairShopContracts.ViewModels;
using System.Collections.Generic;
namespace AbstractCarRepairShopBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<RepairViewModel> Repairs { get; set; }
    }
}
