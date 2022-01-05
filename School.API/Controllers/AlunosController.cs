using Microsoft.AspNetCore.Mvc;
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
        public AlunosController() { }

        public List<Aluno> Alunos = new List<Aluno>()
        {
            new Aluno(){
                Id = 1,
                Nome = "Nalbertinho",
                Sobrenome = "Vinicius"
            },
            new Aluno(){
                Id = 2,
                Nome = "Cloe",
                Sobrenome = "Nilda"
            },
            new Aluno(){
                Id = 3,
                Nome = "Gaby",
                Sobrenome = "Leonel"
            },
            new Aluno(){
                Id = 4,
                Nome = "Teste",
                Sobrenome = "Vinicius"
            },

        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        //Da para realizar tipagem do parametro na url /{id:int}
        // Também da para usar query string na url api/aluno/byId?id=1
        [HttpGet("testeRota/{Id}")]
        public IActionResult GetById(int Id)
        {
            var alunos = Alunos.FirstOrDefault(x => x.Id == Id);
            if(alunos == null)
            {
                return BadRequest("Aluno não foi encontrado");
            }

            return Ok(alunos);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPut("{Id}")]
        public IActionResult Put(int Id)
        {
            return Ok("teste");
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            return Ok("Deletado");
        }
    }
}
