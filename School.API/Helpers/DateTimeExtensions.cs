using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetIdade(this DateTime datetime)
        {
            var dataAtual = DateTime.UtcNow;
            int idade = dataAtual.Year - datetime.Year;

            if (dataAtual < datetime.AddYears(idade))
                idade--;

            return idade;
        }
    }
}
