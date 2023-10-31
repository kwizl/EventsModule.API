using IdentityServer4.Models;
using IdentityServer4.Test;

namespace EventsModule.IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "eventsModuleClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "eventsModuleAPI" }
                }
            };

        public static IEnumerable<ApiScope> ApiScope =>
            new ApiScope[]
            {
                new ApiScope("eventsModuleAPI", "Events Module API")
            };

        public static IEnumerable<ApiResource> ApiResource =>
            new ApiResource[]
            {

            };

        public static IEnumerable<IdentityResource> IdentityResource =>
            new IdentityResource[]
            {

            };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {

            };
    }
}
