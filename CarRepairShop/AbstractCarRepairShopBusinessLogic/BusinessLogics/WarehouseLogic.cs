using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.BusinessLogicsContracts;
using AbstractCarRepairShopContracts.StoragesContracts;
using AbstractCarRepairShopContracts.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractCarRepairShopBusinessLogic.BusinessLogics
{
    public class WarehouseLogic : IWarehouseLogic
    {
		private readonly IWarehouseStorage _warehouseStorage;
		private readonly IComponentStorage _componentStorage;
		public WarehouseLogic(IWarehouseStorage warehouseStorage, IComponentStorage componentStorage)
		{
			_warehouseStorage = warehouseStorage;
			_componentStorage = componentStorage;
		}
		public List<WarehouseViewModel> Read(WarehouseBindingModel model)
		{
			if (model == null)
			{
				return _warehouseStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<WarehouseViewModel> { _warehouseStorage.GetElement(model) };
			}
			return _warehouseStorage.GetFilteredList(model);
		}
		public void CreateOrUpdate(WarehouseBindingModel model)
		{
			var element = _warehouseStorage.GetElement(new WarehouseBindingModel { WarehouseName = model.WarehouseName });
			if (element != null && element.Id != model.Id)
			{
				throw new Exception("Уже есть склад с таким названием");

			}

			if (model.Id.HasValue)
			{
				_warehouseStorage.Update(model);
			}
			else
			{
				_warehouseStorage.Insert(model);
			}
		}
		public void Delete(WarehouseBindingModel model)
		{
			var element = _warehouseStorage.GetElement(new WarehouseBindingModel { Id = model.Id });
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			_warehouseStorage.Delete(model);
		}
		public void AddComponent(WarehouseBindingModel model, int ComponentId, int Count)
		{
			var wareHouse = _warehouseStorage.GetElement(new WarehouseBindingModel
			{
				Id = model.Id
			});
			if (wareHouse == null)
			{
				throw new Exception("Элемент не найден");
			}
			var component = _componentStorage.GetElement(new ComponentBindingModel
			{
				Id = ComponentId
			});
			if (component == null)
			{
				throw new Exception("Элемент не найден");
			}
			if (wareHouse.WarehouseComponents.ContainsKey(ComponentId))
			{
				int oldCount = wareHouse.WarehouseComponents[ComponentId].Item2;
				wareHouse.WarehouseComponents[ComponentId] = (component.ComponentName, oldCount + Count);
			}
			else
			{
				wareHouse.WarehouseComponents.Add(ComponentId, (component.ComponentName, Count));
			}
			_warehouseStorage.Update(new WarehouseBindingModel
			{
				Id = wareHouse.Id,
				WarehouseName = wareHouse.WarehouseName,
				ResponsibleName = wareHouse.ResponsibleName,
				DateCreate = wareHouse.DateCreation,
				WarehouseComponents = wareHouse.WarehouseComponents
			});
		}
	}
}
