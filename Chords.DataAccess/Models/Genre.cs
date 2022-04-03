using System;
using System.Collections;
using System.Collections.Generic;
using Chords.CoreLib.DataAccess.Models;

namespace Chords.DataAccess.Models
{
    public class Genre : IBaseModel, IAuditFields, ISoftDeletedFields
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        
        public string Name { get; set; }
        public int Total { get; set; }
        public string Description { get; set; }
        
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        
        public ICollection<Song> Songs { get; set; }
    }
}