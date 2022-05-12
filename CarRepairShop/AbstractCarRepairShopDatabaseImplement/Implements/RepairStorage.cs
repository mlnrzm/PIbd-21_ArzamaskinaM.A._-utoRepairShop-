using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.StoragesContracts;
using AbstractCarRepairShopContracts.ViewModels;
using AbstractCarRepairShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractCarRepairShopDatabaseImplement.Implements
{
 public class RepairStorage : IRepairStorage
{
    public List<RepairViewModel> GetFullList()
    {
        using var context = new AbstractCarRepairShopDatabase();
        return context.Repairs
        .Include(rec => rec.RepairComponents)
        .ThenInclude(rec => rec.Component)
        .ToList()
        .Select(CreateModel)
        .ToList();
    }
    public List<RepairViewModel> GetFilteredList(RepairBindingModel model)
    {
        if (model == null)
        {
            return null;
        }
        using var context = new AbstractCarRepairShopDatabase();
        return context.Repairs
        .Include(rec => rec.RepairComponents)
        .ThenInclude(rec => rec.Component)
        .Where(rec => rec.RepairName.Contains(model.RepairName))
        .ToList()
        .Select(CreateModel)
        .ToList();
    }
    public RepairViewModel GetElement(RepairBindingModel model)
    {
        if (model == null)
        {
            return null;
        }
        using var context = new AbstractCarRepairShopDatabase();
        var product = context.Repairs
        .Include(rec => rec.RepairComponents)
        .ThenInclude(rec => rec.Component)
        .FirstOrDefault(rec => rec.RepairName == model.RepairName ||
        rec.Id == model.Id);
        return product != null ? CreateModel(product) : null;
    }
    public void Insert(RepairBindingModel model)
    {
        using var context = new AbstractCarRepairShopDatabase();
        using var transaction = context.Database.BeginTransaction();
        try
        {
            Repair repair_to_add = CreateModel(model, new Repair(), context);
            context.Repairs.Add(repair_to_add);
            context.SaveChanges();

                foreach (var pc in model.RepairComponents)
                {
                    context.RepairComponents.Add(new RepairComponent
                    {
                        RepairId = repair_to_add.Id,
                        ComponentId = pc.Key,
                        Count = pc.Value.Item2
                    });
                    context.SaveChanges();
                }

            transaction.Commit();
            
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
    public void Update(RepairBindingModel model)
    {
        using var context = new AbstractCarRepairShopDatabase();
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var element = context.Repairs.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element, context);
            context.SaveChanges();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
    public void Delete(RepairBindingModel model)
    {
        using var context = new AbstractCarRepairShopDatabase();
        Repair element = context.Repairs.FirstOrDefault(rec => rec.Id ==
        model.Id);
        if (element != null)
        {
            context.Repairs.Remove(element);
            context.SaveChanges();
        }
        else
        {
            throw new Exception("Элемент не найден");
        }
    }
    private static Repair CreateModel(RepairBindingModel model, Repair product,
   AbstractCarRepairShopDatabase context)
    {
        product.RepairName = model.RepairName;
        product.Price = model.Price;
            if (model.Id.HasValue)
            {
                var productComponents = context.RepairComponents.Where(rec =>
               rec.RepairId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.RepairComponents.RemoveRange(productComponents.Where(rec =>
               !model.RepairComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in productComponents)
                {
                    updateComponent.Count =
                   model.RepairComponents[updateComponent.ComponentId].Item2;
                    model.RepairComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
                // добавили новые
                foreach (var pc in model.RepairComponents)
                {
                    context.RepairComponents.Add(new RepairComponent
                    {
                        RepairId = product.Id,
                        ComponentId = pc.Key,
                        Count = pc.Value.Item2
                    });
                    context.SaveChanges();
                }
            }
        return product;
    }
    private static RepairViewModel CreateModel(Repair product)
    {
        return new RepairViewModel
        {
            Id = product.Id,
            RepairName = product.RepairName,
            Price = product.Price,
            RepairComponents = product.RepairComponents
        .ToDictionary(recPC => recPC.ComponentId,
        recPC => (recPC.Component?.ComponentName, recPC.Count))
        };
    }
}
}

