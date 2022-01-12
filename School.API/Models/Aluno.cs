using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Models
{
    public class Aluno
    {
        public Aluno(){ }

        public Aluno(int id, int matricula, string nome, string sobrenome, string telefone, DateTime data_nasc)
        {
            this.Id = id;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Telefone = telefone;
            this.Matricula = matricula;
            this.Data_Nasc = data_nasc;
        }

        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime Data_Inicio { get; set; } = DateTime.Now;
        public DateTime Data_Nasc { get; set; }
        public DateTime? Data_Fim { get; set; } = null;
        public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }
    }
}
