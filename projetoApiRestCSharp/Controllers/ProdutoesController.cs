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
    public class ProdutoesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ProdutoesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Produtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProduto()
        {
            return await _context.Produto.ToListAsync();
        }

        // GET: api/Produtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(ulong id)
        {
            var produto = await _context.Produto.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        // PUT: api/Produtoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(ulong id, Produto produto)
        {
            if (id != produto.id)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
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

        // POST: api/Produtoes
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            bool tituloValido = false;
            bool dataValida = false;                      

            if (produto.titulo.Length <= 100)
            {
                tituloValido = true;
            }
            else
            {
                throw new InvalidOperationException("O Titulo do Produto possui mais de 100 caracteres");
            }

            if (DateTime.Compare(produto.dataAquisicao, DateTime.Today.Date) <= 0)
            {
                dataValida = true;
            }
            else
            {
                throw new InvalidOperationException("O Data de Aquisicao é superios a data atual");
            }            

            if (tituloValido == true && dataValida == true)
            {
                _context.Produto.Add(produto);
                await _context.SaveChangesAsync();
                
                return CreatedAtAction(nameof(GetProduto), new { id = produto.id }, produto);
            }

            return NotFound();
        }

        // DELETE: api/Produtoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> DeleteProduto(ulong id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        private bool ProdutoExists(ulong id)
        {
            return _context.Produto.Any(e => e.id == id);
        }
    }
}
