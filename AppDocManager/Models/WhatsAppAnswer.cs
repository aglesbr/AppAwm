using Newtonsoft.Json;
using System.Collections.Generic;

namespace AppDocManager.Models
{
    public class Contact
    {
        [JsonProperty("input")]
        public string Input { get; set; }

        [JsonProperty("wa_id")]
        public string WaId { get; set; }
    }

    public class Message
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("message_status")]
        public string MessageStatus { get; set; }
    }

    public class WhatsAppAnswer
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; }

        [JsonProperty("contacts")]
        public List<Contact> Contacts { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }
    }
}
