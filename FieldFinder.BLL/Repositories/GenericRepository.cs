using FieldFinder.BLL.Interfaces;
using FieldFinder.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _dbContext.SaveChanges();  // db
        }

        public void Delete(T t)
        {
            _dbContext.Set<T>().Remove(t);
            _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
            => _dbContext.Set<T>().ToList();
        

        public void Update(T t)
        {
           _dbContext.Set<T>().Update(t);
            _dbContext.SaveChanges();
        }
    }
}
