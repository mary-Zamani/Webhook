using HangfireApp.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Webhook.Model.Entities;
using Webhook.ViewModels;
using RestSharp;
using Webhook.ViewModels.message;

namespace Webhook.Services
{
    public class ReceiveMessagesService
    {
        public readonly DataBaseContext _dbContext;
        private readonly string _apiUrl = "https://waapi.app/api/v1/instances/25573/client/action/get-message-by-id";
        private readonly string _bearerToken = "YrgjSPik5AJMIWXEYp7fKCzHeaaxPK0rbTcEytgZed652146";


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

        public async Task<string> GetMessageByIdAsync(string messageId)
        {

            string result = "";
            var options = new RestClientOptions(_apiUrl);
            var client = new RestClient(options);
            var request = new RestRequest();

            // اضافه کردن هدرها
            request.AddHeader("accept", "application/json");
            request.AddHeader("authorization", $"Bearer {_bearerToken}");
            request.AddJsonBody(new { messageId });

            // ارسال درخواست
            var response = await client.PostAsync(request);

            if (!response.IsSuccessful)
            {
                
                throw new Exception($"Error retrieving message: {response.StatusCode} - {response.Content}");
            }

            // پردازش پاسخ
           
            var responseData  = JsonConvert.DeserializeObject<ResponseData>(response.Content);
              result = responseData.Data.Data.Message._Data.QuotedMsg.Id._Serialized; 
             
            return result;
        }

    }
}
