using Blog.Api.Data;
using Blog.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/comentarios")]
    public class ComentariosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ComentariosController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentarios()
        {
            return await _context.Comentarios.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Comentario>> GetComentario(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            return comentario;
        }

        [HttpPost]
        public async Task<ActionResult<Post>> PostComentario(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComentario), new { id = comentario.Id }, comentario);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutComentario(int id, Comentario comentario)
        {
            _context.Comentarios.Update(comentario);
            await _context.SaveChangesAsync();

            return NoContent(); ;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
