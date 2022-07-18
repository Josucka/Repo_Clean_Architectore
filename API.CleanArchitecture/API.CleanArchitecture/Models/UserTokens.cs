using System;

namespace Clean.Architecture.Api.Models
{
    public class UserTokens
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
