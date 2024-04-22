using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtterlyComplete.Domain.Entities.Core
{
    public class FacilityType : Entity
    {
        [Required]
        public string Description { get; set; } = null!;

        public ICollection<Facility> Facilities { get; } = [];
    }
}
