using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailSystem.Models
{
    public class AuditedEntity : Entity
    {
        public AuditedEntity()
        {
            CreationTime = DateTime.Now;
        }

        public DateTime CreationTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public string CreatorId { get; set; }
        public IdentityUser CreatedBy { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public string UpdaterId { get; set; }
        public IdentityUser UpdatedBy { get; set; }
    }
}