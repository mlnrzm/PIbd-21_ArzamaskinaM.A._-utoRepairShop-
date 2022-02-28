﻿using System.ComponentModel;

namespace AbstractCarRepairShopContracts.ViewModels
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия (ремонта)
    /// </summary>
    public class ComponentViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название компонента")]
        public string ComponentName { get; set; }
    }

}
