using Newtonsoft.Json;

namespace HolyGraph.API;

public class TokenDto
{
    [JsonProperty("<id>")] 
    public int id {get;set;}
    public int armatID {get;set;}
    public string armat {get;set;}
    public string tags {get;set;}
    public string token {get;set;}
}
