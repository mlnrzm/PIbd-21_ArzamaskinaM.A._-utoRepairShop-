using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.ViewModels;
using System.Collections.Generic;

namespace AbstractCarRepairShopContracts.BusinessLogicsContracts
{
    public interface IWarehouseLogic
    {
        List<WarehouseViewModel> Read(WarehouseBindingModel model);
        void CreateOrUpdate(WarehouseBindingModel model);
        void Delete(WarehouseBindingModel model);
        void AddComponent(WarehouseBindingModel model, int componentId, int Count);
    }
}
