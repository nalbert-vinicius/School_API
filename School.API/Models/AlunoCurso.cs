using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Models
{
    public class AlunoCurso
    {
        public AlunoCurso() { }
        public AlunoCurso(int alunoId, int cursoId)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
        }

        public DateTime Data_Inicio { get; set; } = DateTime.Now;
        public DateTime? Data_Fim { get; set; }
        public int AlunoId { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public Aluno Aluno { get; set; }
    }
}
