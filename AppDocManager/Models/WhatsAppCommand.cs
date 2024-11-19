using Newtonsoft.Json;
using System.Collections.Generic;

namespace AppDocManager.Models
{

    public class Component
    {
        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("parameters")]
        public List<Parameter> parameters { get; set; }
    }

    public class Language
    {
        [JsonProperty("code")]
        public string code { get; set; }
    }

    public class Parameter
    {
        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("text")]
        public string text { get; set; }
    }

    public class WhatsAppCommand
    {
        [JsonProperty("messaging_product")]
        public string messaging_product { get; set; }

        [JsonProperty("to")]
        public string to { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("template")]
        public Template template { get; set; }
    }

    public class Template
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("language")]
        public Language language { get; set; }

        [JsonProperty("components")]
        public List<Component> components { get; set; }
    }


}
