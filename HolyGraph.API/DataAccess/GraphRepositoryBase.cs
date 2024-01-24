using HolyGraph.API.Interfaces;

namespace HolyGraph.API;

public abstract class GraphRepositoryBase
{
    protected readonly INeo4jGraphRepository _neoGraph;
    public GraphRepositoryBase(INeo4jGraphRepository neoGraph)
    {
        _neoGraph = neoGraph;
    }
}
