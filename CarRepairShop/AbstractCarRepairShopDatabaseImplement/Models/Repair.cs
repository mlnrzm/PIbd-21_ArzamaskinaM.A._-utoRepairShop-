using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractCarRepairShopDatabaseImplement.Models
{
    /// <summary>
    /// Изделие (ремонт)
    /// </summary>
    public class Repair
    {
        public int Id { get; set; }
        [Required]
        public string RepairName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("RepairId")]
        public virtual List<Order> Orders { get; set; }
        [ForeignKey("RepairId")]
        public virtual List<RepairComponent> RepairComponents { get; set; }
    }
}
