using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Chords.CoreLib.DataAccess.Models;

namespace Chords.DataAccess.Models
{
    public class Account : IBaseModel, IAuditFields, ISoftDeletedFields
    {
        public Account() : base()
        {
            Tokens = new List<Token>();
        }
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        
        public string Username { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public ICollection<Token> Tokens { get; set; }
    }
}