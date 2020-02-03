using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FourTwenty.Core.Data.Models;

namespace FourTwenty.IoT.Connect.Entities
{
    public class GrowBox : BaseEntity<int>
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public long ConfigVersion { get; set; }
        public virtual ICollection<GrowBoxModule> Modules { get; set; }
    }
}
