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
        private readonly DataContext _context;
        public ProfessorController(IRepository repository, DataContext context)
        {
            _repo = repository;
            _context = context;
        }

        [HttpGet]
        public IActionResult get()
        {
            return Ok(_repo.GetAllProfessores());
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
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
            if(professor == null) { return BadRequest("Professor não encontrado!"); }
            _repo.Delete(professor);
            if (_repo.SaveChanges())
            {
                return Ok("Professor removido sucesso!");
            }

            return BadRequest("Erro ao remover!");
        }


    }
}
