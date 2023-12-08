using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldFinder.BLL.Interfaces
{
    public  interface IUnitOfWork:IDisposable
    {
        public IFieldRepository Fields { get;  }
        public ICategoryRepository Categories { get;  }
        public ICoachRepository Coaches { get;  }
        public Task<int> Save();

    }
}
