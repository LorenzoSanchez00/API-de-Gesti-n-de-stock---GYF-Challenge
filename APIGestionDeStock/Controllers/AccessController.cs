using APIGestionDeStock.DTOs.Request;
using APIGestionDeStock.Mappers;
using APIGestionDeStock.Repository.Interfaces;
using FluentValidation;
using FluentValidation.Validators;
using APIGestionDeStock.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace APIGestionDeStock.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserRequestDTO> _signUpValidator;
        private readonly ITokenService _tokenService;

        public AccessController(IUserRepository userRepository,
                               IValidator<UserRequestDTO> signUpValidator,
                               ITokenService tokenService)
        {
            _userRepository = userRepository;
            _signUpValidator = signUpValidator;
            _tokenService = tokenService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> signUp([FromBody] UserRequestDTO userRequestDTO)
        {
            try
            {
                var validationResult = await _signUpValidator.ValidateAsync(userRequestDTO);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = userRequestDTO.FromRequestDtoToEntity();

                var userEmailDb = await _userRepository.GetByEmail(user.Email);
                if (userEmailDb is not null) return BadRequest($"The email: ({userEmailDb}) has already been registered");


                string passwordHash = BCrypt.Net.BCrypt.HashPassword(userRequestDTO.Password);
                user.Password = passwordHash;
                await _userRepository.Add(user);

                var token =  _tokenService.CreateToken(user);


                return Ok(user.FromEntityToResponseDto(token));
            }
            catch (Exception)
            {

                return BadRequest(new Exception());
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {

                var userDb = await _userRepository.GetByEmail(loginRequestDTO.Email);

                if (userDb is null) return NotFound("User not found");
                if (!BCrypt.Net.BCrypt.Verify(loginRequestDTO.Password, userDb.Password)) return BadRequest("Wrong password");

                await _userRepository.Login(userDb);

                var token = _tokenService.CreateToken(userDb);

                return Ok(userDb.FromEntityToResponseDto(token));
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }
    }
}
