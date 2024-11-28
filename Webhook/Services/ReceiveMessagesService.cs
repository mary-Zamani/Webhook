using HangfireApp.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Webhook.Model.Entities;
using Webhook.ViewModels;
using RestSharp;
using Webhook.ViewModels.message;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using static System.Runtime.InteropServices.JavaScript.JSType;

using Image = SixLabors.ImageSharp.Image;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;




namespace Webhook.Services
{
    public class ReceiveMessagesService
    {
        public readonly DataBaseContext _db;
        public readonly int _instance;
        public readonly string _token;
        private readonly string _apiUrl;
        private readonly string _bearerToken;


        public ReceiveMessagesService(DataBaseContext dbContext)
        {
            _db = dbContext;
            var wapObj = new waapi();
            wapObj = _db.waapi.FirstOrDefault();
            if (wapObj == null) return;
           
                _instance = _db.waapi.FirstOrDefault().InstanceId;
                _token = _db.waapi.FirstOrDefault().token;
            
            _apiUrl = $"https://waapi.app/api/v1/instances/{_instance}/client/action/get-message-by-id";
            _bearerToken = _token;
        }
 
        public async Task AddUser(ReceivedMessages user)
        {
            await _db.ReceivedMessages.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<ReceivedMessages> GetAllUser(string name)
        {
            ReceivedMessages result = new ReceivedMessages();
            result = await _db.ReceivedMessages.Where(i => i.MessageId == name).FirstOrDefaultAsync();
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

            var responseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            result = "false_" + responseData.Data.Data.Message._Data.quotedParticipant._Serialized + "_" +
               responseData.Data.Data.Message._Data.quotedStanzaID;

            return result;
        }
        public void createFile(string serverPath, string type,string data,string data_type,int id, out string fileName)
        {
            fileName = "";
            if (type == "image" && data != null)
            {

                data = Regex.Replace(data, @"\s+", "");
                // پاک کردن کاراکترهای اضافی
                data = data.Trim();
                data = data.Replace("\n", "").Replace("\r", "").Replace(" ", "");

                // اطمینان از اینکه طول Base64 مضرب ۴ باشد
                while (data.Length % 4 != 0)
                {
                    data += "=";
                }

                byte[] videoBytes = Convert.FromBase64String(data);

                using (var ms = new MemoryStream(videoBytes))
                {
                    // تبدیل باینری به تصویر با استفاده از ImageSharp
                    var image = Image.Load<Rgba32>(ms);
                    using (var outputStream = new MemoryStream())
                    {
                        image.Save(outputStream, new PngEncoder());
                       
                        
                        //var imaged =File(outputStream.ToArray(), id + "_image/" + data_type);

                        //ذخیره تصویر در فایل
                        string path = serverPath;// Directory.GetCurrentDirectory() + "/Files/Images";
                        fileName = id + "_image." + data_type;
                        var filePath = Path.Combine(path, fileName);
                        System.IO.File.WriteAllBytes(filePath, outputStream.ToArray());
                    }
                }
            }
            else if (type == "ptt" && data != null)
            {
                // ۱. حذف header Base64 (در صورت وجود)
                if (data.StartsWith("data:audio/wav;base64,")) // تغییر فرمت به مورد نیاز
                {
                    data = data.Substring("data:audio/wav;base64,".Length);
                }

                // ۲. رمزگشایی رشته Base64 به آرایه بایت
                byte[] audioBytes = Convert.FromBase64String(data);
                fileName = id + "_audio.wav";
                // ۳. مشخص کردن مسیر ذخیره فایل
                string filePath = Path.Combine(serverPath, fileName); // تغییر مسیر و نام فایل به دلخواه

                // ۴. ذخیره آرایه بایت به عنوان فایل
                System.IO.File.WriteAllBytes(filePath, audioBytes);

            }
            else if (type == "document" && data != null)
            {
                byte[] pdfBytes = Convert.FromBase64String(data);

                // مسیر فایل خروجی (مثال: wwwroot/files/output.pdf)
                fileName = id + "_" + fileName;// body.Data.media.filename;
                string outputPath = Path.Combine(serverPath, fileName);
                // ذخیره کردن داده‌های باینری به عنوان فایل PDF
                System.IO.File.WriteAllBytes(outputPath, pdfBytes);
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                try
                {
                    var  Obj = _db.ReceivedMessages.Where(i => i.Id == id).FirstOrDefault();
                    Obj.FileName=fileName;
                      _db.SaveChangesAsync();
                     
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }

    }
}
