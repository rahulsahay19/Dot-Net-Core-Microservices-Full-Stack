namespace Ordering.Entities
{
    public abstract class EntityBase
    {
        //protected set is made to use in the derived class
        public int Id { get; protected set; }
        //Few audit props
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }

}
