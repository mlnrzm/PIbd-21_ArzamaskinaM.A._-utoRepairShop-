using System;
using System.ComponentModel;

namespace AbstractCarRepairShopContracts.ViewModels
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class ClientViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО")]
        public string Name { get; set; }
        [DisplayName("Логин")]
        public string Login { get; set; }
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
