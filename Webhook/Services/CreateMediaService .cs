using HangfireApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Text.RegularExpressions;
using Webhook.Model.Entities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using Image = SixLabors.ImageSharp.Image;
 
 

namespace Webhook.Services
{
    public class CreateMediaService: ControllerBase
    {
        public readonly DataBaseContext _dbContext;

        public CreateMediaService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(ReceivedMessages user)
        {
            await _dbContext.ReceivedMessages.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public void CreateImage(  string data, int Id)
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
                    var imaged = File(outputStream.ToArray(), "imagwere/png");

                    //ذخیره تصویر در فایل
                    string path = Directory.GetCurrentDirectory() + "/Files/Images";
                    var filePath = Path.Combine(path, "image5.png");
                    System.IO.File.WriteAllBytes(filePath, imaged.FileContents);
                }
            }



        }
       
    }
}
