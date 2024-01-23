namespace HolyGraph.API;

public class BookParams : PaginationParams
{
    public ScriptureEnum Scripture { get; set; } = ScriptureEnum.All;
    public string OrderBy { get; set; } = "bookOrder";
}


public enum ScriptureEnum
{
    All,
    Bible,
    Narek,
}