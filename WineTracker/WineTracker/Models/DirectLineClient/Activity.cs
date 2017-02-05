using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace WineTracker.Models.DirectLineClient
{
    //
    // Summary:
    //     Object of schema.org types
    public class Entity
    {

        [JsonExtensionData(ReadData = true, WriteData = true)]
        public JObject Properties { get; set; }
        //
        // Summary:
        //     Entity Type (typically from schema.org types)
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

    }

    //
    // Summary:
    //     Channel account information for a conversation
    public class ConversationAccount
    {
        //
        // Summary:
        //     Channel id for the user or bot on this channel (Example: joe@smith.com, or @joesmith
        //     or 123456)
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        //
        // Summary:
        //     Is this a reference to a group
        [JsonProperty(PropertyName = "isGroup")]
        public bool? IsGroup { get; set; }
        //
        // Summary:
        //     Display friendly name
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    //
    // Summary:
    //     An attachment within an activity
    public class Attachment
    {
        //
        // Summary:
        //     Embedded content
        [JsonProperty(PropertyName = "content")]
        public object Content { get; set; }
        //
        // Summary:
        //     mimetype/Contenttype for the file
        [JsonProperty(PropertyName = "contentType")]
        public string ContentType { get; set; }
        //
        // Summary:
        //     Content Url
        [JsonProperty(PropertyName = "contentUrl")]
        public string ContentUrl { get; set; }
        //
        // Summary:
        //     (OPTIONAL) The name of the attachment
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        //
        // Summary:
        //     (OPTIONAL) Thumbnail associated with attachment
        [JsonProperty(PropertyName = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }
    }

    public class Conversation
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    //
    // Summary:
    //     Channel account information needed to route a message
    public class ChannelAccount
    {
        //
        // Summary:
        //     Channel id for the user or bot on this channel (Example: joe@smith.com, or @joesmith
        //     or 123456)
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        //
        // Summary:
        //     Display friendly name
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    public class Activity
    {
 
        //
        // Summary:
        //     ContactAdded/Removed action
        [JsonProperty(PropertyName = "action")]
        public string Action { get; set; }
        //
        // Summary:
        //     AttachmentLayout - hint for how to deal with multiple attachments Values: [list|carousel]
        //     Default:list
        [JsonProperty(PropertyName = "attachmentLayout")]
        public string AttachmentLayout { get; set; }
        //
        // Summary:
        //     Attachments
        [JsonProperty(PropertyName = "attachments")]
        public IList<Attachment> Attachments { get; set; }
        //
        // Summary:
        //     Channel specific payload
        [JsonProperty(PropertyName = "channelData")]
        public object ChannelData { get; set; }
        //
        // Summary:
        //     ChannelId the activity was on
        [JsonProperty(PropertyName = "channelId")]
        public string ChannelId { get; set; }
        //
        // Summary:
        //     Conversation
        [JsonProperty(PropertyName = "conversation")]
        public ConversationAccount Conversation { get; set; }
        //
        // Summary:
        //     Collection of Entity objects, each of which contains metadata about this activity.
        //     Each Entity object is typed.
        [JsonProperty(PropertyName = "entities")]
        public IList<Entity> Entities { get; set; }
        //
        // Summary:
        //     Sender address
        [JsonProperty(PropertyName = "from")]
        public ChannelAccount From { get; set; }
        //
        // Summary:
        //     the previous history of the channel was disclosed
        [JsonProperty(PropertyName = "historyDisclosed")]
        public bool? HistoryDisclosed { get; set; }
        //
        // Summary:
        //     Id for the activity
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        //
        // Summary:
        //     The language code of the Text field
        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }
        //
        // Summary:
        //     Local time when message was sent (set by client Ex: 2016-09-23T13:07:49.4714686-07:00)
        [JsonProperty(PropertyName = "localTimestamp")]
        public DateTimeOffset? LocalTimestamp { get; set; }
        //
        // Summary:
        //     Array of address added
        [JsonProperty(PropertyName = "membersAdded")]
        public IList<ChannelAccount> MembersAdded { get; set; }
        //
        // Summary:
        //     Array of addresses removed
        [JsonProperty(PropertyName = "membersRemoved")]
        public IList<ChannelAccount> MembersRemoved { get; set; }
        [JsonExtensionData(ReadData = true, WriteData = true)]
        public JObject Properties { get; set; }
        //
        // Summary:
        //     (Outbound to bot only) Bot's address that received the message
        [JsonProperty(PropertyName = "recipient")]
        public ChannelAccount Recipient { get; set; }
        //
        // Summary:
        //     the original id this message is a response to
        [JsonProperty(PropertyName = "replyToId")]
        public string ReplyToId { get; set; }
        //
        // Summary:
        //     Service endpoint
        [JsonProperty(PropertyName = "serviceUrl")]
        public string ServiceUrl { get; set; }
        //
        // Summary:
        //     Text to display if you can't render cards
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }
        //
        // Summary:
        //     Content for the message
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        //
        // Summary:
        //     Format of text fields [plain|markdown] Default:markdown
        [JsonProperty(PropertyName = "textFormat")]
        public string TextFormat { get; set; }
        //
        // Summary:
        //     UTC Time when message was sent (Set by service)
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime? Timestamp { get; set; }
        //
        // Summary:
        //     Conversations new topic name
        [JsonProperty(PropertyName = "topicName")]
        public string TopicName { get; set; }
        //
        // Summary:
        //     The type of the activity [message|contactRelationUpdate|converationUpdate|typing]
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        //
        // Summary:
        //     Open ended value
        [JsonProperty(PropertyName = "value")]
        public object Value { get; set; }

    }
}