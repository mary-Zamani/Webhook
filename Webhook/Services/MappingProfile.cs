using AutoMapper;
using Webhook.Model.Entities;
using Webhook.ViewModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<HookData, ReceivedMessages>()
             .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType))
             .ForMember(dest => dest.Message_body, opt => opt.MapFrom(src => src.Data.message.body))
             .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Data.message.type))
             .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.Data.message.from))
             .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.Data.message.to))
             .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.Data.message._data.id.id))
             .ForMember(dest => dest.MessageId_serialized, opt => opt.MapFrom(src => src.Data.message._data.id._serialized))
             .ForMember(dest => dest.Media_mimeType, opt => opt.MapFrom(src => src.Data.media.mimetype))
             .ForMember(dest => dest.Media_data, opt => opt.MapFrom(src => src.Data.media.data))
             .ForMember(dest => dest.Media_filename, opt => opt.MapFrom(src => src.Data.media.filename))
             .ForMember(dest => dest.Media_filesize, opt => opt.MapFrom(src => src.Data.media.filesize))
             .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(_ => DateTime.Now)); // مقدار پیش‌فرض
             //.ForMember(dest => dest.json, opt => opt.MapFrom(src => System.Text.Json.JsonSerializer.Serialize(src))); // ذخیره کل JSON
    }
}
