using Blazored.LocalStorage;
using ClienteNet6.Client.AuthClient.Interfaces;
using ClienteNet6.Shared.Dto;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ClienteNet6.Client.AuthClient
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider, IAuthToken
    {
        /// <summary>
        /// Local storage const 
        /// </summary>
        private const string LocalToken = "token";

        /// <summary>
        /// Empty user
        /// </summary>
        private UserInfo EmptyUser => 
            new UserInfo
            {
                Email = "",
                Empresa = "",
                Id = null,
                UserName = ""
            };

        /// <summary>
        /// Not authorized
        /// </summary>
        private AuthenticationState DefaultAuthentication => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        /// <summary>
        /// DI storage managment
        /// </summary>
        private readonly ILocalStorageService _localStorage;

        /// <summary>
        /// DI Client
        /// </summary>
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
                return await Task.FromResult(DefaultAuthentication);
            }

            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(savedToken))
            {
                return await Task.FromResult(DefaultAuthentication);
            }

            var token = handler.ReadJwtToken(savedToken);

            _httpClient.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("bearer", savedToken);

            var response = await _httpClient.GetAsync("/api/login");

            if (response.IsSuccessStatusCode)
            {
                return
                    new AuthenticationState(new ClaimsPrincipal(
                        new ClaimsIdentity(
                            token.Claims, "jwt")
                    )
                );
            }

            // Unauthorized
            _httpClient.DefaultRequestHeaders.Authorization = null;
            await RemoveToken();

            return DefaultAuthentication;
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

        public UserInfo GetUser()
        {
            string? token = _httpClient.DefaultRequestHeaders.Authorization is null ? null : _httpClient.DefaultRequestHeaders.Authorization.Parameter;

            if (string.IsNullOrEmpty(token))
                return EmptyUser;

            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
            {
                return EmptyUser;
            }

            var claims = tokenHandler.ReadJwtToken(token).Claims;

            var nome = claims.FirstOrDefault(predicate => predicate.Type == JwtRegisteredClaimNames.Name);
            var email = claims.FirstOrDefault(predicate => predicate.Type == JwtRegisteredClaimNames.Email);
            var empresa = claims.FirstOrDefault(predicate => predicate.Type == JwtRegisteredClaimNames.FamilyName);
            var identificador = claims.FirstOrDefault(predicate => predicate.Type == JwtRegisteredClaimNames.NameId);

            return new UserInfo
            {
                Email = email is null ? "" : email.Value,
                Empresa = empresa is null ? "" : empresa.Value,
                Id = identificador is null ? 0 : int.Parse(identificador.Value),
                UserName = nome is null ? "" : nome.Value
            };

        }
    }
}
