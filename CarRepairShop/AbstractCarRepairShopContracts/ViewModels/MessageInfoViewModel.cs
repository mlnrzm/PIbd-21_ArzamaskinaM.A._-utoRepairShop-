using System;
using AbstractCarRepairShopContracts.Attributes;

namespace AbstractCarRepairShopContracts.ViewModels
{
    /// <summary>
    /// Сообщения, приходящие на почту
    /// </summary>
    public class MessageInfoViewModel
    {
        [Column(title: "Номер", width: 50, visible: false)]
        public string MessageId { get; set; }
        [Column(title: "Отправитель", width: 150)]
        public string SenderName { get; set; }
        [Column(title: "Дата письма", width: 100)]
        public DateTime DateDelivery { get; set; }
        [Column(title: "Заголовок", width: 100)]
        public string Subject { get; set; }
        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.AllCells)]
        public string Body { get; set; }

    }
}
