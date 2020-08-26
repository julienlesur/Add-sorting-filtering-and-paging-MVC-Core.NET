using System;
using System.Collections.Generic;
using System.Text;

namespace Recruiting.BL.Models
{
    public class PaginationListResult<T>
    {
        public IEnumerable<T> List { get; set; }
        public int NumbersOfItems { get; set; }
    }
}
