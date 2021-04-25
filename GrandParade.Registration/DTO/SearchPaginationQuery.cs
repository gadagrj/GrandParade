using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;

namespace GrandParade.Registration.DTO
{
    public class SearchPaginationQuery : IPagedQuery
    {
        public int Page { get; set; }
        public int Results { get; set; }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }
    }
}
