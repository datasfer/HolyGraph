using HolyGraph.API.Yaml;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HolyGraph.API;

public class AppState
{
    public AppState()
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(new CamelCaseNamingConvention())
            .Build();

        var book = deserializer.Deserialize<Book>(File.OpenText(@".\YAML\e1994T\new\01.Matthew.yaml"));
    }
}
