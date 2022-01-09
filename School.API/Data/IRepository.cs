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

        Aluno[] GetAllAlunos();
        Aluno[] GetAllById();
        Aluno[] GetAlunoByDisciplinaId();

        Professor[] GetAllProfessores();
        Professor[] GetProfessorById();
        Professor[] GetProfessorByDisciplinaId();
    }
}
