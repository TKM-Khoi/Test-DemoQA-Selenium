using Microsoft.Extensions.Configuration;

namespace Core.Utils
{
    public static class ConfigurationUtils
    {
        private static IConfiguration _config;
        public static IConfiguration ReadConfiguration(string path)
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path)
                .Build();
            return _config;
        }
        public static string GetConfigurationByKey(string key, IConfiguration? config = null)
        {
            var value = config == null ? _config[key] : config[key];

            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
            throw new InvalidDataException($"Attribute [{key}] has not been set in appsettings.json");
        }
        public static IEnumerable<string> GetBrowserArgs(string key, IConfiguration? config = null)
        {
            config = config ?? _config;
            foreach (KeyValuePair<string, string?> pair in config.AsEnumerable())
            {
                if (pair.Key.Contains(key) && pair.Value!=null)
                {
                    yield return pair.Value;
                }
            }
        }
        public static IDictionary<string, string> GetBrowserPrefs(string browserName, IConfiguration? config = null)
        {
            config = config ?? _config;
            var prefsSection = config.GetSection($"Browser:{browserName}:BrowserPrefs");
            if (!prefsSection.Exists())
            {
                return new Dictionary<string, string>();
            }
            return prefsSection.GetChildren().ToDictionary(x => x.Key, x => x.Value ?? string.Empty);
        }
    }
}