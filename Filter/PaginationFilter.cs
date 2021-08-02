using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPGC_API.Filter
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 15;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            //Máximo de pageSize de 15
            this.PageSize = pageSize > 15 ? 15 : pageSize;
        }
    }
}
