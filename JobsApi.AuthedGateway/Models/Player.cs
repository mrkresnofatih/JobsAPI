using System.ComponentModel.DataAnnotations;

namespace JobsApi.AuthedGateway.Models
{
    public class Player
    {
        [StringLength(20)]
        [Required]
        public string Username { get; set; }
        
        [StringLength(30)]
        [Required]
        public string Fullname { get; set; }
        
        [Required]
        public string Password { get; set; }
    }

    public class PlayerGetDto
    {
        public string Username { get; set; }
        
        public string Fullname { get; set; }
    }

    public class PlayerLoginRequestDto
    {
        [StringLength(20)]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class PlayerLoginResponseDto
    {
        public PlayerGetDto Player { get; set; }
        
        public string Token { get; set; }
    }
}