using AbstractCarRepairShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AbstractCarRepairShopDatabaseImplement
{
    public class AbstractCarRepairShopDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-Q2JQENQ\SQLEXPRESS;
                Initial Catalog=AbstractCarRepairShopDatabase;
                Integrated Security=True;
                MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Repair> Repairs { set; get; }
        public virtual DbSet<RepairComponent> RepairComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
    }

}
