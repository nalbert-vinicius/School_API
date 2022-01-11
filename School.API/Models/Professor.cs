using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Models
{
    public class Professor
    {
        public Professor(){ }

        public Professor(int id, string nome, int registro, string sobrenome, string telefone)
        {
            Id = id;
            Nome = nome;
            Registro = registro;
            Sobrenome = sobrenome;
            Telefone = telefone;
        }

        public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime Data_Inicio { get; set; } = DateTime.Now;
        public DateTime? Data_Fim { get; set; } = null;
        public DateTime Data_Nasc { get; set; }
        public IEnumerable<Disciplina> Disciplina { get; set; }
    }
}
