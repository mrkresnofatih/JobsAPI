using shortid;
using shortid.Configuration;

namespace JobsApi.JobsCore.Utils
{
    public static class ShortIdGenerator
    {
        public static string GenerateId()
        {
            var options = new GenerationOptions()
            {
                UseSpecialCharacters = false,
                UseNumbers = true,
                Length = 16
            };
            return ShortId.Generate(options);
        }
    }
}