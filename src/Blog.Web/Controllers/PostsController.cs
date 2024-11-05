using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.Web.Data;
using Blog.Data.Models;
using System.Security.Claims;
using Blog.Data.Services;

using Blog.Data.Data;
using Microsoft.Extensions.Hosting;

namespace Blog.Web.Controllers
{
    public class PostsController : Controller
    {
        //public readonly ApplicationDbContext _context;
        public readonly ApplicationDbContextData _context;

        public PostsController(ApplicationDbContextData context)
        {
            _context = context;
        }


        public async Task<IActionResult> ViewPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            /*
            var post = await _context.Posts
                //.Include(post => post.Id_usuario)
                //.Include(Comentario => Comentario).ThenInclude(post => post.Id_usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            */
            
            var postServices = new PostService(_context);
            var post2 = postServices.GetPost((int)id);

            return View(post2.Result.Value);
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.ToListAsync());
            //var postServices = new PostService(_context);
            //return View(postServices.GetPosts);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*
            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            */
            var postServices = new PostService(_context);
            var post2 = postServices.GetPost((int)id);

            return View(post2);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {            
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_usuario,Titulo,Descricao,DataPost,Autor")] Post post)
        {

            /*
            if (ModelState.IsValid)
            {

                System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;                
                post.Id_usuario = currentUserName;                

                _context.Add(post);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Home");
            }*/

            var postServices = new PostService(_context);
            var post2 = postServices.PostPost(post);

            //return View();
            return RedirectToAction("Index", "Home");
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_usuario,Titulo,Descricao,DataPost")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            */

            var postServices = new PostService(_context);
            postServices.DeletePost(id);

            return RedirectToAction("Index", "Home");
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
