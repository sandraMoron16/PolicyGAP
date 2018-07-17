using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiciosRest.Views.Policies
{
    public class PolicyView
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime PolicyStartDate { get; set; }
        [Required]
        public int CoverageTime { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public Enums.TypeRisk RiskType { get; set; }

        public int CoverageType { get; set; }
    }
}