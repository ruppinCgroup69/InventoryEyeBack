using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;
using inventoryeyeback.Dto;
using inventoryeyeback;
using System.ComponentModel;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace inventoryEyeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        // GET: api/<PostController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("newPost")]
        public async Task<IActionResult> NewPost([FromBody] Post post)
        {

            int status = post.NewPost();
            if (status == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        //[HttpPost("newPost")]
        //public async Task<IActionResult> NewPost([FromBody] Post post, int userId)
        //{
        //    User user = new User();
        //    List<User> allUsers = user.Read();

        //    User foundUser = allUsers.FirstOrDefault(u => u.UserId == userId);

        //    // Call the method to create the post
        //    int status = await post.NewPost(); // Assuming NewPostAsync is now an async method that returns a Task<int>

        //    if (status == 1)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest("Failed to create post.");
        //    }
        //}



        //PUT api/<PostController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Post post)
        {
            int status = post.Update();
            if (status == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{postId}")]
        public IActionResult Delete(int postId)
        {
            Post p = new Post();
            p.Delete(postId);
            return Ok();
        }
    }
}
