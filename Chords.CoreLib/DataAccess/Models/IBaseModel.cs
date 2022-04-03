using System.ComponentModel.DataAnnotations.Schema;

namespace Chords.CoreLib.DataAccess.Models
{
    public interface IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
    }
}