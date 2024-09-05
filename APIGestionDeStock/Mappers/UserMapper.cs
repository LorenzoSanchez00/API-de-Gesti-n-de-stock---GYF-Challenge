using APIGestionDeStock.DTOs.Request;
using APIGestionDeStock.DTOs.Response;
using APIGestionDeStock.Models;

namespace APIGestionDeStock.Mappers
{
    public static class UserMapper
    {
        public static User FromRequestDtoToEntity(this UserRequestDTO userRequestDTO)
        {
            return new User
            {
                Name = userRequestDTO.Name,
                Email = userRequestDTO.Email,
                Password = userRequestDTO.Password
            };
        }

        public static UserRequestDTO FromEntityToRequestDto(this User user)
        {
            return new UserRequestDTO
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
        }
        public static UserResponseDTO FromEntityToResponseDto(this User user)
        {
            return new UserResponseDTO
            {
                Name = user.Name,
                Email = user.Email
            };
        }

    }
}
