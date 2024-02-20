#pragma warning disable
namespace HolyGraph.API.Yaml;

public class Book
{

    public string bookName { get; set; }
    public string osisName { get; set; }
    public string testament { get; set; }
    public string language { get; set; }
    public string translation { get; set; }
    public string dialect { get; set; }
    public string title { get; set; }
    public string bookDivision { get; set; }
    public string bookOrder { get; set; }
    public string chapterCount { get; set; }
    public string verseCount { get; set; }
    public string peopleCount { get; set; }
    public string placeCount { get; set; }

    public Dictionary<int, Chapter> chapters { get; set; }
    public string[] abbreviations { get; set; }
    public int[] verses{ get; set; }

}
