
namespace inventoryeyeback;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;
using inventoryeyeback.Dto;

using AppUser = User;
using inventoryEyeBack;

[Route("api/[controller]")]
[ApiController]
public class AuthUserController : ControllerBase
{
 
    private IConfiguration configuration;

    private DatabaseContext context;
    public AuthUserController(IConfiguration configuration, DatabaseContext context)
    {
        this.configuration = configuration;
        this.context = context;
    }



    [HttpGet("personalInfo/{userId}")]
    public IActionResult GetUserInformation(int userId)
    {
        UserDBservices userDBservices = new UserDBservices();
        PostDBservices postDBservices = new PostDBservices();

        List<User> allUsers = userDBservices.ReadUsers();

        User? foundUser = allUsers.FirstOrDefault(u => u.UserId == userId);

        if (foundUser == null)
        {
            return NotFound();
        }

        List<PostWithUser> allPosts = postDBservices.ReadPostsWithUsers().Where(p => p.UserId == userId).ToList();
        foundUser.Posts = foundUser.Posts.Concat(allPosts).ToList();
        return Ok(foundUser);
    }

    // POST api/<UserRegisterController>
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegister userRegister)
    {

        int status = userRegister.Insert();
        if (status == 1)
        {
            return Ok(userRegister);
        }
        else
        {
            return BadRequest();
        }

    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        try
        {
            User existingUser = UserLogin.Login(userLogin.Email, userLogin.Password);
            if (existingUser == null)
            {
                return BadRequest("Email or password incorrect.");
            }

            // Check if UserId is valid
            if (existingUser.UserId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            // Store the user ID in the session
            //string sessionId = Guid.NewGuid().ToString();
            //HttpContext.Session.SetString(sessionId, existingUser.UserId.ToString());

            // Return the session ID to the client
            return Ok(new { userId = existingUser.UserId });
        }
        catch (Exception e)
        {
            // Log the exception details
            Console.WriteLine($"Exception: {e.Message}\nStack Trace: {e.StackTrace}");


            // Return an appropriate error response
            return StatusCode(500, "An error occurred during login.");
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
        AppUser.Delete(email);
        return Ok();
    }


}
