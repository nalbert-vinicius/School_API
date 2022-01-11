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
        private readonly IRepository _repo;
        public AlunosController(IRepository repository) 
        {
            _repo = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var aluno = _repo.GetAllAlunos(true);
            if (aluno == null) { return BadRequest( "Alunos não encontrados!"); }
            return Ok(aluno);
        }

        //Da para realizar tipagem do parametro na url /{id:int}
        // Também da para usar query string na url api/aluno/byId?id=1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var alunos = _repo.GetAlunoById(id, true);
            if(alunos == null)
            {
                return BadRequest("Aluno não foi encontrado");
            }
            return Ok(alunos);
        }

        [HttpGet("ByDisciplinaId/{id}")]
        public IActionResult GetByDisciplinaId(int id)
        {
            var alunos = _repo.GetAllAlunoByDisciplinaId(id, true);
            if (alunos == null)
            {
                return BadRequest("Aluno  não foi encontrado");
            }
            return Ok(alunos);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não cadastrado!");
            
        }

        [HttpPut("{Id}")]
        public IActionResult Put(int Id, Aluno aluno)
        {
            //asNotracking não bloqueia o registro no banco
            var alu = _repo.GetAlunoById(Id);
            if (alu == null){return BadRequest("Aluno não foi encontrado");}
            _repo.Update(aluno);
            if(_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não encontrado!");
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var alu = _repo.GetAlunoById(Id);
            if (alu == null) { return BadRequest("Aluno não foi encontrado"); };
            _repo.Delete(alu);
            if (_repo.SaveChanges()) {
                return Ok("Aluno removido!");
            };
            return BadRequest("Aluno não foi removido!");
        }
    }
}
