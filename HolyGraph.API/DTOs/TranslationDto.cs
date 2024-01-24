using Newtonsoft.Json;

namespace HolyGraph.API;

public class TranslationDto
{
    [JsonProperty("<elementId>")]
    public string elementId { get; set; }
    [JsonProperty("<id>")]
    public string id { get; set; }
    [JsonProperty("id")]
    public string translationId { get; set; }
    public string labels { get; set; }
    public string dialect { get; set; }
    public string translators { get; set; }
    public string year { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string language { get; set; }
    public string from { get; set; }
    public string lang { get; set; }
    public string version { get; set;  }
}
