using Blazored.LocalStorage;
using ClienteNet6.Client.AuthClient.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ClienteNet6.Client.AuthClient
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider, IAuthToken
    {
        private const string LocalToken = "token";

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
            var savedToken = await _localStorage.GetItemAsync<string>(LocalToken);

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
                 new AuthenticationHeaderValue("Bearer", savedToken);
            
            return new AuthenticationState(new ClaimsPrincipal(
                new ClaimsIdentity(
                    new JwtSecurityTokenHandler().ReadJwtToken(savedToken).Claims, "jwt"))
            );
        }

        public async Task GetToken()
        {
            await _localStorage.GetItemAsync<string>(LocalToken);
        }

        public async Task RemoveToken()
        {
            await _localStorage.RemoveItemAsync(LocalToken);
        }

        public async Task SetToken(string token)
        {
            await _localStorage.SetItemAsync(LocalToken, token);
        }
    }
}
