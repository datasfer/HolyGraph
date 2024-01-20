using Microsoft.AspNetCore.Components;
using Neo4j.Driver;

namespace HolyGraph.API.Controllers
{
    [Route("api/scriptures")]
    public class EntityController : BaseApiController
    {
        private readonly IDriver _driver;

        public EntityController()
        {
            //_driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "neo"));
            _driver = GraphDatabase.Driver("bolt://195.201.22.161:7687", AuthTokens.Basic("neo4j", "neo"));
        }
    }

    
}