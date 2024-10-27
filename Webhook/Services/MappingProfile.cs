using AutoMapper;
using Webhook.Model.Entities;
using Webhook.ViewModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<HookData, ReceivedMessages>().ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType))
            .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.Data.message.@from))
            .ForMember(dest => dest.Data_body, opt => opt.MapFrom(src => src.Data.message._data.body))
            .ForMember(dest => dest.Message_body, opt => opt.MapFrom(src => src.Data.message.body));
    }
}
