using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HolyGraph.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using Newtonsoft.Json;

namespace HolyGraph.API.Controllers
{
    public class VerseController : BaseApiController
    {
        private readonly IDriver _driver;

        public VerseController()
        {
            //_driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "neo"));
            _driver = GraphDatabase.Driver("bolt://195.201.22.161:7687", AuthTokens.Basic("neo4j", "neo"));
        }
    }
}