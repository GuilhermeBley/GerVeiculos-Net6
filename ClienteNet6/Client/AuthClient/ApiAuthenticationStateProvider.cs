using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ClienteNet6.Client.AuthClient
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private const string StorageName = "token";

        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        public ApiAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Parse Token from local storage
        /// </summary>
        /// <returns>authentication state</returns>
        /// <inheritdoc cref="JwtSecurityTokenHandler.ReadJwtToken(string)" path="/exception"/>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return 
                    await Task.FromResult(
                        new AuthenticationState(new 
                            ClaimsPrincipal(new ClaimsIdentity())
                        )
                    );
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("bearer", savedToken);

            return new AuthenticationState(new ClaimsPrincipal(
                new ClaimsIdentity(
                    new JwtSecurityTokenHandler().ReadJwtToken(savedToken).Claims, "jwt"))
            );
        }
    }
}
