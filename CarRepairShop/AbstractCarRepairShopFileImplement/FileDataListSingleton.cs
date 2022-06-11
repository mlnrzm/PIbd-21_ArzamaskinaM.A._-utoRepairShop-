using AbstractCarRepairShopContracts.Enums;
using AbstractCarRepairShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AbstractCarRepairShopFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string RepairFileName = "Repair.xml";
        private readonly string WarehouseFileName = "Warehouse.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Repair> Repairs { get; set; }
        public List<Warehouse> Warehouses { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Repairs = LoadRepairs();
            Warehouses = LoadWarehouses();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        public void SaveFileDataListSingleton()
        {
            SaveComponents();
            SaveOrders();
            SaveRepairs();
            SaveWarehouses();
        }
        private List<Component> LoadComponents()
        {
            var list = new List<Component>();
            if (File.Exists(ComponentFileName))
            {
                var xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                var xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    DateTime dt = default;
                    if (elem.Element("DateImplement").Value != "")
                        dt = Convert.ToDateTime(elem.Element("DateImplement").Value);

                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        RepairId = Convert.ToInt32(elem.Element("RepairId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), Convert.ToString(elem.Element("Status").Value), true),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = dt
                    });
                }
            }
            return list;
        }
        private List<Repair> LoadRepairs()
        {
            var list = new List<Repair>();
            if (File.Exists(RepairFileName))
            {
                var xDocument = XDocument.Load(RepairFileName);
                var xElements = xDocument.Root.Elements("Repair").ToList();
                foreach (var elem in xElements)
                {
                    var prodComp = new Dictionary<int, int>();
                    foreach (var component in
                    elem.Element("RepairComponents").Elements("RepairComponent").ToList())
                    {
                        prodComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Repair
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        RepairName = elem.Element("RepairName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        RepairComponents = prodComp
                    });
                }
            }
            return list;
        }
        private List<Warehouse> LoadWarehouses()
        {
            var list = new List<Warehouse>();
            if (File.Exists(WarehouseFileName))
            {
                XDocument xDocument = XDocument.Load(WarehouseFileName);

                var xElements = xDocument.Root.Elements("Warehouse").ToList();

                foreach (var elem in xElements)
                {
                    var warehouseComponents = new Dictionary<int, int>();
                    foreach (var ingredient in elem.Element("WarehouseComponents").Elements("WarehouseComponent").ToList())
                    {
                        warehouseComponents.Add(Convert.ToInt32(ingredient.Element("Key").Value), Convert.ToInt32(ingredient.Element("Value").Value));
                    }

                    list.Add(new Warehouse
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        WarehouseName = elem.Element("WarehouseName").Value,
                        ResponsibleName = elem.Element("ResponsibleName").Value,
                        DateCreation = Convert.ToDateTime(elem.Element("DateCreation").Value),
                        WarehouseComponents = warehouseComponents
                    });
                }
            }
            return list;
        }
        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                    new XAttribute("Id", component.Id),
                    new XElement("ComponentName", component.ComponentName)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("RepairId", order.RepairId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)
                    ));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveRepairs()
        {
            if (Repairs != null)
            {
                var xElement = new XElement("Repairs");
                foreach (var product in Repairs)
                {
                    var compElement = new XElement("RepairComponents");
                    foreach (var component in product.RepairComponents)
                    {
                        compElement.Add(new XElement("RepairComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Repair",
                     new XAttribute("Id", product.Id),
                     new XElement("RepairName", product.RepairName),
                     new XElement("Price", product.Price),
                     compElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(RepairFileName);
            }
        }
        private void SaveWarehouses()
        {
            if (Warehouses != null)
            {
                var xElement = new XElement("Warehouses");

                foreach (var wareHouse in Warehouses)
                {
                    var compElement = new XElement("WarehouseComponents");

                    foreach (var component in wareHouse.WarehouseComponents)
                    {
                        compElement.Add(new XElement("WarehouseComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }

                    xElement.Add(new XElement("Warehouse",
                        new XAttribute("Id", wareHouse.Id),
                        new XElement("WarehouseName", wareHouse.WarehouseName),
                        new XElement("ResponsibleName", wareHouse.ResponsibleName),
                        new XElement("DateCreation", wareHouse.DateCreation),
                        compElement));
                }

                XDocument xDocument = new(xElement);
                xDocument.Save(WarehouseFileName);
            }
        }
    }
}
