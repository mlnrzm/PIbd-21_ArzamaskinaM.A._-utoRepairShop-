using System;
using AbstractCarRepairShopContracts.Enums;
using AbstractCarRepairShopContracts.Attributes;
using System.Runtime.Serialization;

namespace AbstractCarRepairShopContracts.ViewModels
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class OrderViewModel
    {
        [Column(title: "Номер", width: 50, visible: false)]
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public int? ImplementerId { get; set; }
        public int RepairId { get; set; }
        [Column(title: "Клиент", width: 120)]
        public string ClientName { get; set; }
        [Column(title: "Исполнитель", width: 120)]
        [DataMember]
        public string ImplementerName { get; set; }
        [Column(title: "Ремонт", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string RepairName { get; set; }
        [Column(title: "Количество", width: 50)]
        public int Count { get; set; }
        [Column(title: "Сумма", width: 50)]
        public decimal Sum { get; set; }
        [Column(title: "Статус", width: 100)]
        public OrderStatus Status { get; set; }
        [Column(title: "Дата создания", width: 100)]
        public DateTime DateCreate { get; set; }
        [Column(title: "Дата выполнения", width: 100)]
        public DateTime? DateImplement { get; set; }
    }
}
