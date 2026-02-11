using System.ComponentModel.DataAnnotations;

namespace DrugManager.Controllers.Drug.Models
{
    public class CreateDrugRequestModel
    {
        [Required(ErrorMessage = "藥品代碼必填")]
        [StringLength(50)]
        public string DrugCode { get; set; }

        [Required(ErrorMessage = "藥品名稱必填")]
        [StringLength(100)]
        public string DrugName { get; set; }

        [StringLength(100)]
        public string DrugNameEn { get; set; }

        [Required(ErrorMessage = "藥劑類型必填")]
        [StringLength(30)]
        public string FormType { get; set; }

        [StringLength(20)]
        public string Unit { get; set; }

        public bool IsControlled { get; set; }
    }
}