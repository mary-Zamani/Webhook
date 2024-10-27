using System.Text.Json.Serialization;

namespace Webhook.ViewModels
{
    public class HookData
    {
        [JsonPropertyName("event")]
        public string EventType { get; set; } = null!;

        [JsonPropertyName("instanceId")]
        public int InstanceId { get; set; }

        [JsonPropertyName("data")]
        public Data? Data { get; set; }
    }
    public class Data
    {
        [JsonPropertyName("message")]
        public messageModel? message { get; set; } = null!;


        [JsonPropertyName("media")]
        public mediaModel? media { get; set; } = null!;
    }
    public class messageModel
    {
        [JsonPropertyName("_data")]
        public _dataModel? _data { get; set; } = null!;
        [JsonPropertyName("body")]
        public string? body { get; set; } = null!;

        [JsonPropertyName("type")]
        public string? type { get; set; } = null!;
        [JsonPropertyName("from")]
        public string? from { get; set; } = null!;

        [JsonPropertyName("to")]
        public string? to { get; set; } = null!;
        [JsonPropertyName("ack")]
        public int? ack { get; set; } = null!;
        [JsonPropertyName("hasMedia")]
        public bool? hasMedia { get; set; } = null!;
        [JsonPropertyName("timestamp")]
        public int? timestamp { get; set; } = null!;
        [JsonPropertyName("deviceType")]
        public string? deviceType { get; set; } = null!;
        [JsonPropertyName("isForwarded")]
        public bool? isForwarded { get; set; } = null!;
        [JsonPropertyName("forwardingScore")]
        public int? forwardingScore { get; set; } = null!;
        [JsonPropertyName("isStatus")]
        public bool? isStatus { get; set; } = null!;
        [JsonPropertyName("isStarred")]
        public bool? isStarred { get; set; } = null!;
        [JsonPropertyName("broadcast")]
        public bool? broadcast { get; set; } = null!;
        [JsonPropertyName("fromMe")]
        public bool? fromMe { get; set; } = null!;
        [JsonPropertyName("hasQuotedMsg")]
        public bool? hasQuotedMsg { get; set; } = null!;
        [JsonPropertyName("vCards")]
        public object[]? vCards { get; set; } = null!;
        [JsonPropertyName("mentionedIds")]
        public object[]? mentionedIds { get; set; } = null!;
        [JsonPropertyName("isGif")]
        public bool? isGif { get; set; } = null!;
        [JsonPropertyName("isEphemeral")]
        public bool? isEphemeral { get; set; } = null!;
        [JsonPropertyName("links")]
        public object[]? links { get; set; } = null!;
        
    }
    public class _dataModel
    {
        [JsonPropertyName("id")]
        public idModel? id { get; set; } = null!;

        [JsonPropertyName("body")]
        public string? body { get; set; } = null!;

        [JsonPropertyName("type")]
        public string? type { get; set; } = null!;

        [JsonPropertyName("t")]
        public int? t { get; set; } = null!;


        [JsonPropertyName("notifyName")]
        public string? notifyName { get; set; } = null!;

        [JsonPropertyName("from")]
        public string? from { get; set; } = null!;

        [JsonPropertyName("to")]
        public string? to { get; set; } = null!;
        [JsonPropertyName("self")]
        public string? self { get; set; } = null!;

        [JsonPropertyName("ack")]
        public int? ack { get; set; } = null!;

        [JsonPropertyName("isNewMsg")]
        public bool? isNewMsg { get; set; } = null!;
        [JsonPropertyName("star")]
        public bool? star { get; set; } = null!;
        [JsonPropertyName("kicNotified")]
        public bool? kicNotified { get; set; } = null!;
        [JsonPropertyName("recvFresh")]
        public bool? recvFresh { get; set; } = null!;
        [JsonPropertyName("isFromTemplate")]
        public bool? isFromTemplate { get; set; } = null!;
        [JsonPropertyName("pollInvalidated")]
        public bool? pollInvalidated { get; set; } = null!;
        [JsonPropertyName("latestEditMsgKey")]
        public object? latestEditMsgKey { get; set; } = null!;

        [JsonPropertyName("latestEditSenderTimestampMs")]
        public object? latestEditSenderTimestampMs { get; set; } = null!;

        [JsonPropertyName("broadcast")]
        public bool? broadcast { get; set; } = null!;

        [JsonPropertyName("mentionedJidList")]
        public object[]? mentionedJidList { get; set; } = null!;
        [JsonPropertyName("isVcardOverMmsDocument")]
        public bool? isVcardOverMmsDocument { get; set; } = null!;
        [JsonPropertyName("isForwarded")]
        public bool? isForwarded { get; set; } = null!;
        [JsonPropertyName("labels")]
        public object[]? labels { get; set; } = null!;
        [JsonPropertyName("hasReaction")]
        public bool? hasReaction { get; set; } = null!;
        [JsonPropertyName("ephemeralOutOfSync")]
        public bool? ephemeralOutOfSync { get; set; } = null!;
        [JsonPropertyName("productHeaderImageRejected")]
        public bool? productHeaderImageRejected { get; set; } = null!;
        [JsonPropertyName("lastPlaybackProgress")]
        public int? lastPlaybackProgress { get; set; } = null!;
        [JsonPropertyName("isDynamicReplyButtonsMsg")]
        public bool? isDynamicReplyButtonsMsg { get; set; } = null!;
        [JsonPropertyName("isMdHistoryMsg")]
        public bool? isMdHistoryMsg { get; set; } = null!;
        [JsonPropertyName("stickerSentTs")]
        public int? stickerSentTs { get; set; } = null!;
        [JsonPropertyName("isAvatar")]
        public bool? isAvatar { get; set; } = null!;
        [JsonPropertyName("requiresDirectConnection")]
        public bool? requiresDirectConnection { get; set; } = null!;
        [JsonPropertyName("pttForwardedFeaturesEnabled")]
        public bool? pttForwardedFeaturesEnabled { get; set; } = null!;
        [JsonPropertyName("isEphemeral")]
        public bool? isEphemeral { get; set; } = null!;
        [JsonPropertyName("isStatusV3")]
        public bool? isStatusV3 { get; set; } = null!;

        [JsonPropertyName("links")]
        public object[]? links { get; set; } = null!;

    }
    public class idModel
    {
        [JsonPropertyName("fromMe")]
        public bool? fromMe { get; set; } = false;

        [JsonPropertyName("remote")]
        public string? remote { get; set; } = null!;

        [JsonPropertyName("id")]
        public string? id { get; set; } = null!;

        [JsonPropertyName("_serialized")]
        public string? _serialized { get; set; } = null!;
    }

    public class mediaModel
    {


        [JsonPropertyName("mimetype")]
        public object? mimetype { get; set; } = null!;

        [JsonPropertyName("data")]
        public object? data { get; set; } = null!;

        [JsonPropertyName("filename")]
        public object? filename { get; set; } = null!;

        [JsonPropertyName("filesize")]
        public int? filesize { get; set; }
    }


}
