using Blog.Data.Data;
using Blog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Services
{
    public class ComentariosService
    {
        private ApplicationDbContextData _context;

        public ComentariosService(ApplicationDbContextData context)
        {
            _context = context;
        }

        public async Task<List<Comentario>> GetComentarios(int id)
        {
            /*
            var comentario = await _context.Comentarios.ToListAsync();
            return comentario;
            */
            
            var comentario = _context.Comentarios
                .Where(m => m.Id_post == id);

            /*
            if (comments == null)
            {
                return RedirectToAction("Index","Home");
            }
            */
            return comentario.ToList();
        }

        public async Task<ActionResult<Comentario>> GetComentario(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            return comentario;
        }
        public async Task<ActionResult<Comentario>> PostComentario(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return comentario;
        }

        public async Task<IActionResult> DeleteComentario(int id)
        {

            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario != null)
            {
                _context.Comentarios.Remove(comentario);
            }

           await  _context.SaveChangesAsync();

            return (IActionResult)comentario;
        }
    }
}
