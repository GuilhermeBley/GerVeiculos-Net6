using ClienteNet6.Shared.Dto;

namespace ClienteNet6.Client.AuthClient.Interfaces
{
    public interface IAuthToken
    {
        Task SetToken(string Token);
        Task RemoveToken();
        Task GetToken();
        UserInfo GetUser();
    }
}
