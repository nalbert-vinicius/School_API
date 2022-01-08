using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.API.Data;
using School.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace School.API.Controllers
{
    [Route("api/aluno")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IRepository _repo;
        public AlunosController(DataContext context, IRepository repository) 
        {
            _context = context;
            _repo = repository;
        }

 

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        //Da para realizar tipagem do parametro na url /{id:int}
        // Também da para usar query string na url api/aluno/byId?id=1
        [HttpGet("testeRota/{Id}")]
        public IActionResult GetById(int Id)
        {
            var alunos = _context.Alunos.FirstOrDefault(x => x.Id == Id);
            if(alunos == null)
            {
                return BadRequest("Aluno não foi encontrado");
            }

            return Ok(alunos);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpPut("{Id}")]
        public IActionResult Put(int Id, Aluno aluno)
        {
            //asNotracking não bloqueia o registro no banco
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == Id);
            if (alu == null){return BadRequest("Aluno não foi encontrado");}
            _context.Update(aluno);
            _context.SaveChanges();
            return Ok("teste");
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var alu = _context.Alunos.FirstOrDefault(x => x.Id == Id);
            if (alu == null) { return BadRequest("Aluno não foi encontrado"); };
            _context.Remove(Id);
            _context.SaveChanges();
            return Ok("Deletado");
        }
    }
}
