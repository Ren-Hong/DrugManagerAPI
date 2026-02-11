using System.Collections.Generic;

namespace DrugManager.Repositories.Contracts.PagedResult
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }

        public int Total { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}