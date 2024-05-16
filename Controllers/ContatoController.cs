using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetModuloAPI.Context;
using DotnetModuloAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DotnetModuloAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        [HttpPost("Cadastrar")]
        public IActionResult Cadastrar(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new{id = contato.Id}, contato);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos(){
            var contato = _context.Contatos;

            if (contato == null)
                return NotFound();

            return Ok(contato);
        }

            [HttpGet("ObterTodos{nome}")]
        public IActionResult ObterPorNome(string nome)
        {
            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));
            return Ok(contatos);
        }

        [HttpGet("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
                return NotFound();

            return Ok(contato);
        }

        [HttpPut("Atualizar/{id}")]
        public IActionResult Atualizar(int id, Contato contato)
        {
            var _contato = _context.Contatos.Find(id);

            if (_contato == null)
                return NotFound();

            _contato.Nome = contato.Nome;
            _contato.Telefone = contato.Telefone;
            _contato.Ativo = contato.Ativo;

            _context.Contatos.Update(_contato);
            _context.SaveChanges();

            return Ok(_contato);
        }

        [HttpDelete("Apagar{id}")]
        public IActionResult Delete(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
                return NotFound();

            _context.Contatos.Remove(contato);
            _context.SaveChanges();

            return NoContent();
        }
        
    }
}