using System;

namespace Authentication.Model
{
    public class TokenResultModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
