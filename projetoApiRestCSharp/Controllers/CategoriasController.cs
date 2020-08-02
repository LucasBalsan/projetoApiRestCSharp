using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetoApiRestCSharp;
using projetoApiRestCSharp.Models;

namespace projetoApiRestCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CategoriasController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoria()
        {
            return await _context.Categoria.ToListAsync();
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(ulong id)
        {
            var categoria = await _context.Categoria.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // PUT: api/Categorias/5        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(ulong id, Categoria categoria)
        {
            if (id != categoria.id)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categorias        
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            bool tituloValido = false;
            
            if (categoria.titulo.Length <= 100)
            {
                tituloValido = true;
            }
            else
            {
                throw new InvalidOperationException("O Titulo da Categoria possui mais de 100 caracteres");
            }

            if (tituloValido == true)
            {
                _context.Categoria.Add(categoria);
                await _context.SaveChangesAsync();
                
                return CreatedAtAction(nameof(GetCategoria), new { id = categoria.id }, categoria);
            }
            
            return NotFound();                        
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> DeleteCategoria(ulong id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }

        private bool CategoriaExists(ulong id)
        {
            return _context.Categoria.Any(e => e.id == id);
        }
    }
}
