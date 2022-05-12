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
        private readonly string ClientFileName = "Client.xml";
        private readonly string ImplementerFileName = "Implementer.xml";
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string RepairFileName = "Repair.xml";
        private readonly string MessageFileName = "Message.xml";
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Repair> Repairs { get; set; }
        public List<MessageInfo> Messages { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Repairs = LoadRepairs();
            Clients = LoadClients();
            Implementers = LoadImplementers();
            Messages = LoadMessages();
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
            SaveClients();
            SaveImplementers();
            SaveMessages();
        }
        private List<MessageInfo> LoadMessages()
        {
            var list = new List<MessageInfo>();
            if (File.Exists(MessageFileName))
            {
                var xDocument = XDocument.Load(MessageFileName);
                var xElements = xDocument.Root.Elements("Message").ToList();
                int? clientId;
                foreach (var elem in xElements)
                {
                    clientId = null;
                    if (elem.Element("ClientId").Value != "")
                    {
                        clientId = Convert.ToInt32(elem.Element("ClientId").Value);
                    }
                    list.Add(new MessageInfo
                    {
                        MessageId = elem.Attribute("MessageId").Value,
                        ClientId = clientId,
                        Body = elem.Element("Body").Value,
                        SenderName = elem.Element("SenderName").Value,
                        Subject = elem.Element("Subject").Value,
                        DateDelivery = DateTime.Parse(elem.Element("DateDelivery").Value)
                    });
                }
            }
            return list;
        }
        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                var xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        Name = elem.Element("Name").Value,
                        Login = elem.Element("Login").Value,
                        Password = elem.Element("Password").Value
                    });
                }
            }
            return list;
        }
        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();
            if (File.Exists(ImplementerFileName))
            {
                var xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Implementer").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        Name = elem.Element("Name").Value,
                        WorkingTime = Convert.ToInt32(elem.Element("WorkingTime").Value),
                        PauseTime = Convert.ToInt32(elem.Element("PauseTime").Value)
                    });
                }
            }
            return list;
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
                    int? client_id = null;
                    int? implementer_id = null;
                    if (elem.Element("DateImplement").Value != "")
                        dt = Convert.ToDateTime(elem.Element("DateImplement").Value);
                    if (elem.Element("ClientId").Value != "")
                        client_id = Convert.ToInt32(elem.Element("ClientId").Value);
                    if (elem.Element("ImplementerId").Value != "")
                        implementer_id = Convert.ToInt32(elem.Element("ImplementerId").Value);

                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientId = (int)client_id,
                        ImplementerId = (int)implementer_id,
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
        private void SaveMessages()
        {
            if (Messages != null)
            {
                var xElement = new XElement("Messages");
                foreach (var message in Messages)
                {
                    xElement.Add(new XElement("Message",
                        new XAttribute("MessageId", message.MessageId),
                        new XElement("ClientId", message.ClientId),
                        new XElement("SenderName", message.SenderName),
                        new XElement("Subject", message.Subject),
                        new XElement("Body", message.Body),
                        new XElement("DateDelivery", message.DateDelivery)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");
                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                    new XAttribute("Id", implementer.Id),
                    new XElement("Name", implementer.Name)),
                    new XElement("WorkingTime", implementer.WorkingTime),
                    new XElement("PauseTime", implementer.PauseTime));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }
        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("Name", client.Name)),
                    new XElement("Login", client.Login),
                    new XElement("Password", client.Password));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
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
                    new XElement("ClientId", order.ClientId),
                    new XElement("ImplementerId", order.ImplementerId),
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

    }
}
