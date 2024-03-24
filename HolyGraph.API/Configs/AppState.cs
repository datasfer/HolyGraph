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

        Books = 
        [
            deserializer.Deserialize<Book>(File.OpenText(@"./Data/YAML/e1994T/New/01.Matthew.yaml")),
            deserializer.Deserialize<Book>(File.OpenText(@"./Data/YAML/e1994T/New/02.Mark.yaml")),
            deserializer.Deserialize<Book>(File.OpenText(@"./Data/YAML/e1994T/New/03.Luke.yaml")),
            deserializer.Deserialize<Book>(File.OpenText(@"./Data/YAML/e1994T/New/04.John.yaml")),
            deserializer.Deserialize<Book>(File.OpenText(@"./Data/YAML/e1994T/New/05.Acts.yaml")),
            deserializer.Deserialize<Book>(File.OpenText(@"./Data/YAML/e1994T/New/06.Romans.yaml")),
            deserializer.Deserialize<Book>(File.OpenText(@"./Data/YAML/e1994T/New/07.1_Corinthians.yaml")),
            deserializer.Deserialize<Book>(File.OpenText(@"./Data/YAML/e1994T/New/08.2_Corinthians.yaml")),
        ];
    }

    public List<Book> Books {get; private set;}
}
