using AutoMapper;
using School.API.DTO;
using School.API.Models;

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
             * Mapeamento de alunos
             * **/
            CreateMap<Aluno, AlunosDTO>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"))
                .ForMember(dest => dest.Idade, opt => opt.MapFrom(src => src.Data_Nasc.GetIdade()));

            CreateMap<AlunosDTO, Aluno>();

            CreateMap<AlunosDTO, Aluno>().ReverseMap();

            /** 
             * Mapemaneto de professores
             * **/
            CreateMap<Professor, ProfessorDTO>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"));

            CreateMap<ProfessorDTO, Professor>().ReverseMap();
        }
    }
}
