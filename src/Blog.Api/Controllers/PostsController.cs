using Blog.Data.Models;
using Microsoft.AspNetCore.Mvc;

using Blog.Data.Data;
using Blog.Data.Services;
using Microsoft.AspNetCore.Authorization;

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
            //_context.Posts.Update(post);
            //await _context.SaveChangesAsync();

            return NoContent(); ;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {

            //var post = await _context.Posts.FindAsync(id);
            //_context.Posts.Remove(post);
            //await _context.SaveChangesAsync();

            var postServices = new PostService(_context);
            postServices.DeletePost(id);

            return NoContent();
        }
    }
}
