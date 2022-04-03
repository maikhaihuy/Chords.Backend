namespace Chords.CoreLib.DataAccess.Models
{
    public interface ISoftDeletedFields
    {
        public bool IsDeleted { get; set; }
    }
}