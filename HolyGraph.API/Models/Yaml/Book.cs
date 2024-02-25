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
    public int bookOrder { get; set; }
    
    // total count of chapters in a book
    public int chapterCount { get; set; }
    // total count of verses in a chapter
    public int verseCount { get; set; }

    public string peopleCount { get; set; }
    public string placeCount { get; set; }

    public Dictionary<int, Chapter> chapters { get; set; }
    public string[] abbreviations { get; set; }

    // verse count in each chapter
    // array index is chapter number, 
    // 0 index set to 0 count    
    public int[] verses{ get; set; }
    
}
