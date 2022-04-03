using System;
using Chords.CoreLib.DataAccess.Models;

namespace Chords.DataAccess.Models
{
    public class Token : IBaseModel, IAuditFields
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string UserId { get; set; }
        public string UserAgent { get; set; }
        public Account Account { get; set; }
    }
}