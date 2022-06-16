using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.StoragesContracts;
using AbstractCarRepairShopContracts.ViewModels;
using AbstractCarRepairShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractCarRepairShopFileImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly FileDataListSingleton source;

        public WarehouseStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<WarehouseViewModel> GetFullList()
        {
            return source.Warehouses.Select(CreateModel).ToList();
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Warehouses.Where(rec => rec.WarehouseName.Contains(model.WarehouseName)).Select(CreateModel).ToList();
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var wareHouse = source.Warehouses.FirstOrDefault(rec => rec.WarehouseName == model.WarehouseName || rec.Id == model.Id);
            return wareHouse != null ? CreateModel(wareHouse) : null;
        }

        public void Insert(WarehouseBindingModel model)
        {
            int maxId = source.Warehouses.Count > 0 ? source.Warehouses.Max(rec => rec.Id) : 0;
            var element = new Warehouse { Id = maxId + 1, WarehouseComponents = new Dictionary<int, int>() };
            source.Warehouses.Add(CreateModel(model, element));
        }

        public void Update(WarehouseBindingModel model)
        {
            var element = source.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        public void Delete(WarehouseBindingModel model)
        {
            Warehouse element = source.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Warehouses.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public bool CheckWriteOff(CheckWriteOffBindingModel model)
        {
            var list = GetFullList();
            var neccesary = new Dictionary<int, int>(source.Repairs.FirstOrDefault(rec => rec.Id == model.RepairId).RepairComponents);
            var available = new Dictionary<int, int>();
            neccesary.ToDictionary(kvp => neccesary[kvp.Key] *= model.Count);

            foreach (var wareHouseComponents in list.Select(rec => rec.WarehouseComponents))
            {
                foreach (var component in wareHouseComponents)
                {
                    if (available.ContainsKey(component.Key))
                    {
                        available[component.Key] += component.Value.Item2;
                    }
                    else
                    {
                        available.Add(component.Key, component.Value.Item2);
                    }
                }
            }

            bool can = available.ToList().Where(comp => neccesary.ContainsKey(comp.Key)).All(component => component.Value >= (int)neccesary[component.Key]);
            if (!can || available.Count == 0)
            {
                return false;
            }

            foreach (var wareHouse in list)
            {
                var wareHouseComponents = wareHouse.WarehouseComponents;
                foreach (var key in wareHouse.WarehouseComponents.Keys)
                {
                    var value = wareHouse.WarehouseComponents[key];
                    if (neccesary.ContainsKey(key))
                    {
                        if (value.Item2 > neccesary[key])
                        {
                            wareHouseComponents[key] = (value.Item1, value.Item2 - neccesary[key]);
                            neccesary[key] = 0;
                        }
                        else
                        {
                            wareHouseComponents[key] = (value.Item1, 0);
                            neccesary[key] -= value.Item2;
                        }
                        Update(new WarehouseBindingModel
                        {
                            Id = wareHouse.Id,
                            WarehouseName = wareHouse.WarehouseName,
                            ResponsibleName = wareHouse.ResponsibleName,
                            DateCreate = wareHouse.DateCreation,
                            WarehouseComponents = wareHouseComponents
                        });
                    }
                }
            }
            return true;
        }

        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.ResponsibleName = model.ResponsibleName;
            warehouse.DateCreation = model.DateCreate;
            foreach (var key in warehouse.WarehouseComponents.Keys.ToList())
            {
                if (!model.WarehouseComponents.ContainsKey(key))
                {
                    warehouse.WarehouseComponents.Remove(key);
                }
            }
            foreach (var component in model.WarehouseComponents)
            {
                if (warehouse.WarehouseComponents.ContainsKey(component.Key))
                {
                    warehouse.WarehouseComponents[component.Key] = model.WarehouseComponents[component.Key].Item2;
                }
                else
                {
                    warehouse.WarehouseComponents.Add(component.Key, model.WarehouseComponents[component.Key].Item2);
                }
            }
            return warehouse;
        }

        private WarehouseViewModel CreateModel(Warehouse wareHouse)
        {
            return new WarehouseViewModel
            {
                Id = wareHouse.Id,
                WarehouseName = wareHouse.WarehouseName,
                ResponsibleName = wareHouse.ResponsibleName,
                DateCreation = wareHouse.DateCreation,
                WarehouseComponents = wareHouse.WarehouseComponents
                    .ToDictionary(recPC => recPC.Key, recPC =>
                    (source.Components.FirstOrDefault(recC => recC.Id == recPC.Key)?.ComponentName, recPC.Value))
            };
        }
    }
}
