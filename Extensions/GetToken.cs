namespace EventsModule.API.Extensions
{
    public class GetToken
    {
        // Helper Method for getting API Key from appsettings.json
        public static string Key()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            string value = config["TokenSettings:Value"];
            return value;
        }
    }
}
