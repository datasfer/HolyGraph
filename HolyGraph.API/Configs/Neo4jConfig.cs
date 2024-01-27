#nullable disable warnings

namespace HolyGraph.API.Configs
{
    public class Neo4jConfig
    {
        public string InstanceUri { get; set; }
        public string DatabaseName { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
