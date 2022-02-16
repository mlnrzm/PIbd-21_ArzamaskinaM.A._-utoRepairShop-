using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.ViewModels;
using System.Collections.Generic;

namespace AbstractCarRepairShopContracts.StoragesContracts
{
    public interface IRepairStorage
    {
        List<RepairViewModel> GetFullList();
        List<RepairViewModel> GetFilteredList(RepairBindingModel model);
        RepairViewModel GetElement(RepairBindingModel model);
        void Insert(RepairBindingModel model);
        void Update(RepairBindingModel model);
        void Delete(RepairBindingModel model);
    }
}
