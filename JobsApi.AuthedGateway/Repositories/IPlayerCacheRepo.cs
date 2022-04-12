using System.Threading.Tasks;
using JobsApi.AuthedGateway.Models;

namespace JobsApi.AuthedGateway.Repositories
{
    public interface IPlayerCacheRepo
    {
        Task SaveByUsername(string username, Player player);
        
        Task<Player> GetByUsername(string username);
        
        Task DeleteByUsername(string username);
    }
}