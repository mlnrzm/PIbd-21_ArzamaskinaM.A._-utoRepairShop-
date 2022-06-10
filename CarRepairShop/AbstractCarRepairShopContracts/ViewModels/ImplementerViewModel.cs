using AbstractCarRepairShopContracts.Attributes;

namespace AbstractCarRepairShopContracts.ViewModels
{
    /// <summary>
    /// Исполнитель
    /// </summary>
    public class ImplementerViewModel
    {
        [Column(title: "Номер", width: 50, visible: false)]
        public int Id { get; set; }
        [Column(title: "ФИО исполнителя", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Name { get; set; }
        [Column(title: "Время работы", width: 100)]
        public int WorkingTime { get; set; }
        [Column(title: "Время отдыха", width: 100)]
        public int PauseTime { get; set; }
    }
}
