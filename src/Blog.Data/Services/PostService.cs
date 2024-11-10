using Blog.Data.Data;
using Blog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blog.Data.Services
{
    public class PostService
    {
        private ApplicationDbContextData _context;
        
        public PostService(ApplicationDbContextData context)
        {
            _context = context;
        }

        //[HttpGet]
        public async Task<List<Post>> GetPosts()
        {
            var post2 = await _context.Posts.ToListAsync();
            return post2;
        }

        //[HttpGet("{id:int}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            return post;
        }

        //[HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return post;
        }

        //[HttpPut("{id:int}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return (IActionResult)post;
        }

        //[HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            /*
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            */
            //var x = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            _context.SaveChanges();

            return (IActionResult)post;
        }

        public async Task<IActionResult> DeletePostObj(Post post)
        {
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            _context.SaveChanges();

            return (IActionResult)post;
        }
    }
}
