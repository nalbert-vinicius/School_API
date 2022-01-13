using AutoMapper;
using School.API.DTO;
using School.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Helpers
{
    public class School : Profile
    {
        /** 
         * É possivel usar a função ReverseMap() para que o mapeamento possar ser feito de ambos os lados
         * Aluno to alunoDTO e alunoDTO para aluno
        **/
        public School()
        {
            /** 
             * Aluno<Source - Dados>, AlunosDTO<Destination - destino>
             * ForMember<Campo - Nome > Options - opções
             * **/
            CreateMap<Aluno, AlunosDTO>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"))
                .ForMember(dest => dest.Idade, opt => opt.MapFrom(src => src.Data_Nasc.GetIdade()));

            CreateMap<AlunosDTO, Aluno>();

            CreateMap<AlunosDTO, Aluno>().ReverseMap();
        }
    }
}
