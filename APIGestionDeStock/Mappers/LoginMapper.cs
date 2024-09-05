using APIGestionDeStock.DTOs.Request;
using APIGestionDeStock.DTOs.Response;
using APIGestionDeStock.Models;

namespace APIGestionDeStock.Mappers
{
    public static class LoginMapper
    {
        public static User FromRequestDtoToEntity(this LoginRequestDTO loginRequestDTO)
        {
            return new User
            {
                Email = loginRequestDTO.Email,
                Password = loginRequestDTO.Password
            };
        }

        public static LoginRequestDTO FromEntityToResponseDto(this User user)
        {
            return new LoginRequestDTO
            {
                Email = user.Email,
                Password = user.Password
            };
        }
        public static LoginResponseDTO FromEntityToResponseDto(this User user, string token)
        {
            return new LoginResponseDTO
            {
                Token = token,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
