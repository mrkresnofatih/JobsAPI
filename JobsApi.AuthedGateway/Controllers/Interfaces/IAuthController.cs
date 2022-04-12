using System.Threading.Tasks;
using JobsApi.AuthedGateway.Models;
using JobsApi.AuthedGateway.Utils;

namespace JobsApi.AuthedGateway.Controllers.Interfaces
{
    public interface IAuthController
    {
        Task<ResponsePayload<PlayerGetDto>> Signup(Player player);

        Task<ResponsePayload<PlayerLoginResponseDto>> Login(PlayerLoginRequestDto playerLoginRequestDto);
    }
}