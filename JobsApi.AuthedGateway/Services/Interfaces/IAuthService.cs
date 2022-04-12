using System.Threading.Tasks;
using JobsApi.AuthedGateway.Models;

namespace JobsApi.AuthedGateway.Services.Interfaces
{
    public interface IAuthService
    {
        Task<PlayerGetDto> Signup(Player player);

        Task<PlayerLoginResponseDto> Login(PlayerLoginRequestDto playerLoginRequestDto);
    }
}