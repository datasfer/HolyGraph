
using HolyGraph.API.Interfaces;

namespace HolyGraph.API;

public class ArmatRepository : IArmatRepository
{
    private readonly INeo4jGraphRepository _neoGraph;
    public ArmatRepository(INeo4jGraphRepository neoGraph)
    {
        _neoGraph = neoGraph;
    }   
    public async Task<PagedList<ArmatDto>> GetArmatsAsync(PaginationParams armatParams)
    {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Request: Armat -> GetArmatsAsync");
        
        var query =
            $@"
            MATCH(a:Armat)
            RETURN a 
            <skip> 
            <limit>;
            ";

        var skip = (armatParams.PageNumber - 1) * armatParams.PageSize;

        query = query.Replace("<skip>",  $"SKIP  {skip}");
        query = query.Replace("<limit>", $"LIMIT {armatParams.PageSize}");

        var armats = await _neoGraph.RunQueryAsync<ArmatDto>(query);
        var pagedList = new PagedList<ArmatDto>(armats, 11713, armatParams.PageNumber, armatParams.PageSize);

        return pagedList;
    }

    public async Task<IEnumerable<ArmatDto>> GetArmatsByStartingLetterAsync(string letter, bool ignoreCase = false)
    {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Request: Armat -> GetArmatsByStartingLetterAsync");
        


        string where = $"where a.armat STARTS WITH '{letter}'";
        if (ignoreCase)
        {
            var lowerL = letter.ToLower();
            var upperL = letter.ToUpper();
            string toggleCase = lowerL;
            if(lowerL == letter)
            {
                toggleCase =  upperL;                
            }

            where += $" OR a.armat STARTS WITH '{toggleCase}'";
        }

        var query =
            $@"
            MATCH(a:Armat)
            {where}
            RETURN a  
            order by a.armat
            ";

        return await _neoGraph.RunQueryAsync<ArmatDto>(query);
    }
}
