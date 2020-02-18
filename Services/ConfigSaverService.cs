using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using rss_feed.Models;
using rss_feed.Services.Interfaces;

namespace rss_feed.Services {
    public class ConfigSaverService : IConfigSaverService {
        public async Task SaveConfig(Stream stream) {
            using var bodyStream = new StreamReader(stream);
            var body = await bodyStream.ReadToEndAsync();

            var config = JsonSerializer.Deserialize<Config>(body);

            var serializer = new XmlSerializer(typeof(Config));
            TextWriter writer = new StreamWriter("Properties/config.xml");
            serializer.Serialize(writer, config);
        }
    }
}