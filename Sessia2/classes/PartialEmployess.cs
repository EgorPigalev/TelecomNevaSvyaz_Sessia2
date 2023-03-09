using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sessia2
{
    public partial class Employees
    {
        public string FIO
        {
            get
            {
                return "" + Surname + " " + Name + " " + Patronymic;
            }
        }
    }
}
