using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.DTO
{
    public class AlunoCadastroDTO
    {
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime Data_Inicio { get; set; } = DateTime.Now;
        public DateTime Data_Nasc { get; set; }
        public DateTime? Data_Fim { get; set; } = null;
    }
}
