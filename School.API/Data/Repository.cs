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


    }
}
