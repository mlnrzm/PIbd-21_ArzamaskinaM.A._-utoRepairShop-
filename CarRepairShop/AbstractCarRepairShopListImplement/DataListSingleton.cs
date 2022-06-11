using AbstractCarRepairShopListImplement.Models;
using System.Collections.Generic;

namespace AbstractCarRepairShopListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Repair> Repairs { get; set; }
        public List<Warehouse> Warehouse { get; set; }
        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>();
            Repairs = new List<Repair>();
            Warehouse = new List<Warehouse>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}