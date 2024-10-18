using Blog.Api.Data;
using Blog.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.Api.Data;

namespace Blog.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public PostsController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts() 
        { 
            return await _context.Posts.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        { 
            var post = await _context.Posts.FindAsync(id);
            return post;
        }

        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        { 
            _context.Posts.Add(post); 
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        { 
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return NoContent(); ;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
