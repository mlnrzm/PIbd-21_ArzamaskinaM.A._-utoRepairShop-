using AbstractCarRepairShopBusinessLogic.OfficePackage;
using AbstractCarRepairShopBusinessLogic.OfficePackage.HelperModels;
using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.BusinessLogicsContracts;
using AbstractCarRepairShopContracts.StoragesContracts;
using AbstractCarRepairShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractCarRepairShopBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IComponentStorage _componentStorage;
        private readonly IRepairStorage _repairStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;
        public ReportLogic(IRepairStorage repairStorage, IComponentStorage
       componentStorage, IOrderStorage orderStorage,
        AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord,
       AbstractSaveToPdf saveToPdf)
        {
            _repairStorage = repairStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }
        /// <summary>
        /// Получение списка ремонтов с указанием, какие компоненты используются
        /// </summary>
        /// <returns></returns>
        public List<ReportRepairComponentViewModel> GetRepair()
        {
            var components = _componentStorage.GetFullList();
            var repairs = _repairStorage.GetFullList();
            var list = new List<ReportRepairComponentViewModel>();
            foreach (var repair in repairs)
            {
                var record = new ReportRepairComponentViewModel
                {
                    RepairName = repair.RepairName,
                    RepairComponents = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in components)
                {
                    if (repair.RepairComponents.ContainsKey(component.Id))
                    {
                        record.RepairComponents.Add(new Tuple<string, int>(component.ComponentName, 
                            repair.RepairComponents[component.Id].Item2));
                        record.TotalCount += repair.RepairComponents[component.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                RepairName = x.RepairName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status.ToString()
            })
           .ToList();
        }
        /// <summary>
        /// Сохранение ремонтов в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список ремонтов",
                Repairs = _repairStorage.GetFullList()
            });
        }
        /// <summary>
        /// Сохранение ремонтов с указанием компонент в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveRepairComponentToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список изделий и компонент",
                Repairs = GetRepair()
            });
        }
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}

