﻿ 
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using HangfireApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Webhook.Model.Entities;
using Webhook.ViewModels;
using System.Net;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Webhook.Services;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System.Text.RegularExpressions;
using SixLabors.ImageSharp;
using Image = SixLabors.ImageSharp.Image;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NAudio.Lame;
using NAudio.Wave;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting.Server;
using NVorbis;
using Concentus.Structs;
using Concentus.Oggfile;
 
 

namespace WebHookExample.Properties
{

    [ApiController]
    [Route("webhooks/{security_token}")]
    public class WebhookController : Controller
    {
        private readonly DataBaseContext _db;

      
       
        private readonly Dictionary<int, string> securityTokens;
        private readonly ReceiveMessagesService _receiveMessagesService;
      

        public WebhookController(  DataBaseContext db,  Dictionary<int, string> securityTokens, ReceiveMessagesService receiveMessagesService )
        {
             
            _db = db;
          
            this.securityTokens = _db.waapi.ToDictionary(t => t.InstanceId, t => t.token);
            _receiveMessagesService = receiveMessagesService;
             
        }

        [HttpPost]
        public async Task<IActionResult> Get(string security_token, [FromBody] HookData body)
        {

          

            string jsonData = System.Text.Json.JsonSerializer.Serialize(body);
            var log = new webhookError
            {
                json = jsonData,
                createdate = DateTime.UtcNow,

            };

            _db.webhookError.Add(log);
            await _db.SaveChangesAsync();

            string QuotedMsgId = "";

            if (body.InstanceId == 0 || body.EventType == null || body.Data == null)
            {
                Console.WriteLine("Invalid request");
                return BadRequest();
            }

            if (securityTokens.GetValueOrDefault(body.InstanceId) != security_token)
            {
                Console.WriteLine("Authentication failed");
                return Unauthorized();
            }
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    if (body.EventType == "message")
                    {
                        Console.WriteLine("Handle message event...");

                        string data = "", data_type = "", fileName = "", serverPath = "wwwroot/ticket/Files/";

                        messageModel? messageData = body.Data.message;
                        string? type = messageData != null ? messageData.@type : null;

                        if (messageData != null || !string.IsNullOrEmpty(type))
                        {
                            _dataModel? _data = messageData != null ? messageData._data : null;
                            string? messageSenderId = _data != null ? _data.@from : null;

                            var messageContent = _data != null ? _data.body : null;

                            var messageSenderPhoneNumber = !string.IsNullOrEmpty(messageSenderId) ? messageSenderId.Replace("@c.us", "") : null;
                            var messageId = messageData._data.id.id;
                            var MessageId_serialized = messageData._data.id._serialized;

                            // Run your business logic: someone has sent you a WhatsApp message
                            //=========================
                            ReceivedMessages obj = new ReceivedMessages();
                            //var obj1 = _mapper.Map<ReceivedMessages>(body);
                            obj.EventType = body.EventType;
                            obj.json = jsonData;
                            obj.From = messageSenderPhoneNumber;
                            obj.Data_body = messageContent;
                            obj.Message_body = messageData.body;
                            obj.SenderPhoneNumber = messageSenderPhoneNumber;
                            obj.Type = messageData.@type;
                            if (body.Data.media != null)
                            {

                                obj.Media_mimeType = body.Data.media.mimetype != null ? body.Data.media.mimetype.ToString() : null;
                                obj.Media_filename = body.Data.media.filename != null ? body.Data.media.filename.ToString() : null;
                                obj.Media_data = body.Data.media.data != null ? body.Data.media.data.ToString() : null;
                                obj.Media_filesize = body.Data.media != null ? body.Data.media.filesize : null;
                                data = body.Data.media.data != null ? body.Data.media.data.ToString() : null;
                                data_type = body.Data.media.mimetype != null ? body.Data.media.mimetype.ToString().Substring(6) : null;


                            }
                            obj.MessageId = messageId;
                            if (messageData.hasQuotedMsg.Value)
                            {
                                QuotedMsgId = await _receiveMessagesService.GetMessageByIdAsync(MessageId_serialized);
                                obj.QuotedMsgId = QuotedMsgId;
                            }
                            if (messageData != null)
                            {
                                obj.To = messageData._data.to;
                                obj.MessageId_serialized = MessageId_serialized;
                                obj.t = messageData._data.t;
                                obj.notifyName = messageData._data.notifyName;
                                obj.self = messageData._data.self;
                                obj.ack = messageData._data.ack;
                                obj.isNewMsg = messageData._data.isNewMsg;
                                obj.star = messageData._data.star;
                                obj.kicNotified = messageData._data.kicNotified;
                                obj.recvFresh = messageData._data.recvFresh;
                                obj.isFromTemplate = messageData._data.isFromTemplate;
                                obj.pollInvalidated = messageData._data.pollInvalidated;
                                obj.latestEditMsgKey = messageData._data.latestEditMsgKey != null ? messageData._data.latestEditMsgKey.ToString() : null;
                                obj.latestEditSenderTimestampMs = messageData._data.latestEditSenderTimestampMs != null ? messageData._data.latestEditSenderTimestampMs.ToString() : null;
                                obj.broadcast = messageData._data.broadcast;
                                obj.isVcardOverMmsDocument = messageData._data.isVcardOverMmsDocument;
                                obj.isForwarded = messageData._data.isForwarded;
                                obj.hasReaction = messageData._data.hasReaction;
                                obj.ephemeralOutOfSync = messageData._data.ephemeralOutOfSync;
                                obj.productHeaderImageRejected = messageData._data.productHeaderImageRejected;
                                obj.lastPlaybackProgress = messageData._data.lastPlaybackProgress;
                                obj.isDynamicReplyButtonsMsg = messageData._data.isDynamicReplyButtonsMsg;
                                obj.isMdHistoryMsg = messageData._data.isMdHistoryMsg;
                                obj.stickerSentTs = messageData._data.stickerSentTs;
                                obj.isAvatar = messageData._data.isAvatar;
                                obj.requiresDirectConnection = messageData._data.requiresDirectConnection;
                                obj.pttForwardedFeaturesEnabled = messageData._data.pttForwardedFeaturesEnabled;
                                obj.isEphemeral = messageData._data.isEphemeral;
                                obj.isStatusV3 = messageData._data.isStatusV3;
                                obj.hasMedia = messageData.hasMedia;
                                obj.timestamp = messageData.timestamp;
                                obj.deviceType = messageData.deviceType;
                                obj.forwardingScore = messageData.forwardingScore;
                                obj.isStatus = messageData.isStatus;
                                obj.isStarred = messageData.isStarred;
                                obj.fromMe = messageData.fromMe;
                                obj.hasQuotedMsg = messageData.hasQuotedMsg;
                                obj.isGif = messageData.isGif;
                            }

                            var result = _db.ReceivedMessages.Where(i => i.MessageId == messageId && i.json == null).ToList();
                            if (result.Count() == 0)
                            {

                                _db.ReceivedMessages.Add(obj);
                                _db.SaveChanges();
                            }

                            
                            int id = obj.Id;
                            //============create File in Path 
                            //if(messageData.@type!= "chat")
                            //_receiveMessagesService.createFile(serverPath, messageData.@type, data, data_type, id,out fileName);
                            if (messageData.@type == "image" && data != null)
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
                                        var imaged = File(outputStream.ToArray(), id + "_image/" + data_type);

                                        //ذخیره تصویر در فایل
                                        string path = serverPath;// Directory.GetCurrentDirectory() + "/Files/Images";
                                        fileName = id + "_image." + data_type;
                                        var filePath = Path.Combine(path, fileName);
                                        System.IO.File.WriteAllBytes(filePath, imaged.FileContents);
                                    }
                                }
                            }
                            else if (messageData.@type == "ptt" && data != null)
                            {

                                //====================

                                string mp3FilePath = Path.Combine(serverPath, $"{id}_audio.mp3");
                               
                                byte[] audioBytes = Convert.FromBase64String(data);
                                var filePath = Directory.GetCurrentDirectory()+"\\";// $@"C:\project\Webhook\";
                                string tempOggFile = Path.Combine( serverPath, $"{id}_audio.ogg" );
                           
                                string fileWav = Path.Combine(serverPath, $"{id}_audio.wav");
                                System.IO.File.WriteAllBytes(tempOggFile, audioBytes);

                                fileName = $"{id}_audio.wav";
                                using (FileStream fileIn = new FileStream($"{filePath}{tempOggFile}", FileMode.Open))
                                using (MemoryStream pcmStream = new MemoryStream())
                                {
                                    OpusDecoder decoder = OpusDecoder.Create(48000, 1);
                                    OpusOggReadStream oggIn = new OpusOggReadStream(decoder, fileIn);
                                    while (oggIn.HasNextPacket)
                                    {
                                        short[] packet = oggIn.DecodeNextPacket();
                                        if (packet != null)
                                        {
                                            for (int i = 0; i < packet.Length; i++)
                                            {
                                                var bytes = BitConverter.GetBytes(packet[i]);
                                                pcmStream.Write(bytes, 0, bytes.Length);
                                            }
                                        }
                                    }
                                    pcmStream.Position = 0;
                                    var wavStream = new RawSourceWaveStream(pcmStream, new WaveFormat(48000, 1));
                                    var sampleProvider = wavStream.ToSampleProvider();
                                    WaveFileWriter.CreateWaveFile16($"{filePath}{fileWav}", sampleProvider);

                                    //===========
                                    //System.IO.File.Delete(tempOggFile);

                                }
                                // خواندن فایل WAV
                                //using (var reader = new WaveFileReader($"{filePath}{fileWav}"))
                                //{
                                //    // ایجاد فایل MP3 برای نوشتن
                                //    using (var writer = new LameMP3FileWriter(mp3FilePath, reader.WaveFormat, LAMEPreset.STANDARD))
                                //    {
                                //        // خواندن داده‌ها از فایل WAV و نوشتن به فایل MP3
                                //        reader.CopyTo(writer);
                                //    }
                                //}
                                //fileName = mp3FilePath;
                                //System.IO.File.Delete(fileWav);

                            }

