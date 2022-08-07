using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TravelApp.API.Model;
using TravelApp.Domain.Entities;
using TravelApp.Domain.Model;
using TravelApp.Domain.Repositories;
using TravelApp.Infrastructure.Concrete.Interfaces;

namespace TravelApp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHashing _passwordHashing;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserRepository _userRepository;

        public AccountController(IMapper mapper, IPasswordHashing passwordHashing, ITokenHandler tokenHandler, IUserRepository userRepository)
        {
            _mapper = mapper;
            _passwordHashing = passwordHashing;
            _tokenHandler = tokenHandler;
            _userRepository = userRepository;
        }

        [ProducesResponseType(typeof(AccessToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(AccessToken), StatusCodes.Status400BadRequest)]
        [HttpPost("login")]
        public ActionResult Login(TokenRequestModel requestData)
        {
            AccessToken token = null;
            Result<User> existUser = _userRepository.GetUserByUsername(requestData.username);
            if (!existUser.IsSuccess)
                return NotFound(existUser.ErrorMessage);
            if (existUser.Data == null)
                return Unauthorized("invalid username.");


            if (!_passwordHashing.VerifyPassword(requestData.password, existUser.Data.Password))
                return Unauthorized("invalid user password.");

            token = _tokenHandler.CreateToken(requestData, new List<OperationClaim>() { new OperationClaim() { Name = "user" } });
            if (token == null)
                return BadRequest((AccessToken)null);

            return Ok(token);
        }
        [ProducesResponseType(typeof(Result<UserDto>), StatusCodes.Status200OK)]
        [HttpPost("register")]
        public IActionResult Register(UserDto user)
        {
            Result<UserDto> result = new Result<UserDto>();
            #region check User Is Exists
            Result existUser = _userRepository.CheckUserByExist(user.UserName);
            if (!existUser.IsSuccess)
                return BadRequest(existUser.ErrorMessage);
            if (existUser.AlreadyExist)
            {
                result.SetFailure("Username  is already exists.");
                return Ok(result);
            }
            #endregion

            User userEntity = _mapper.Map<UserDto, User>(user);
            userEntity.Password = _passwordHashing.HashPassword(user.Password);
            _userRepository.Add(userEntity);
            result.ObjectId = userEntity.Id.ToString();
            result.SetData(_mapper.Map<User, UserDto>(userEntity));
            return Ok(result);

        }
    }
}
