using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class DataService
    {
        private DataManagement management;

        public DataService(DataManagement management)
        {
            if (management != null)
            {
                this.management = management;
            }
            else throw new NullReferenceException(management.GetType().ToString() + " is null!");
        }
    }
}
