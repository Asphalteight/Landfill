namespace Landfill.DataAccess.Models
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }
    }
}
