using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Webhook.Model.Entities;
using Webhook.ViewModels;
using System;
using System.IO;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using Image = SixLabors.ImageSharp.Image;
using System.Text.RegularExpressions;


namespace Webhook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {

        [HttpPost]
        public IActionResult CreateImage([FromBody] string data)
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

            return Ok("ویدیو با موفقیت ذخیره شد.");

        }
        [Route("CreateVoice")]
        [HttpPost]
        public IActionResult CreateVoice([FromBody] string data)
        {
            
            // ۱. حذف header Base64 (در صورت وجود)
            if (data.StartsWith("data:audio/wav;base64,")) // تغییر فرمت به مورد نیاز
            {
                data = data.Substring("data:audio/wav;base64,".Length);
            }

            // ۲. رمزگشایی رشته Base64 به آرایه بایت
            byte[] audioBytes = Convert.FromBase64String(data);

            // ۳. مشخص کردن مسیر ذخیره فایل
            string filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Files/Voice", "audio.wav"); // تغییر مسیر و نام فایل به دلخواه

            // ۴. ذخیره آرایه بایت به عنوان فایل
            System.IO.File.WriteAllBytes(filePath, audioBytes);

            return Ok("فایل  با موفقیت ذخیره شد.");

        }

        [Route("CreateVideo")]
        [HttpPost]
        public IActionResult CreateVideo([FromBody] string data)
        {

            // ۱. حذف header Base64 (در صورت وجود)
            if (data.StartsWith("data:video/mp4;base64,")) // تغییر فرمت به مورد نیاز
            {
                data = data.Substring("data:video/mp4;base64,".Length);
            }


            // ۲. حذف کاراکترهای غیرمجاز (اختیاری)
            data = data.Replace(" ", "").Replace("\n", "").Replace("\r", "");
            // اطمینان از اینکه طول Base64 مضرب ۴ باشد
            while (data.Length % 4 != 0 && Regex.IsMatch(data, @"^[a-zA-Z0-9\+/]*={0,2}$"))
            {
                data += "=";
            }

            // ۲. رمزگشایی رشته Base64 به آرایه بایت
            byte[] videoBytes = Convert.FromBase64String(data);

            // ۳. مشخص کردن مسیر ذخیره فایل
            string filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Files/Videos", "video.mp4"); // تغییر مسیر و نام فایل به دلخواه

            // ۴. ذخیره آرایه بایت به عنوان فایل
            System.IO.File.WriteAllBytes(filePath, videoBytes);




            return Ok("فایل  با موفقیت ذخیره شد.");

        }

    }
}
