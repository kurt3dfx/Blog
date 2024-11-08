using Blog.Api.Data;
using Blog.Data.Data;
using Blog.Data.Models;
using Blog.Data.Services;
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
        private readonly ApplicationDbContextData _context;

        public ComentariosController(ApplicationDbContextData context)
        {
            _context = context;
        }

        [HttpGet]
        //[Route("meus-comentario")]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentarios(int id)
        {
            //return await _context.Comentarios.ToListAsync();
            var comentariosServices = new ComentariosService(_context);
            var comentarios = comentariosServices.GetComentarios(id);

            return comentarios.Result;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Comentario>> GetComentario(int id)
        {
            //var comentario = await _context.Comentarios.FindAsync(id);
            //return comentario;
            var comentariosServices = new ComentariosService(_context);
            var comentarios = comentariosServices.GetComentario(id);

            return comentarios.Result;
        }

        [HttpPost]
        public async Task<ActionResult<Post>> PostComentario(Comentario comentario)
        {
            //_context.Comentarios.Add(comentario);
            //await _context.SaveChangesAsync();
            var comentariosServices = new ComentariosService(_context);
            var comentarios = comentariosServices.PostComentario(comentario);

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

            var comentariosServices = new ComentariosService(_context);

            var comentario = await comentariosServices.GetComentario(id);
            if (comentario != null)
            {
                //_context.Comentarios.Remove(comentario);
                comentariosServices.DeleteComentarioObj(comentario.Value);
            }

            /*
            var comentario = await _context.Comentarios.FindAsync(id);
            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();
            var comentariosServices = new ComentariosService(_context);

            //var comentarios = comentariosServices.GetComentario(id);
            comentariosServices.DeleteComentario(id);
            */            

            return NoContent();
        }
    }
}