                            else if (messageData.@type == "document" && data != null)
                            {
                                byte[] pdfBytes = Convert.FromBase64String(data);

                                // مسیر فایل خروجی (مثال: wwwroot/files/output.pdf)
                                fileName = id + "_" + body.Data.media.filename;
                                string outputPath = Path.Combine(serverPath, fileName);
                                // ذخیره کردن داده‌های باینری به عنوان فایل PDF
                                System.IO.File.WriteAllBytes(outputPath, pdfBytes);
                            }
                            //=============================
                            var CustomObj = _db.Customers.Where(i => i.Mobile == messageSenderPhoneNumber).ToList();
                            if (CustomObj.Count() == 0)
                            {
                                 
                                Customers customObj = new Customers();
                                customObj.Mobile = messageSenderPhoneNumber;
                                customObj.Name = messageData._data.notifyName;
                                customObj.CreateDate = DateTime.Now;
                                _db.Customers.Add(customObj);
                                _db.SaveChanges();
                                //==================
                                ticket ticketObj = new ticket();
                                ticketObj.CustomerId = customObj.Id;
                                ticketObj.CustomerSendUc = customObj.Id;
                                ticketObj.CustomerMobile = messageSenderPhoneNumber;
                                ticketObj.Subject = "پیام واتس آپ ";
                                ticketObj.Discription = messageData.body;
                                ticketObj.Status = 1;
                                ticketObj.FileName = fileName;
                                string Pcurentdate = DateConverter.ConvertToPersianDate(DateAndTime.Now);
                                ticketObj.CreateDate = DateTime.Now;
                                ticketObj.PcreateDate = Pcurentdate;
                                _db.ticket.Add(ticketObj);
                                _db.SaveChanges();
                                await transaction.CommitAsync();
                            }
                            else
                            {
                              
                                var existingTicket = await _db.ticket.Where(i => i.CustomerId == CustomObj[0].Id).FirstOrDefaultAsync();
                                if (existingTicket == null)
                                {
                                    return NotFound("Product not found.");
                                }
                                if (existingTicket.Asign == null)
                                {

                                    existingTicket.Discription += " " + messageData.body;
                                    existingTicket.FileName = fileName;
                                    string Pcurentdate = DateConverter.ConvertToPersianDate(DateAndTime.Now);
                                    existingTicket.UpdateDate = DateTime.Now;
                                    existingTicket.PupdateDate = Pcurentdate;
                                   
                                    await _db.SaveChangesAsync();
                                    await transaction.CommitAsync();
                                     
                                }
                                else
                                {
                                    
                                    ticketResponse responseObj = new ticketResponse();
                                    responseObj.TicketId = existingTicket.s_id;
                                    responseObj.CustomerId = CustomObj[0].Id;
                                    responseObj.RefId = existingTicket.Asign;
                                    responseObj.Response = messageData.body;
                                    string Pcurentdate = DateConverter.ConvertToPersianDate(DateAndTime.Now);
                                    responseObj.CreateDate = DateTime.Now;
                                    responseObj.PcreateDate = Pcurentdate;
                                    responseObj.FileName = fileName;
                                    responseObj.ReceivedMessagesId = id;
                                    if (messageData.hasQuotedMsg.Value)

                                        responseObj.QuotedMsgId = QuotedMsgId;
                                    
                                    responseObj.MessageId_serialized = MessageId_serialized;
                                    _db.ticketResponse.Add(responseObj);
                                    _db.SaveChanges();
                                    if (existingTicket.ReciveUc == null)
                                    {
                                        existingTicket.CustomerSendUc = CustomObj[0].Id;
                                        existingTicket.ReciveUc = existingTicket.SendUc;
                                        existingTicket.CustomerReciveUc = null;
                                        existingTicket.SendUc = null;
                                    }
                                    
                                    existingTicket.FileName = fileName;
                                    existingTicket.UpdateDate = DateTime.Now;
                                    existingTicket.PupdateDate = Pcurentdate;
                                    await _db.SaveChangesAsync();
                                    await transaction.CommitAsync();
                                }
                            }

                        }

                        return Ok();
                    }
                    else
                    {
                        Console.WriteLine($"Cannot handle this event: {body.EventType}");
                        return NotFound();
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    var log1 = new webhookError
                    {
                        json = jsonData,
                        createdate = DateTime.UtcNow,
                        error=ex.Message,
                    };

                    _db.webhookError.Add(log1);
                    await _db.SaveChangesAsync();

                    throw;
                }
            }
        }




    }
}

