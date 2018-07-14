using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Model
{
    public class Policy
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
        public Enums.Enums.TypeRisk RiskType { get; set; }
        public CoverageType CoverageType { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

    }
}
