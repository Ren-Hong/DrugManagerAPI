using System;
using System.Threading.Tasks;
using System.Web.Http;
using Cms.Web.Controllers.Contracts.Api;
using DrugManager.Controllers.Drug.Models;
using DrugManager.Repositories.Drug;

namespace DrugManagerAPI.Controllers
{
    [RoutePrefix("api/drug")]
    public class DrugController : ApiController
    {
        private readonly DrugRepository _repo = new DrugRepository();

        // ======================
        // 查 Code
        // GET /api/drug/A001
        // ======================
        [HttpGet]
        [Route("{code}")]
        public async Task<IHttpActionResult> Get(string code)
        {
            try
            {
                var entity = await _repo.GetByCodeAsync(code);

                if (entity == null)
                {
                    return Json(new ApiResponse
                    {
                        Success = false,
                        ErrorMessage = "找不到藥品資料"
                    });
                }

                return Json(new ApiResponse<object>
                {
                    Success = true,
                    Data = entity
                });
                }
            catch (Exception ex)
            {
                return Json(new ApiResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetByName(string name)
        {
            return Json(new ApiResponse
            {
                Success = false,
                ErrorMessage = "尚未實作此功能"
            });
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create(CreateDrugRequestModel model)
        {
            try
            {
                var success = await _repo.CreateDrugAsync(model);

                return Json(new ApiResponse
                {
                    Success = success,
                    ErrorMessage = success ? null : "新增失敗"
                });
            }
            catch (Exception ex)
            {
                return Json(new ApiResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("{code}")]
        public async Task<IHttpActionResult> Update(string code, UpdateDrugRequestModel model)
        {
            try
            {
                var success = await _repo.UpdateDrugByCodeAsync(code, model);

                return Json(new ApiResponse
                {
                    Success = success,
                    ErrorMessage = success ? null : "更新失敗"
                });
            }
            catch (Exception ex)
            {
                return Json(new ApiResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("{code}")]
        public async Task<IHttpActionResult> Delete(string code)
        {
            try
            {
                var success = await _repo.DeleteDrugByCodeAsync(code);

                return Json(new ApiResponse
                {
                    Success = success,
                    ErrorMessage = success ? null : "刪除失敗"
                });
            }
            catch (Exception ex)
            {
                return Json(new ApiResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
