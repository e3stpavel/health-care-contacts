using System.ComponentModel.DataAnnotations;
using UtterlyComplete.Domain.Common;

namespace UtterlyComplete.Domain.Core
{
    public class FacilityType : Entity
    {
        [Required]
        public string Description { get; set; } = null!;

        public virtual ICollection<Facility> Facilities { get; } = [];
    }
}
