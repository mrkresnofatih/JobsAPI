using System.Threading.Tasks;
using AutoMapper;
using JobsApi.AuthedGateway.Exceptions;
using JobsApi.AuthedGateway.Models;
using JobsApi.AuthedGateway.Repositories;
using JobsApi.AuthedGateway.Services.Interfaces;
using JobsApi.AuthedGateway.Utils;

namespace JobsApi.AuthedGateway.Services
{
    public class AuthService : IAuthService
    {
        public AuthService(PlayerCacheRepo playerCacheRepo, IMapper mapper,
            PlayerAccessTokenUtility playerAccessTokenUtility, AccessTokenCache accessTokenCache)
        {
            _mapper = mapper;
            _playerCacheRepo = playerCacheRepo;
            _playerAccessTokenUtility = playerAccessTokenUtility;
            _accessTokenCache = accessTokenCache;
        }

        private readonly PlayerCacheRepo _playerCacheRepo;
        private readonly IMapper _mapper;
        private readonly PlayerAccessTokenUtility _playerAccessTokenUtility;
        private readonly AccessTokenCache _accessTokenCache;

        public async Task<PlayerGetDto> Signup(Player player)
        {
            var foundPlayerWithSameUsername = await _playerCacheRepo.GetByUsername(player.Username);
            if (foundPlayerWithSameUsername != null)
            {
                throw new UsernameTakenException();
            }

            await _playerCacheRepo.SaveByUsername(player.Username, player);
            return _mapper.Map<PlayerGetDto>(player);
        }

        public async Task<PlayerLoginResponseDto> Login(PlayerLoginRequestDto playerLoginRequestDto)
        {
            var foundPlayer = await _playerCacheRepo
                .GetByUsername(playerLoginRequestDto.Username);

            if (foundPlayer == null)
            {
                throw new RecordNotFoundException();
            }

            if (playerLoginRequestDto.Password != foundPlayer.Password)
            {
                throw new InvalidCredException();
            }

            var playerGet = _mapper.Map<PlayerGetDto>(foundPlayer);
            var token = _playerAccessTokenUtility.GenerateToken(foundPlayer.Username);

            await _accessTokenCache.SaveByUsername(foundPlayer.Username, token);

            return new PlayerLoginResponseDto
            {
                Player = playerGet,
                Token = token
            };
        }
    }
}