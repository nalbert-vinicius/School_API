using Microsoft.EntityFrameworkCore;
using School.API.Helpers;
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

        //ALUNOS METHODS
        public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (incluirProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas).ThenInclude(ad => ad.Disciplina).ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
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

        public Aluno[] GetAllAlunoByDisciplinaId(int disciplinaId, bool incluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (incluirProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas).ThenInclude(ad => ad.Disciplina).ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking().OrderBy(aluno => aluno.Id).Where(a => a.AlunosDisciplinas.Any(ad => ad.Disciplina.Id == disciplinaId));

            return query.AsNoTracking().OrderBy(a => a.Id).ToArray();
        }




        // PROFESSOR METHODS
        public Professor[] GetAllProfessores(bool incluirDisciplina = false)
        {
            IQueryable<Professor> query = _context.Professores;
            if (incluirDisciplina)
            {
                query = query.Include(d => d.Disciplina).ThenInclude(ad => ad.AlunosDisciplinas).ThenInclude(a => a.Aluno);
            }

            return query.AsNoTracking().OrderBy(a => a.Id).ToArray();
        }

        public Professor[] GetaAllProfessorByDisciplinaId(int disciplinaId, bool incluirDisciplina = false)
        {
            IQueryable<Professor> query = _context.Professores;
            if (incluirDisciplina)
            {
                query = query.Include(d => d.Disciplina).ThenInclude(ad => ad.AlunosDisciplinas).ThenInclude(a => a.Aluno);
            }

            query = query.AsNoTracking().Where(a => a.Disciplina.Any(d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)));
            return query.ToArray();
        }

        public Professor GetProfessorById(int professorId, bool incluirDisciplina = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (incluirDisciplina)
            {
                query = query.Include(d => d.Disciplina).ThenInclude(ad => ad.AlunosDisciplinas).ThenInclude(a => a.Aluno);
            }

            return query.AsNoTracking().OrderBy(p => p.Id).Where(p => p.Id == professorId).FirstOrDefault();
            ;
        }


    }
}
