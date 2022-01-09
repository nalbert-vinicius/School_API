using Microsoft.EntityFrameworkCore;
using School.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Data
{
    public class Repository : IRepository
    {

        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }

        //o tipo T recebe a classe do objeto Aluno, professor, disciplina
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);   
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        //Alunos methods :)
        public Aluno[] GetAllAlunos(bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (incluirProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas).ThenInclude(ad => ad.Disciplina).ThenInclude(p => p.Professor);
            }

            return query.AsNoTracking().OrderBy(a => a.Id).ToArray();
        }

        public Aluno GetAlunoById(int Id, bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (incluirProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas).ThenInclude(ad => ad.Disciplina).ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking().OrderBy(aluno => aluno.Id).Where(aluno => aluno.Id == Id);
            //retorna primeirou ou ultimo da query
            return query.FirstOrDefault();
        }

        public Aluno[] GetAlunoByDisciplinaId(int disciplinaId,bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (incluirProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas).ThenInclude(ad => ad.Disciplina).ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking().OrderBy(aluno => aluno.Id).Where(a => a.AlunosDisciplinas.Any(ad => ad.Disciplina.Id == disciplinaId));

            return query.AsNoTracking().OrderBy(a => a.Id).ToArray();
        }

        public Professor[] GetAllProfessores()
        {
            return _context.Professores.ToArray();
        }

        public Professor[] GetProfessorById()
        {
            throw new NotImplementedException();
        }

        public Professor[] GetProfessorByDisciplinaId()
        {
            throw new NotImplementedException();
        }
    }
}
