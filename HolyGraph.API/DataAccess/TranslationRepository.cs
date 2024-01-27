
using HolyGraph.API.Interfaces;

namespace HolyGraph.API;

public class TranslationRepository : GraphRepositoryBase, ITranslationRepository
{

    public TranslationRepository(INeo4jGraphRepository neoGraph) : base(neoGraph) {}

    public async Task<IEnumerable<TranslationDto>> GetTranslationsAsync()
    {
          var query =
            @"
            MATCH (n:Translation) RETURN n;
            ";
        return await _neoGraph.RunQueryAsync<TranslationDto>(query);
    }

    public async Task<IEnumerable<BookDto>> GetTranslationBooksAsync(string translation)
    {
          var query =
            $@"
            MATCH(t:Translation)
            MATCH(t)-[r:CONTAINS]->(b:Book)
            where t.id='{translation}'
            RETURN b,t.id as translationId;
            ";

        return await _neoGraph.RunQueryAsync<BookDto>(query);
    }

}
