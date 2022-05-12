using System;
using System.ComponentModel;

namespace AbstractCarRepairShopContracts.ViewModels
{
    /// <summary>
    /// Исполнитель
    /// </summary>
    public class ImplementerViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО")]
        public string Name { get; set; }
        [DisplayName("Время работы")]
        public int WorkingTime { get; set; }

        [DisplayName("Время простоя")]
        public int PauseTime { get; set; }
    }
}
