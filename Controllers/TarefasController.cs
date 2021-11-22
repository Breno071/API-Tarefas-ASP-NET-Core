using GerenciamentoDeTarefas_ASP_NET_Core.Data;
using GerenciamentoDeTarefas_ASP_NET_Core.Models;
using GerenciamentoDeTarefas_ASP_NET_Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeTarefas_ASP_NET_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TarefasController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TarefasController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Tarefas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefa()
        {
            return await _context.Tarefas.ToListAsync();
        }

        // GET: api/Tarefas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetTarefa(int id)
        {
            var tarefas = await _context.Tarefas.FindAsync(id);

            if (tarefas == null)
            {
                return NotFound();
            }

            return tarefas;
        }

        // PUT: api/Tarefas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefa(int id, TarefaViewModelInput tarefaViewModelInput, bool finalizada)
        {

            var tarefa = await _context.Tarefas.FindAsync(id);

            if (!TarefaViewModelInputExists(id))
            {
                return NotFound();
            }
            else
            {
                tarefa.nome = tarefaViewModelInput.nome;
                tarefa.finalizada = finalizada;
            }

            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }

            return NoContent();
        }

        // POST: api/Tarefas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tarefa>> PostTarefaViewModelInput(TarefaViewModelInput tarefaViewModelInput)
        {
            var tarefa = new Tarefa(tarefaViewModelInput.nome);
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("Tarefa criada com sucesso", new {tarefaViewModelInput.nome = tarefa.nome}, tarefa);
            return Ok(tarefa);
        }

        // DELETE: api/Tarefas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefaViewModelInput(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarefaViewModelInputExists(int id)
        {
            return _context.Tarefas.Any(e => e.id == id);
        }
    }
}
