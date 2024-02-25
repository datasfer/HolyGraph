
using HolyGraph.API.Interfaces;
using HolyGraph.API.Models;

namespace HolyGraph.API;

public class VerseRepository : GraphRepositoryBase, IVerseRepository
{
    public VerseRepository(INeo4jGraphRepository neoGraph, AppState appState) : base(neoGraph, appState) {}
    
    public async Task<VerseDto> GetVerseAsync(VerseParams verseParams)
    {
       throw new NotImplementedException();
    }


    public async Task<IEnumerable<VerseDto>> GetChapterVersesAsync(string translation, string book, int chapter)
    {
        var query = 
            $@"
            MATCH(t:Translation) where t.id = '{translation}' 
            MATCH(t)-[rt:CONTAINS]->(n:Book) where n.book = '{book}'
            MATCH (n)<-[r:CONTAINS]->(ch:Chapter)-[:CONTAINS]->(v:Verse) 
            where ch.num = {chapter}
            return v, t.id as translation, n.title as bookTitle order by v.verseOrder
            ";

        var verses = await _neoGraph.RunQueryAsync<VerseDto>(query);
        return verses;
    }

    public async Task<PagedList<VerseDto>> GetVerseRangeAsync(VerseParams verseParams)
    {
        
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Repository Method: GetVerseRangeAsync");
        var book = _appState
                        .Books
                        .Where(b => b.bookName.Equals(verseParams.Book, StringComparison.OrdinalIgnoreCase))
                        .SingleOrDefault();

        var chapter = book?.chapters[verseParams.Chapter];

        int verseTo = verseParams.VerseTo > 0 ? verseParams.VerseTo : verseParams.Verse;;
        if(chapter is not null)
        {
            verseTo = Math.Min(verseTo, chapter.verseCount);
        }


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

        var verses = await _neoGraph.RunQueryAsync<VerseDto>(query);

        return new PagedList<VerseDto>(verses, chapter?.verseCount ?? 222, verseParams.PageNumber, verseParams.PageSize);
    }

    public async Task<PagedList<VerseDto>> GetVersesAsync(VerseParams verseParams)
    {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Repository Method: GetVersesAsync");

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
        WHERE ch.num = {verseParams.Chapter} 
        RETURN v, t.id as translationId, n.title as bookTitle order by v.verse, v.verseOrder
        {skipQuery}
        {limitQuery};
        ";

        var verses = await _neoGraph.RunQueryAsync<VerseDto>(query);


        var book = _appState
                        .Books
                        .Where(b => b.bookName.Equals(verseParams.Book, StringComparison.OrdinalIgnoreCase))
                        .SingleOrDefault();

        var chapter = book?.chapters[verseParams.Chapter];

        return new PagedList<VerseDto>(verses, chapter?.verseCount ?? 222, verseParams.PageNumber, verseParams.PageSize);
    }

    public async Task<PagedList<VerseDto>> GetSectionedVersesAsync(VerseParams verseParams)
    {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Repository Method: GetSectionedVersesAsync");

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
        MATCH (n)<-[r:CONTAINS*2..2]->(s:Section)-[:CONTAINS]->(v:Verse) 
        WHERE s.chapter = {verseParams.Chapter} 
        RETURN v, t.id as translationId, n.title as bookTitle, s.sectionID as sectionID, s.title as sectionTitle order by v.section, v.verseOrder
        {skipQuery}
        {limitQuery};
        ";

        var verses = await _neoGraph.RunQueryAsync<VerseDto>(query);

        var book = _appState
                        .Books
                        .Where(b => b.bookName.Equals(verseParams.Book, StringComparison.OrdinalIgnoreCase))
                        .SingleOrDefault();

        Console.WriteLine(book);
        
        // this doesnot work, because in yaml missing this sections, this is huge work
        //var chapter = book?.chapters[verseParams.Chapter];
        
        var chapterCount = book?.chapterCount ?? 33;
        var verseCount = book?.verses[verseParams.Chapter] ?? 144; // chapter?.verseCount ?? 164

        Console.WriteLine(chapterCount);

        return new PagedList<VerseDto>(verses, verseCount, verseParams.PageNumber, verseParams.PageSize);
    }
}
