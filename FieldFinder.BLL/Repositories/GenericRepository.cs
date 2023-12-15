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


        public async void Add(T t)
        {
            await _dbContext.Set<T>().AddAsync(t); //memory
            //_dbContext.SaveChanges();  // db
        }

        public async void Delete(T t)
        {
             _dbContext.Set<T>().Remove(t);
            //_dbContext.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof(Field))
                return (IEnumerable<T>) await _dbContext.Set<Field>().Include(f=> f.Category).Include(f=> f.Location).ToListAsync();
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetById(Expression<Func<T, bool>> filter) 
            => await _dbContext.Set<T>().FirstOrDefaultAsync(filter);

        public async void Update(T t)
        {
            _dbContext.Set<T>().Update(t);
            //_dbContext.SaveChanges();
        }
    }
}
