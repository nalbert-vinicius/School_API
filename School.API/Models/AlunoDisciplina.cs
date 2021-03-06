using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Models
{
    public class AlunoDisciplina
    {
        public AlunoDisciplina() { }
        public AlunoDisciplina(int alunoId, int disciplinaId)
        {
            AlunoId = alunoId;
            DisciplinaId = disciplinaId;
        }

        public int? Nota { get; set; } = null;
        public DateTime Data_Inicio { get; set; } = DateTime.Now;
        public DateTime? Data_Fim { get; set; }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public Aluno Aluno { get; set; }
    }
}
