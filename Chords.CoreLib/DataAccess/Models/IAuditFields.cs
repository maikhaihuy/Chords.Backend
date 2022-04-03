using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chords.CoreLib.DataAccess.Models
{
    public interface IAuditFields
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}