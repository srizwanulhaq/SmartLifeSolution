using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dto.Paging
{
    public class PagingDto
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public int Skip => (Page - 1) * Size;
    }

    public class PagingDao<T> where T : class
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public int Skip => (Page - 1) * Size;
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
        
}
