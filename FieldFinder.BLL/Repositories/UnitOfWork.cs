using FieldFinder.BLL.Interfaces;
using FieldFinder.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldFinder.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IFieldRepository Fields        { get ;private  set; }
        public ICategoryRepository Categories { get ;private set ; }
        public ICoachRepository Coaches       { get ;private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Fields = new FieldRepository(dbContext);
            Categories = new CategoryRepository(dbContext);
            Coaches = new CoachRepository(dbContext);
        }
        public void Dispose()
            => _dbContext.Dispose();

        public int Save()
            => _dbContext.SaveChanges();
    }
}
