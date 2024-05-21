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
        public IActionResult GetPosts()
        {
            List<PostWithUser> posts = PostWithUser.Read();
            return Ok(posts);
        }


        [HttpGet("byId/{postId}")]
        public IActionResult GetPostById(int postId)
        {
            PostWithUser post = PostWithUser.ReadById(postId);
            return Ok(post);
        }

        [HttpGet("byCategory/{category}")]
        public IActionResult GetPostByCategory(int category)
        {
            List<PostWithUser> posts = PostWithUser.ReadByCategory(category);
            return Ok(posts);
        }

        [HttpGet("byUser/{userId}")]
        public IActionResult GetPostByUser(int userId)
        {
            List<PostWithUser> posts = PostWithUser.ReadByUser(userId);
            return Ok(posts);
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("newPost")]
        public IActionResult NewPost([FromBody] PostCreation postDto)
        {
            UserDBservices userDBservices = new UserDBservices();
            List<User> allUsers = userDBservices.ReadUsers();

            User? foundUser = allUsers.FirstOrDefault(u => u.UserId == postDto.UserId);
            if (foundUser == null)
            {
                return Unauthorized("User not found");
            }
            // Call the NewPost method to create the post
            Post post = new Post(postDto);
            int status = post.NewPost();
            PostWithUser postWithUser = new PostWithUser(post, foundUser);
            if (status == 1)
            {
                return Ok(postWithUser);
            }
            else
            {
                return BadRequest();
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
            Post.Delete(postId);
            return Ok();
        }
    }
}
