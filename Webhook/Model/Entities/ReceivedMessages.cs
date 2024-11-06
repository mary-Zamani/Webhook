using System.Text.Json.Serialization;

namespace Webhook.Model.Entities
{
    public class ReceivedMessages
    {
        public int Id { get; set; }
        public string? Message_body { get; set; } = null!;
        public string? Data_body { get; set; } = null!;
        public string? Type { get; set; } = null!;
        public string?   From { get; set; } = null!;
        public string? To { get; set; } = null!;
        public string? SenderPhoneNumber { get; set; } = null!;
        public string? MessageId { get; set; } = null!;
        public string? json { get; set; } = null!;
        public string? MessageId_serialized { get; set; } = null!;
        public string? EventType { get; set; } = null!;
        public string? Media_mimeType { get; set; } = null!;
        public string? Media_data { get; set; } = null!;
        public string? Media_filename { get; set; } = null!;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public int?  t { get; set; } = null!;
        public string? notifyName { get; set; } = null!;
        public string? self { get; set; } = null!;
        public int? ack { get; set; } = null!;
        public bool? isNewMsg { get; set; } = null!;
        public bool? star { get; set; } = null!;
        public bool? kicNotified { get; set; } = null!;
        public bool? recvFresh { get; set; } = null!;
        public bool? isFromTemplate { get; set; } = null!;
        public bool? pollInvalidated { get; set; } = null!;
        public string? latestEditMsgKey { get; set; } = null!;
        public string?   latestEditSenderTimestampMs { get; set; } = null!;
        public bool? broadcast { get; set; } = null!;
        public bool? isVcardOverMmsDocument { get; set; } = null!;
        public bool? isForwarded { get; set; } = null!;
        public bool? hasReaction { get; set; } = null!;
        public bool? ephemeralOutOfSync { get; set; } = null!;
        public bool? productHeaderImageRejected { get; set; } = null!;
        public int? lastPlaybackProgress { get; set; } = null!;
        public bool? isDynamicReplyButtonsMsg { get; set; } = null!;
        public bool? isMdHistoryMsg { get; set; } = null!;
        public int? stickerSentTs { get; set; } = null!;
        public bool? isAvatar { get; set; } = null!;
        public bool? requiresDirectConnection { get; set; } = null!;
        public bool? pttForwardedFeaturesEnabled { get; set; } = null!;
        public bool? isEphemeral { get; set; } = null!;
        public bool? isStatusV3 { get; set; } = null!;
        public bool? hasMedia { get; set; } = null!;
        public int? timestamp { get; set; } = null!;
        public string? deviceType { get; set; } = null!;
        public int? forwardingScore { get; set; } = null!;
        public bool? isStatus { get; set; } = null!;
        public bool? isStarred { get; set; } = null!;
        public bool? fromMe { get; set; } = null!;
        public bool? hasQuotedMsg { get; set; } = null!;
        public string? QuotedMsgId { get; set; } = null!;
        public bool? isGif { get; set; } = null!;
        public int? Media_filesize { get; set; } = null!;
    }
}