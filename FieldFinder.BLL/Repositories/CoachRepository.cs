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
    public class CoachRepository:GenericRepository<Coach> , ICoachRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CoachRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<Coach> GetById(int value)
        {
            throw new NotImplementedException();
        }
    }
}
