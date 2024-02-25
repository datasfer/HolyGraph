using Newtonsoft.Json;

namespace HolyGraph.API;

public class ArmatDto
{
    [JsonProperty("<id>")] 
    public int id {get;set;}

    public string armat {get;set;}

    public int formID {get;set;}

}

