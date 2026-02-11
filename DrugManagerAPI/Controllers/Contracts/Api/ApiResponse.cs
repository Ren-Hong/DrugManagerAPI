using System.Collections.Generic;

namespace Cms.Web.Controllers.Contracts.Api
{
    public class ApiResponse
    {
        public bool Success { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public Dictionary<string, List<string>> ValidationErrors { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; } //ResponseModel
    }
}