using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Model
{
    public class AssignmentPolicy
    {
        public int Id { get; set; }
        public Policy Policy { get; set; }
        [Required]
        public int PercentCoverage { get; set; }
        [Required]
        public DateTime DateAssigmentPolicy { get; set; }
        [Required]
        public Enums.Enums.State State { get; set; }

        public Client Client { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
