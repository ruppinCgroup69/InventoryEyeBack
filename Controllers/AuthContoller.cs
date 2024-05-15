

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;

namespace inventoryeyeback;


// https//localhost:5001/auth/login

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private IConfiguration configuration;

    private DatabaseContext context;
    public AuthController(IConfiguration configuration, DatabaseContext context)
    {
        this.configuration = configuration;
        this.context = context;
    }



    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
    {
        try
        {
            var ExistingUser = await context.Users.Where(u => u.Email == userRegister.Email).FirstAsync();
            return BadRequest("User with email already exists.");
        }
        catch (Exception)
        {
            var NewUser = new User
            {
                Email = userRegister.Email,
                Password = BC.HashPassword(userRegister.Password),
                BirthDate = userRegister.BirthDate,
                UserType = userRegister.UserType,
                FullName = userRegister.FullName,
                AddressLatitude = userRegister.AddressLatitude,
                AddressLongtitude = userRegister.AddressLongtitude,
            };

            await context.Users.AddAsync(NewUser);
            await context.SaveChangesAsync();

            return Ok(NewUser);
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
    {
        try
        {
            var ExistingUser = await context.Users.Where(u => u.Email == userLogin.Email).FirstAsync();

            if (!BC.Verify(userLogin.Password, ExistingUser.Password))
            {
                return BadRequest("Email or password incorrect.");
            }
            var signingKey = configuration["JwtKey"]!;
            var Token = new Token
            {
                AccessToken = Jwt.GenerateJwtToken(signingKey, ExistingUser.UserId)
            };
            return Ok(Token); // status = 200
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return BadRequest($@"User with email {userLogin.Email} not found"); // status = 400
        }
    }

    [HttpGet("info"), Authorize, ExtractTokenFilter]
    public async Task<IActionResult> GetUserInformation(int userId)
    {

        var User = await context.Users.FindAsync(userId);
        return Ok(User);
    }


}