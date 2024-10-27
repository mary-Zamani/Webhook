using HangfireApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Webhook.Model.Entities;

namespace Webhook.Services
{
    public class ReceiveMessagesService
    {
        public readonly DataBaseContext _dbContext;

        public ReceiveMessagesService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(ReceivedMessages user)
        {
            await _dbContext.ReceivedMessages.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ReceivedMessages> GetAllUser(string name)
        {
            ReceivedMessages result = new ReceivedMessages();
            result = await _dbContext.ReceivedMessages.Where(i => i.MessageId == name).FirstOrDefaultAsync();
            return result;

        }
    }
}
