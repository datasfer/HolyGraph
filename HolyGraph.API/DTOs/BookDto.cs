using Newtonsoft.Json;

namespace HolyGraph.API;

public class BookDto
{
    [JsonProperty("<elementId>")]
    public string elementId {get;set;}
    
    [JsonProperty("<id>")]
    public string id {get;set;}

    [JsonProperty("id")]
    public string bookId {get;set;}
    public string translationId {get;set;}
    public string labels {get;set;}
    public string book {get;set;}
    public string bookOrder {get;set;}
    public string osisref {get;set;}
    public string shortName {get;set;}
    public string title {get;set;}
    public string testament {get;set;}
    public string slug {get;set;}
}
