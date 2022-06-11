using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.StoragesContracts;
using AbstractCarRepairShopContracts.ViewModels;
using AbstractCarRepairShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AbstractCarRepairShopFileImplement.Implements
{
    public class RepairStorage : IRepairStorage
    {
        private readonly FileDataListSingleton source;
        public RepairStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<RepairViewModel> GetFullList()
        {
            return source.Repairs.Select(CreateModel).ToList();
        }
        public List<RepairViewModel> GetFilteredList(RepairBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Repairs
            .Where(rec => rec.RepairName.Contains(model.RepairName))
            .Select(CreateModel)
            .ToList();
        }
        public RepairViewModel GetElement(RepairBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var product = source.Repairs
            .FirstOrDefault(rec => rec.RepairName == model.RepairName || rec.Id
           == model.Id);
            return product != null ? CreateModel(product) : null;
        }
        public void Insert(RepairBindingModel model)
        {
        int maxId = source.Repairs.Count > 0 ? source.Components.Max(rec => rec.Id): 0;
            var element = new Repair
            {
                Id = maxId + 1,
                RepairComponents = new Dictionary<int, int>()
            };
            source.Repairs.Add(CreateModel(model, element));
        }
        public void Update(RepairBindingModel model)
        {
            var element = source.Repairs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(RepairBindingModel model)
        {
            Repair element = source.Repairs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Repairs.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Repair CreateModel(RepairBindingModel model, Repair product)
        {
            product.RepairName = model.RepairName;
            product.Price = model.Price;
            // удаляем убранные
            foreach (var key in product.RepairComponents.Keys.ToList())
            {
                if (!model.RepairComponents.ContainsKey(key))
                {
                    product.RepairComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.RepairComponents)
            {
                if (product.RepairComponents.ContainsKey(component.Key))
                {
                    product.RepairComponents[component.Key] =
                   model.RepairComponents[component.Key].Item2;
                }
                else
                {
                    product.RepairComponents.Add(component.Key,
                   model.RepairComponents[component.Key].Item2);
                }
            }
            return product;
        }
        private RepairViewModel CreateModel(Repair product)
        {
            return new RepairViewModel
            {
                Id = product.Id,
                RepairName = product.RepairName,
                Price = product.Price,
                RepairComponents = product.RepairComponents
             .ToDictionary(recPC => recPC.Key, recPC =>
             (source.Components.FirstOrDefault(recC => recC.Id ==
            recPC.Key)?.ComponentName, recPC.Value))
             };
        }
    }
}
