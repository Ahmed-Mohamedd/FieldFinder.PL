using FieldFinder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldFinder.BLL.Interfaces
{
    public interface ICoachRepository : IGenericRepository<Coach>
    {
        Task<Coach> GetById(int value);
    }
}
