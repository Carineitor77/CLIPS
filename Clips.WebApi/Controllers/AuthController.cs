using AutoMapper;
using Clips.Bll.Dtos;
using Clips.Bll.JWT;
using Clips.Dal.Repository;
using Clips.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Clips.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("PublicPolicy")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _repository;
        private readonly ITokenManager _tokenManager;

        public AuthController(IMapper mapper, IRepository<User> repository, ITokenManager tokenManager)
        {
            _mapper = mapper;
            _repository = repository;
            _tokenManager = tokenManager;
        }

        [HttpPost("RegisterUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(RegisterUserDto registerUserDto)
        {
            if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                return BadRequest("Passwords don't match");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password);
            registerUserDto.Password = passwordHash;

            var result = await _repository.AddAsync(_mapper.Map<User>(registerUserDto));

            if (result != null)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("LoginUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthResponce>> Login(LoginUserDto loginUserDto)
        {
            var authUser = await _repository
                .Query()
                .Include(user => user.Role)
                .FirstOrDefaultAsync(u => u.Email == loginUserDto.Email);

            if (authUser == null)
            {
                return NotFound();
            }

            if (!BCrypt.Net.BCrypt.Verify(loginUserDto.Password, authUser.Password))
            {
                return BadRequest("Incorrect Password! Please check your password");
            }

            var authResponce = new AuthResponce()
            {
                IsAuthenticated = true,
                Role = authUser.Role!.Name,
                Token = _tokenManager.GenerateToken(authUser)
            };

            return Ok(authResponce);
        }
    }
}
