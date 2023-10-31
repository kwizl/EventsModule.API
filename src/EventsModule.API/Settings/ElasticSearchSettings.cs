using System.ComponentModel.DataAnnotations;

namespace EventsModule.API.Settings
{
    public class ElasticSearchSettings
    {
        public const string MinimumVersion = "8.9.0";

        [Required] public string ConnectionString { get; set; } = null!;
    }
}
