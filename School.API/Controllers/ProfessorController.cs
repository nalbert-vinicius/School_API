using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.API.Data;
using School.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        public ProfessorController(IRepository repository, DataContext context)
        {
            _repo = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professor = _repo.GetAllProfessores(true);
            if (professor == null) { return BadRequest("Professores não encontrados!"); };
            return Ok(professor);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, true);
            if (professor == null) { return BadRequest("Professores não encontrados!"); };
            return Ok(professor);
        }


        [HttpGet("ByDisciplina/{id}")]
        public IActionResult GetBtDisciplinaId(int id)
        {
            var professor = _repo.GetaAllProfessorByDisciplinaId(id, true);
            if (professor == null) { return BadRequest("Professores não encontrados!"); };
            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Ok("Professor cadastrado com sucesso!");
            }

            return BadRequest("Professor não foi cadastrado!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Ok("Dados atualizados com sucesso!");
            }

            return BadRequest("Dados não foram atualizados!");

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) { return BadRequest("Professor não encontrado!"); }
            _repo.Delete(professor);
            if (_repo.SaveChanges())
            {
                return Ok("Professor removido sucesso!");
            }

            return BadRequest("Erro ao remover!");
        }


    }
}
