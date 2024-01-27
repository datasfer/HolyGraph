#nullable disable warnings
using Newtonsoft.Json;

namespace HolyGraph.API;

public class VerseDto
{
    [JsonProperty("<id>")]
    public int id {get;set;}
    public string translationId {get; set;}
    public string bookTitle {get; set;} 
    public string sectionTitle {get; set;} 
    public string sectionID {get; set;} 
    public string book {get; set;}
    public int chapter {get; set;}
    public int section {get; set;}
    public int verse {get; set;}
    public string verseID {get; set;}
    public int verseOrder  {get; set;}
    public string verseText { get; set; }
}
