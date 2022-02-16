using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.StoragesContracts;
using AbstractCarRepairShopContracts.ViewModels;
using AbstractCarRepairShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractCarRepairShopListImplement.Implements
{
    public class RepairStorage : IRepairStorage
    {
        private readonly DataListSingleton source;
        public RepairStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<RepairViewModel> GetFullList()
        {
            var result = new List<RepairViewModel>();
            foreach (var component in source.Repairs)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<RepairViewModel> GetFilteredList(RepairBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<RepairViewModel>();
            foreach (var repair in source.Repairs)
            {
                if (repair.RepairName.Contains(model.RepairName))
                {
                    result.Add(CreateModel(repair));
                }
            }
            return result;
        }
        public RepairViewModel GetElement(RepairBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var repair in source.Repairs)
            {
                if (repair.Id == model.Id || repair.RepairName ==
                model.RepairName)
                {
                    return CreateModel(repair);
                }
            }
            return null;
        }
        public void Insert(RepairBindingModel model)
        {
            var tempProduct = new Repair
            {
                Id = 1,
                RepairComponents = new Dictionary<int, int>()
            };
            foreach (var repair in source.Repairs)
            {
                if (repair.Id >= tempProduct.Id)
                {
                    tempProduct.Id = repair.Id + 1;
                }
            }
            source.Repairs.Add(CreateModel(model, tempProduct));
        }
        public void Update(RepairBindingModel model)
        {
            Repair tempProduct = null;
            foreach (var repair in source.Repairs)
            {
                if (repair.Id == model.Id)
                {
                    tempProduct = repair;
                }
            }
            if (tempProduct == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempProduct);
        }
        public void Delete(RepairBindingModel model)
        {
            for (int i = 0; i < source.Repairs.Count; ++i)
            {
                if (source.Repairs[i].Id == model.Id)
                {
                    source.Repairs.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private static Repair CreateModel(RepairBindingModel model, Repair repair)
        {
            repair.RepairName = model.RepairName;
            repair.Price = model.Price;
            // удаляем убранные
            foreach (var key in repair.RepairComponents.Keys.ToList())
            {
                if (!model.RepairComponents.ContainsKey(key))
                {
                    repair.RepairComponents.Remove(key);
                }
            }
            // обновляем существующие и добавляем новые
            foreach (var component in model.RepairComponents)
            {
                if (repair.RepairComponents.ContainsKey(component.Key))
                {
                    repair.RepairComponents[component.Key] =
                    model.RepairComponents[component.Key].Item2;
                }
                else
                {
                    repair.RepairComponents.Add(component.Key,
                    model.RepairComponents[component.Key].Item2);
                }
            }
            return repair;
        }
        private RepairViewModel CreateModel(Repair repair)
        {
            // требуется дополнительно получить список компонентов для ремонта с названиями и их количество
            var repairComponents = new Dictionary<int, (string, int)>();
            foreach (var pc in repair.RepairComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (pc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                repairComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new RepairViewModel
            {
                Id = repair.Id,
                RepairName = repair.RepairName,
                Price = repair.Price,
                RepairComponents = repairComponents
            };
        }
    }
}
