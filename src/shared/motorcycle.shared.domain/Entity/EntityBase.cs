using motorcycle.shared.CreationalBase;
using System.ComponentModel.DataAnnotations;

namespace motorcycle.shared.domain.Entity
{
    public abstract class EntityBase<T> : BaseObject<T> where T : class
    {
        [Key]
        public Guid Id { get; set; } = Guid.Empty;

        public void SetId(Guid? id = null)
        {
            Id = id is not null ? id.Value : Guid.NewGuid();
        }
    }
}
