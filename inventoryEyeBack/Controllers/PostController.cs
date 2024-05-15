using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;
using inventoryeyeback.Dto;
using inventoryeyeback;
using System.ComponentModel;


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
            // Get the user ID from the session
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                // Set the user ID in the post object
                post.UserId = userId.Value;

                // Call the NewPost method to create the post
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
            else
            {
                return Unauthorized("User not authenticated.");
            }
        }



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
