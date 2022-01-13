﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.API.Data;
using School.API.DTO;
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
        private readonly IMapper _mapper;
        public AlunosController(IRepository repository, IMapper mapper) 
        {
            _repo = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);          
            if (alunos == null) { return BadRequest( "Alunos não encontrados!"); }
            return Ok(_mapper.Map(alunos, new List<AlunosDTO>()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var alunos = _repo.GetAlunoById(id, true);
            if(alunos == null) { return BadRequest("Aluno não foi encontrado"); }
            return Ok(_mapper.Map<AlunosDTO>(alunos));
        }

        [HttpGet("ByDisciplinaId/{id}")]
        public IActionResult GetByDisciplinaId(int id)
        {
            var alunos = _repo.GetAllAlunoByDisciplinaId(id, true);
            if (alunos == null) { return BadRequest("Aluno  não foi encontrado"); }
            return Ok(_mapper.Map<AlunosDTO>(alunos));
        }

        [HttpPost]
        public IActionResult Post(AlunoCadastroDTO modelo)
        {
            // Faz mapeamento de alunos DTO para alunos
            var aluno = _mapper.Map<Aluno>(modelo);

            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"api/aluno/{modelo.Id}",_mapper.Map<AlunoCadastroDTO>(aluno));
            }
            return BadRequest("Aluno não cadastrado!");
            
        }

        [HttpPut("{Id}")]
        public IActionResult Put(int Id, AlunoCadastroDTO modelo)
        {
            //asNotracking não bloqueia o registro no banco
            var aluno = _repo.GetAlunoById(Id);
            if (aluno == null){ return BadRequest("Aluno não foi encontrado"); }

            //Recebo os dados<source - model>, mapeio para objeto aluno<destination - aluno>
            _mapper.Map(modelo, aluno);

            _repo.Update(aluno);
            if(_repo.SaveChanges())
            {
                //a URI é um parametro do metodo created!
                return Created($"api/aluno/{modelo.Id}", _mapper.Map<AlunoCadastroDTO>(aluno));
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
