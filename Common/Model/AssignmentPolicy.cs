using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web.Mvc;

namespace Common.Model
{
    public class AssignmentPolicy
    {
        public int Id { get; set; }
        [ForeignKey("PolicyId")]
        public Policy Policy { get; set; }
        public int PolicyId { get; set; }
        [Required]
        public int PercentCoverage { get; set; }
        [Required]
        public DateTime DateAssigmentPolicy { get; set; }
        [Required]
        public Enums.Enums.State State { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public int ClientId { get; set; }

        [NotMapped]
        public virtual SelectList ListClients { get; set; }
        //[ForeignKey("ApplicationUserId")]
        //public ApplicationUser ApplicationUser { get; set; }
        //public string ApplicationUserId { get; set; }
    }
}
