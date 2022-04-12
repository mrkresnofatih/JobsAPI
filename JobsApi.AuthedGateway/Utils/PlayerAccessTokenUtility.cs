using System;
using System.Collections.Generic;
using JobsApi.AuthedGateway.Templates;

namespace JobsApi.AuthedGateway.Utils
{
    public class PlayerAccessTokenUtility : JwtTemplate
    {
        protected override string GetSecret()
        {
            return "w5eyl3CROxSeOmtHa8zgV9nIk7F7zznBDiWLr3cZ";
        }
        
        public string GenerateToken(string username)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var payload = new Dictionary<string, string>
            {
                { "username", username },
                { "genesisTime", now.ToString() }
            };
            return GetToken(payload);
        }

        public string GetUsernameFromToken(string token)
        {
            var claims = GetPayload(token);
            var username = claims["username"];
            return username;
        }
    }
}