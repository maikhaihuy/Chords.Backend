using System;
using System.Collections.Generic;
using Chords.CoreLib.DataAccess.Models;

namespace Chords.DataAccess.Models
{
    public class Performance : IBaseModel, IAuditFields, ISoftDeletedFields
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        
        public string KeyTone { get; set; }
        public ICollection<Artist> Singers { get; set; }
        public string Url { get; set; }
        public string SongId { get; set; }
    }
}