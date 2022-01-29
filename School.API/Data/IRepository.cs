using School.API.Helpers;
using School.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Data
{
    public interface IRepository
    {
        //Define o tipo classe
        void Add<T>(T entity) where T: class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool incluirProfessor = false);
        Aluno[] GetAllAlunoByDisciplinaId(int disciplinaId, bool incluirProfessor = false);
        Aluno GetAlunoById(int Id, bool incluirProfessor = false);

        Professor[] GetAllProfessores(bool incluirDisciplina = false);
        Professor[] GetaAllProfessorByDisciplinaId(int disciplinaId, bool incluirDisciplina = false);
        Professor GetProfessorById(int professorId, bool incluirDisciplina = false);
        Professor[] GetProfessorByAlunoId(int alunoId, bool incluirDisciplina = false);
    }
}
