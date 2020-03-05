using System.Collections.Generic;

namespace ptPKT.Infrastructure.localization
{
    // Neend to test
    // https://github.com/AlexTeixeira/Askmethat-Aspnet-JsonLocalizer
    // https://stackoverflow.com/questions/43615912/asp-net-core-localization-with-json-files
    public class JsonLocalization
    {
        public string Key { get; set; }
        public Dictionary<string, string> LocalizedValue = new Dictionary<string, string>();
    }
}
