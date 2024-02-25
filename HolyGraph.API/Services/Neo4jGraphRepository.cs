using HolyGraph.API.Configs;
using HolyGraph.API.Extensions;
using HolyGraph.API.Interfaces;
using Neo4j.Driver;
using Newtonsoft.Json;

namespace HolyGraph.API.Services;

 public class Neo4jGraphRepository : INeo4jGraphRepository, IDisposable
{
    private readonly IDriver _driver;
    private readonly Neo4jConfig _config;
    private readonly ILogger<Neo4jGraphRepository> _logger;

    public Neo4jGraphRepository(IDriver driver, Neo4jConfig config, ILogger<Neo4jGraphRepository> logger)
     {
          _driver = driver;
          _config = config;
          _logger = logger;
     }
    
     public async Task<string> RunQueryAsync(string query)
     {
        var jsonResult = await GetResultAsync(query);
        return JsonConvert.SerializeObject(jsonResult);
     }

     public async Task<IEnumerable<T>> RunQueryAsync<T>(string query)
     {
         Console.WriteLine($"Request: {query}");
        var jsonResult = await GetResultAsync(query);
        var jsonify = JsonConvert.SerializeObject(jsonResult);
        return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonify);
     }
   
     private async Task<IEnumerable<IDictionary<string, object>>> GetResultAsync(string query)
     {
            using var dbSession = StartSession();
            var cursor = await dbSession.RunAsync(query);

            var result = new List<IDictionary<string, object>>();

            while (await cursor.FetchAsync())
            {
                var record = cursor.Current;
                var resultDictionary = new Dictionary<string, object>();

                foreach (var (key, value) in record.Values)
                {
                    ProcessValue(resultDictionary, key, value);
                }

                result.Add(resultDictionary);
            }

            return result;
        }

        private static void ProcessValue(Dictionary<string, object> resultDictionary, string key, object value)
        {
            switch (value)
            {
                case INode node:
                    resultDictionary.Add("<elementId>", node.ElementId);
                    resultDictionary.Add("<id>", node.Id);
                    resultDictionary.Add("labels", string.Join(",", node.Labels));
                    resultDictionary.AddRange(node.Properties);
                    break;
                case IPath path:
                    // TODO: Process this case
                    resultDictionary.Add(key, path);
                    break;
                default:
                    resultDictionary.Add(key, value);
                    break;
            }
        }

        private IAsyncSession StartSession()
        {
            return _driver.AsyncSession(options =>
            {
                options.WithDatabase("bibles");
                options.WithDefaultAccessMode(AccessMode.Read);
            });
        }
        
     public void Dispose()
     {
        throw new NotImplementedException();
     }

}    