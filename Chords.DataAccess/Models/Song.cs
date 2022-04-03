using System;
using System.Collections.Generic;
using Chords.CoreLib.DataAccess.Models;

namespace Chords.DataAccess.Models
{
    public class Song : IBaseModel, IAuditFields, ISoftDeletedFields
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string Status { get; set; }
        public long Views { get; set; }
        public int Rating { get; set; }
        
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        
        public ICollection<Artist> Authors { get; set; }
        public ICollection<Performance> Performances { get; set; }
        public Genre Genre { get; set; }
    }
}