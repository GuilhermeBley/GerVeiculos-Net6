namespace ClienteNet6.Shared.Dto
{
    public class UserTokenJwt
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
