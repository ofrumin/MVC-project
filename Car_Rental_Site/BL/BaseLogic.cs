using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL
{
    public abstract class BaseLogic : IDisposable
    {
        private ModelContext _db = new ModelContext();

        protected ModelContext DB
        {
            get { return _db; }
        }

        public void Dispose()
        {
            if (_db != null)
                _db.Dispose();
        }
    }
}
