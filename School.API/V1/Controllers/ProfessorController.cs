using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.API.Data;
using School.API.DTO;
using School.API.Models;

using System.Collections.Generic;


namespace School.API.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/professor")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        public ProfessorController(IRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professor = _repo.GetAllProfessores(true);
            if (professor == null) { return BadRequest("Professores não encontrados!"); };
            return Ok(_mapper.Map(professor, new List<ProfessorDTO>()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, true);
            if (professor == null) { return BadRequest("Professores não encontrados!"); };
            return Ok(_mapper.Map<ProfessorDTO>(professor));
        }

        [HttpGet("ByAlunoId/{id}")]
        public IActionResult GetByAlunoId(int id)
        {
            var professor = _repo.GetProfessorByAlunoId(id, true);
            if (professor == null) { return BadRequest("Professores não encontrados!"); };
            return Ok(_mapper.Map<IEnumerable<ProfessorDTO>>(professor));
        }

        [HttpGet("ByDisciplina/{id}")]
        public IActionResult GetBtDisciplinaId(int id)
        {
            var professor = _repo.GetaAllProfessorByDisciplinaId(id, true);
            if (professor == null) { return BadRequest("Professores não encontrados!"); };
            return Ok(_mapper.Map<ProfessorDTO>(professor));
        }

        [HttpPost]
        public IActionResult Post(DTO.ProfessorCadastroDTO model)
        {
            var professor = _mapper.Map<Professor>(model);
            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Created("api/professor"+model.Id, _mapper.Map<DTO.ProfessorCadastroDTO>(professor));
            }

            return BadRequest("Professor não foi cadastrado!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, DTO.ProfessorCadastroDTO model)
        {
            var professor = _repo.GetProfessorById(id);
            if(professor == null) { return BadRequest("Professor não encontrado!"); }

            _mapper.Map(model, professor);
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
