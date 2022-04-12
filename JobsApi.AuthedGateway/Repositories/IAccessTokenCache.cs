using System.Threading.Tasks;

namespace JobsApi.AuthedGateway.Repositories
{
    public interface IAccessTokenCache
    {
        Task SaveByUsername(string username, string token);
        
        Task<string> GetByUsername(string username);
        
        Task DeleteByUsername(string username);
    }
}