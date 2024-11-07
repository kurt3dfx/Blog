using Blog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Blog.Data.Data;
using Blog.Data.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Hosting;

namespace Blog.Api.Controllers
{    
    [Authorize]
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContextData _context;

        public PostsController(ApplicationDbContextData context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts() 
        {
            //return await _context.Posts.ToListAsync();
            var postServices = new PostService(_context);
            var post2 = postServices.GetPosts();
            return await post2;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            //var post = await _context.Posts.FindAsync(id);
            //return post;
            var postServices = new PostService(_context);
            var post2 = postServices.GetPost((int)id);

            return await post2;
        }

        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            //_context.Posts.Add(post); 
            //await _context.SaveChangesAsync();

            var postServices = new PostService(_context);
            var post2 = postServices.PostPost(post);

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {

            if (((post.Autor) == User.FindFirst(ClaimTypes.Email)?.Value) || ((User.FindFirst(ClaimTypes.Role)?.Value) == "Admin"))
            {
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return Unauthorized();
            }            

            /*
            var postServices = new PostService(_context);
            var post2 = postServices.GetPost((int)id);

            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if ((post2.Result.Value.Id_usuario) != currentUserName)
                return Unauthorized();

            return NoContent(); ;
            */
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {

            //var post = await _context.Posts.FindAsync(id);
            //_context.Posts.Remove(post);
            //await _context.SaveChangesAsync();
        
            var postServices = new PostService(_context);
            var post2 = postServices.GetPost((int)id);


            if (((post2.Result.Value.Autor) == User.FindFirst(ClaimTypes.Email)?.Value) || ((User.FindFirst(ClaimTypes.Role)?.Value) == "Admin"))
            {
                postServices.DeletePost(id);
                return NoContent();
            }
            else 
            {
                return Unauthorized();
            }              
            
        }
    }
}
