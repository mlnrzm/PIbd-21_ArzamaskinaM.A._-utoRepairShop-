using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.StoragesContracts;
using AbstractCarRepairShopContracts.ViewModels;
using AbstractCarRepairShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractCarRepairShopListImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly DataListSingleton source;
        public WarehouseStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<WarehouseViewModel> GetFullList()
        {
            List<WarehouseViewModel> result = new();
            foreach (var component in source.Warehouse)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<WarehouseViewModel> result = new();
            foreach (var WareHouse in source.Warehouse)
            {
                if (WareHouse.WarehouseName.Contains(model.WarehouseName))
                {
                    result.Add(CreateModel(WareHouse));
                }
            }
            return result;
        }
        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var wareHouse in source.Warehouse)
            {
                if (wareHouse.Id == model.Id || wareHouse.WarehouseName == model.WarehouseName)
                {
                    return CreateModel(wareHouse);
                }
            }
            return null;
        }
        public void Insert(WarehouseBindingModel model)
        {
            Warehouse tempWareHouse = new() 
            { Id = 1, WarehouseComponents = new Dictionary<int, (string, int)>() };

            foreach (var WareHouse in source.Warehouse)
            {
                if (WareHouse.Id >= tempWareHouse.Id)
                {
                    tempWareHouse.Id = WareHouse.Id + 1;
                }
            }
            source.Warehouse.Add(CreateModel(model, tempWareHouse));
        }
        public void Update(WarehouseBindingModel model)
        {
            Warehouse tempWarehouse = null;
            foreach (var Warehouse in source.Warehouse)
            {
                if (Warehouse.Id == model.Id)
                {
                    tempWarehouse = Warehouse;
                }
            }
            if (tempWarehouse == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempWarehouse);
        }
        public void Delete(WarehouseBindingModel model)
        {
            for (int i = 0; i < source.Warehouse.Count; ++i)
            {
                if (source.Warehouse[i].Id == model.Id)
                {
                    source.Warehouse.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse wareHouse)
        {
            wareHouse.WarehouseName = model.WarehouseName;
            wareHouse.ResponsibleName = model.ResponsibleName;
            wareHouse.DateCreate = model.DateCreate;

            foreach (var key in wareHouse.WarehouseComponents.Keys.ToList())
            {
                if (!model.WarehouseComponents.ContainsKey(key))
                {
                    wareHouse.WarehouseComponents.Remove(key);
                }
            }
            foreach (var component in model.WarehouseComponents)
            {
                if (wareHouse.WarehouseComponents.ContainsKey(component.Key))
                {
                    wareHouse.WarehouseComponents[component.Key] = model.WarehouseComponents[component.Key];
                }
                else
                {
                    wareHouse.WarehouseComponents.Add(component.Key, model.WarehouseComponents[component.Key]);
                }
            }
            return wareHouse;
        }
        private WarehouseViewModel CreateModel(Warehouse Warehouse)
        {
            Dictionary<int, (string, int)> WarehouseComponents = new();
            foreach (var sc in Warehouse.WarehouseComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (sc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                WarehouseComponents.Add(sc.Key, (componentName, sc.Value.Item2));
            }
            return new WarehouseViewModel
            {
                Id = Warehouse.Id,
                WarehouseName = Warehouse.WarehouseName,
                ResponsibleName = Warehouse.ResponsibleName,
                DateCreation = Warehouse.DateCreate,
                WarehouseComponents = WarehouseComponents
            };
        }
        public bool CheckWriteOff(CheckWriteOffBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
