using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.Web.Data;
using Blog.Data.Models;
using System.Security.Claims;

using Blog.Data.Data;

namespace Blog.Web.Controllers
{
    public class ComentariosController : Controller
    {
        private readonly ApplicationDbContextData _context;

        public ComentariosController(ApplicationDbContextData context)
        {
            _context = context;
        }

        public async Task<IActionResult> ViewComments(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Home", "Index");
            }

            var comments = _context.Comentarios
                .Where(m => m.Id_post == id);
            if (comments == null)
            {
                return RedirectToAction("Index","Home");
            }

            return View(comments);
        }

        // GET: Comentarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comentarios.ToListAsync());
        }

        // GET: Comentarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // GET: Comentarios/Create
        public IActionResult Create(int id)
        {
            var comentario = new Comentario();

            comentario.Id_post = id;

            return View(comentario);
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_post,Id_usuario,Descricao,DataComentario,Autor")] Comentario comentario)
        {

            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            comentario.Id_usuario = currentUserName;

            //if (ModelState.IsValid)
            //{
                //comentario.Id_post = Id_post;

                _context.Add(comentario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");

                //return RedirectToAction(nameof(Index));
                //return RedirectToAction("ViewComments","Comentarios");
                //return RedirectToAction("Home", "Index");
            //}
            //return View(comentario);
        }

        // GET: Comentarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }
            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_post,Id_usuario,Descricao,DataComentario")] Comentario comentario)
        {
            if (id != comentario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentarioExists(comentario.Id))
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
            return View(comentario);
        }

        // GET: Comentarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);            
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario != null)
            {
                _context.Comentarios.Remove(comentario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool ComentarioExists(int id)
        {
            return _context.Comentarios.Any(e => e.Id == id);
        }
    }
}
