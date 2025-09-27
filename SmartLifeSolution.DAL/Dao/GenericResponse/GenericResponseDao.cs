using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dao.GenericResponse
{
    public class GenericResponseDao
    {
        public string Message { get; set; }
        public object data { get; set; }
        public int StatusCode { get; set; }
        public bool IsError { get; set; }
    }
}
