using HolyGraph.API.Interfaces;

namespace HolyGraph.API;

public abstract class GraphRepositoryBase
{
    protected readonly INeo4jGraphRepository _neoGraph;
    protected readonly AppState _appState;

    public GraphRepositoryBase(INeo4jGraphRepository neoGraph, AppState appState)
    {
        _neoGraph = neoGraph;
        _appState = appState;
    }


}
