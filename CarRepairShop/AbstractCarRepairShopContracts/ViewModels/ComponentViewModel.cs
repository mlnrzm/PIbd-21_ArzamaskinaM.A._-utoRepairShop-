using AbstractCarRepairShopContracts.Attributes;

namespace AbstractCarRepairShopContracts.ViewModels
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия (ремонта)
    /// </summary>
    public class ComponentViewModel
    {
        [Column(title: "Номер", width: 50, visible: false)]
        public int Id { get; set; }
        [Column(title: "Название компонента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComponentName { get; set; }
    }

}
