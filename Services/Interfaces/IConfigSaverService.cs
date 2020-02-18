using System;
using System.IO;
using System.Threading.Tasks;

namespace rss_feed.Services.Interfaces {
    public interface IConfigSaverService {
        public Task SaveConfig(Stream stream);
    }
}