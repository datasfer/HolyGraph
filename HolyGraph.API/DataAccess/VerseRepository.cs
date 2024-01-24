
using HolyGraph.API.Interfaces;

namespace HolyGraph.API;

public class VerseRepository : GraphRepositoryBase, IVerseRepository
{
    public VerseRepository(INeo4jGraphRepository neoGraph) : base(neoGraph) {}
    
    public async Task<VerseDto> GetVerseAsync(VerseParams verseParams)
    {
       throw new NotImplementedException();
    }

    public async Task<PagedList<VerseDto>> GetVerseRangeAsync(VerseParams verseParams)
    {
        int verseTo = verseParams.VerseTo > 0 ? verseParams.VerseTo : verseParams.Verse;
        var skip = (verseParams.PageNumber - 1) * verseParams.PageSize;

        var skipQuery = string.Empty;
        if(skip > 0)
        {
            skipQuery = $"SKIP  {skip}";
        }
        var limitQuery = string.Empty;
        if(verseParams.PageSize > 0)
        {
            limitQuery = $"LIMIT {verseParams.PageSize}";
        }

         var query =
        $@"
        MATCH(t:Translation) where t.id = '{verseParams.Translation}' 
        MATCH(t)-[rt:CONTAINS]->(n:Book) WHERE n.book = '{verseParams.Book}'
        MATCH (n)<-[r:CONTAINS]->(ch:Chapter)-[:CONTAINS]->(v:Verse) 
        WHERE ch.num = {verseParams.Chapter} and v.verse >= {verseParams.Verse} and v.verse <= {verseTo}
        RETURN v, t.id as translationId, n.title as bookTitle order by v.verseOrder
        {skipQuery}
        {limitQuery};
        ";

         Console.WriteLine($"{query}");
        var verses = await _neoGraph.RunQueryAsync<VerseDto>(query);

        return new PagedList<VerseDto>(verses, 164, verseParams.PageNumber, verseParams.PageSize);
    }
}
