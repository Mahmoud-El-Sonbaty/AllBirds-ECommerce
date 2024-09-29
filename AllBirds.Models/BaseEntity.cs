namespace AllBirds.Models
{
    public class BaseEntity<TId>
    {
        public TId Id { get; set; }
        public DateTime? Created { get; set; }
        public TId? CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public TId? UpdatedBy { get; set; }
        public DateTime? Deleted { get; set; }
        public TId? DeletedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
