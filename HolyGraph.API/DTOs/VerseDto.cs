namespace HolyGraph.API;

public class VerseDto
{
    public string id {get; set;}
    public string translationId {get; set;}
    public string bookTitle {get; set;} 
    public string book {get; set;}
    public int chapter {get; set;}
    public int section {get; set;}
    public int verse {get; set;}
    public string verseID {get; set;}
    public int verseOrder  {get; set;}
    public string verseText { get; set; }
}
