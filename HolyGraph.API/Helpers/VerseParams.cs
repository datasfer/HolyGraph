namespace HolyGraph.API;

public class VerseParams : PaginationParams
{
    public string Translation { get; set; } = string.Empty;
    public string Book { get; set; } = string.Empty;

    public int Chapter { get; set; } 
    public int Section { get; set; } 

    public bool withSections { get; set; }  = false;

    public bool shouldPaginate { get; set; }  = false;
    
    /// <summary>
    /// verse from number
    /// </summary>
    public int Verse { get; set; } 

    public int VerseTo { get; set; } 

    public string OrderBy { get; set; } = "bookOrder";

    public override string ToString()
    {
        return $"Translation: {Translation}, PageNumber: {PageNumber}, PageSize: {PageSize}, withSections: {withSections}, shouldPaginate: {shouldPaginate}";
    }
}
