﻿using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.ViewModels;
using AbstractCarRepairShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractCarRepairShopListImplement.Implements
{
    public class MessageInfoStorage
    {
        private readonly DataListSingleton source;
        public MessageInfoStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<MessageInfoViewModel> GetFullList()
        {
            var result = new List<MessageInfoViewModel>();
            foreach (var message in source.Messages)
            {
                result.Add(CreateModel(message));
            }
            return result;
        }
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<MessageInfoViewModel>();
            foreach (var message in source.Messages)
            {
                if ((model.ClientId.HasValue && message.ClientId == model.ClientId) ||
                    (!model.ClientId.HasValue && message.DateDelivery.Date == model.DateDelivery.Date))
                {
                    result.Add(CreateModel(message));
                }
            }
            return result;
        }
        public void Insert(MessageInfoBindingModel model)
        {
            source.Messages.Add(CreateModel(model, new MessageInfo()));
        }
        private MessageInfo CreateModel(MessageInfoBindingModel model,
            MessageInfo message)
        {
            string clientFIO = string.Empty;
            foreach (var client in source.Clients)
            {
                if (client.Id == model.ClientId)
                {
                    clientFIO = client.Name;
                    break;
                }
            }
            message.MessageId = model.MessageId;
            message.SenderName = clientFIO;
            message.Body = model.Body;
            message.ClientId = model.ClientId;
            message.DateDelivery = model.DateDelivery;
            message.Subject = model.Subject;
            return message;
        }
        private MessageInfoViewModel CreateModel(MessageInfo message)
        {
            return new MessageInfoViewModel
            {
                MessageId = message.MessageId,
                Body = message.Body,
                DateDelivery = message.DateDelivery,
                SenderName = message.SenderName,
                Subject = message.Subject
            };
        }
    }
}
