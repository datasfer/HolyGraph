using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace HolyGraph.API;

public class BookParams : PaginationParams
{
     
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public ScriptureEnum Scripture { get; set; } = ScriptureEnum.All;

    public string Translation { get; set; } = string.Empty;
    public string OrderBy { get; set; } = "bookOrder";

    public override string ToString()
    {
        return $"Scripture: {Scripture}, PageNumber: {PageNumber}, PageSize: {PageSize}";
    }
}


public enum ScriptureEnum
{
    All,
    Bible,
    Narek,
}