using FieldFinder.BLL.Interfaces;
using FieldFinder.DAL.Context;
using FieldFinder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldFinder.BLL.Repositories
{
    public  class FieldRepository:GenericRepository<Field>, IFieldRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FieldRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext=dbContext;
        }

       
    }
}
