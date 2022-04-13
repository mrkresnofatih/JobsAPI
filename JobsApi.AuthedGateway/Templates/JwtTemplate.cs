using System;
using System.Collections.Generic;
using JobsApi.AuthedGateway.Exceptions;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;

namespace JobsApi.AuthedGateway.Templates
{
    public abstract class JwtTemplate
    {
        protected abstract string GetSecret();
        
        private static IJwtAlgorithm GetAlgorithm()
        {
            return new HMACSHA256Algorithm();
        }

        private static IJsonSerializer GetJsonSerializer()
        {
            return new JsonNetSerializer();
        }

        private static IBase64UrlEncoder GetBase64UrlEncoder()
        {
            return new JwtBase64UrlEncoder();
        }

        private IJwtEncoder GetJwtEncoder()
        {
            return new JwtEncoder(GetAlgorithm(), GetJsonSerializer(), GetBase64UrlEncoder());
        }

        protected string GetToken(Dictionary<string, string> payload)
        {
            var encoder = GetJwtEncoder();
            return encoder.Encode(payload, GetSecret());
        }

        protected Dictionary<string, string> GetPayload(string token)
        {
            return JwtBuilder
                .Create()
                .WithAlgorithm(GetAlgorithm())
                .WithSecret(GetSecret())
                .MustVerifySignature()
                .Decode<Dictionary<string, string>>(token);
        }

        public bool ValidateToken(string token)
        {
            try
            {
                var decoded = GetPayload(token);
                return decoded != null;
            }
            catch (Exception)
            {
                throw new InvalidCredException();
            }
        }
    }
}