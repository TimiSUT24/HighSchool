using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchool
{
    public static class Database    //ConnectionString for ADO.NET
    {
        public static readonly string ConnectionString =
            "Data Source=localhost;Database=HÖGSTADIESKOLA;Integrated Security=True;Trust Server Certificate=true;";
    }
}
