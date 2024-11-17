using System.Text.Json.Serialization;

namespace Webhook.ViewModels.message 
{
    public class Message
    {
        public string? Id { get; set; }
    }
    public class ResponseData
    {
        public DataContainer? Data { get; set; }
    }

    public class DataContainer
    {
        public MessageContainer? Data { get; set; }
    }

    public class MessageContainer
    {
        public MessageDetails? Message { get; set; }
        //public object? Media { get; set; }
    }

    public class MessageDetails
    {
        public DataDetails? _Data { get; set; }
    }

    public class DataDetails
    {
        
        public QuotedMessage? QuotedMsg { get; set; }
        public string? quotedStanzaID { get; set; } = null!;

        public MessageId? quotedParticipant;

    }

    public class MessageId
    {
        public string? _Serialized { get; set; }
    }

    public class UserInfo
    {
        public string? Server { get; set; }
        public string? User { get; set; }
        public string? _Serialized { get; set; }
    }

    public class QuotedMessage
    {
        public MessageId? Id { get; set; }
       
    }

}
