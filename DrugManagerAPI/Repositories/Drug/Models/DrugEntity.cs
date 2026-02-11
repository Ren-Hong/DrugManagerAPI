using System;

namespace DrugManager.Repositories.Drug.Models
{
    public class DrugEntity
    {
        public int DrugId { get; set; }

        public string DrugCode { get; set; }

        public string DrugName { get; set; }

        public string DrugNameEn { get; set; }

        public string FormType { get; set; }

        public string Unit { get; set; }

        public bool IsControlled { get; set; }
    }
}