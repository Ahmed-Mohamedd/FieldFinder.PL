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
    public class CategoryRepository:GenericRepository<Category> , ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext=dbContext;
        }
    }
}
