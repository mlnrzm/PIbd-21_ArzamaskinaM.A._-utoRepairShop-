using AbstractCarRepairShopContracts.Attributes;

namespace AbstractCarRepairShopContracts.ViewModels
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 50, visible: false)]
        public int Id { get; set; }
        [Column(title: "ФИО клиента", width: 150)]
        public string Name { get; set; }
        [Column(title: "Логин", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Login { get; set; }
        [Column(title: "Пароль", width: 150)]
        public string Password { get; set; }
    }
}
