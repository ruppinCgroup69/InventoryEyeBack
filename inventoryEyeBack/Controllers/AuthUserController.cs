
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;
using inventoryeyeback.Dto;


namespace inventoryeyeback;

[Route("api/[controller]")]
[ApiController]
public class AuthUserController : ControllerBase
{

    //לשאול אם למחוק 
    private IConfiguration configuration;

    private DatabaseContext context;
    public AuthUserController(IConfiguration configuration, DatabaseContext context)
    {
        this.configuration = configuration;
        this.context = context;
    }


    
    [HttpGet("personalInfo")]
    public IActionResult GetUserInformation(int userId)
    {
        User user = new User();
        List<User> allUsers = user.Read();

        User foundUser = allUsers.FirstOrDefault(u => u.UserId == userId);

        if (foundUser == null)
        {
            return NotFound();
        }

        return Ok(foundUser);
    }

    // POST api/<UserRegisterController>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
    {
  
        int status = userRegister.Insert();
        if (status == 1)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
      
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
    {
        try
        {
            // Call the static Login method in the User class
            User ExistingUser = UserLogin.Login(userLogin.Email, userLogin.Password);

            if (ExistingUser == null)
            {
                return BadRequest("Email or password incorrect.");
            }

            return Ok(); // status = 200
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return BadRequest($@"User with email {userLogin.Email} not found"); // status = 400
        }
    }


    [HttpPut("{email}")]
    public IActionResult Put([FromBody] User user)
    {
        int status = user.Update();
        if (status == 1)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    // DELETE api/<UserRegisterController>/5
    [HttpDelete("{email}")]
    public IActionResult Delete(string email)
    {
        User u = new User();
        u.Delete(email);
        return Ok();
    }


}
