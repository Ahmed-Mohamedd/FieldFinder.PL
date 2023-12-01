using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldFinder.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class // crud
    {

        IEnumerable<T> GetAll(); //retrieve

        void Add(T t);  //create

        void Update(T t); //update
        void Delete(T t); //delete

    }
}
