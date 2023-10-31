using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

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
                },
                new Client
                {
                    ClientId = "events_mvc_client",
                    ClientName = "Events MVC Web App",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:7165/signin-oidc" // client app port
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:7165/signin-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("eventsModuleAPI", "Events Module API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {

            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "58E86359-073C-434B-AD2D-A3932222DABE",
                    Username = "martin",
                    Password = "martin",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "Martin"),
                        new Claim(JwtClaimTypes.GivenName, "Njoroge")
                    }
                }
            };
    }
}
