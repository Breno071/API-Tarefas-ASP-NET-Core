using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciamentoDeTarefas_ASP_NET_Core.Data;
using GerenciamentoDeTarefas_ASP_NET_Core.Models;
using Microsoft.AspNetCore.Authorization;
using GerenciamentoDeTarefas_ASP_NET_Core.Enum;
using GerenciamentoDeTarefas_ASP_NET_Core.ViewModel;
using GerenciamentoDeTarefas_ASP_NET_Core.Services;

namespace GerenciamentoDeTarefas_ASP_NET_Core.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsuariosController : ControllerBase
  {
    private readonly ApplicationContext _context;

    public UsuariosController(ApplicationContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Usuario>>> GetUsuarios()
    {
      return await _context.Usuarios.ToListAsync();
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUsuario(int id)
    {
      var usuario = await _context.Usuarios.FindAsync(id);

      if (usuario == null)
      {
        return NotFound();
      }

      return usuario;
    }

    // PUT: api/Usuarios/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
    {
      if (id != usuario.id)
      {
        return BadRequest();
      }

      _context.Entry(usuario).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UsuarioExists(id))
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

    // POST: api/Usuarios
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /*[HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUsuario", new { id = usuario.id }, usuario);
    }*/

    // DELETE: api/Usuarios/5
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
      var usuario = await _context.Usuarios.FindAsync(id);
      if (usuario == null)
      {
        return NotFound();
      }

      _context.Usuarios.Remove(usuario);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpPost("logar")]
    public ActionResult<dynamic> Logar(UsuarioViewModel usuario)
    {
      var query = from m in _context.Usuarios where m.login == usuario.login select m;

     if(query.Count() == 0)
    {
        return NotFound("Senha ou login Inválidos");
    }


      Usuario user = query.First();

      var token = TokenService.TokenGenerator(user);

      return new
      {
        user = user,
        token = token,
      };
    }

    [HttpPost("cadastrar")]
    public async Task<IActionResult> Cadastrar([FromBody] Usuario usuario)
    {
      var query = from m in _context.Usuarios where m.login == usuario.login select m;

      if (query.Contains(usuario)) return BadRequest("Usuário já cadastrado");

      _context.Usuarios.Add(usuario);
      await _context.SaveChangesAsync();

      return Created("", usuario);
    }

    private bool UsuarioExists(int id)
    {
      return _context.Usuarios.Any(e => e.id == id);
    }
  }
}
