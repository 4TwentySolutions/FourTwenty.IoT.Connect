using System.ComponentModel.DataAnnotations.Schema;

namespace FourTwenty.IoT.Connect.Entities
{
    public class BaseEntity<T>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T Id { get; set; }
    }
}
