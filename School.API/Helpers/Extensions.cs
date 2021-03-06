using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Helpers
{
    public static class Extensions 
    {
        public static void AddPagination(this HttpResponse response,int currentPage, int itemsPerPage, int totalPages, int totalItems)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalPages, totalItems);
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader));
            response.Headers.Add("Access-Control-Expose-Header", "Pagination");
        }

    }
}
