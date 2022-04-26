using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.ViewModels;

namespace AbstractCarRepairShopContracts.BusinessLogicsContracts
{
    public interface IImplementerLogic
    {
        List<ImplementerViewModel> Read(ImplementerBindingModel model);

        void CreateOrUpdate(ImplementerBindingModel model);

        void Delete(ImplementerBindingModel model);
    }
}
