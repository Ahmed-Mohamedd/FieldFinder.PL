using FieldFinder.BLL.Interfaces;
using FieldFinder.DAL.Context;
using FieldFinder.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FieldFinder.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext) //Allow Dependency Injection For Our Dependencies
        {
            _dbContext=dbContext;
        }


        public void Add(T t)
        {
            _dbContext.Set<T>().Add(t); //memory
            //_dbContext.SaveChanges();  // db
        }

        public void Delete(T t)
        {
            _dbContext.Set<T>().Remove(t);
            //_dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T) == typeof(Field))
                return (IEnumerable<T>) _dbContext.Set<Field>().Include(f=> f.Category).ToList();
            return _dbContext.Set<T>().ToList();
        }
        public T GetById(Expression<Func<T, bool>> filter)
            => _dbContext.Set<T>().FirstOrDefault(filter);

        public void Update(T t)
        {
           _dbContext.Set<T>().Update(t);
            //_dbContext.SaveChanges();
        }
    }
}
