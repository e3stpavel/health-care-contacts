using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtterlyComplete.Domain.Entities.Core
{
    public class Facility : Entity
    {
        [Required]
        public string Description { get; set; } = null!;

        public int? PartOfFacilityId { get; set; }

        public Facility? PartOfFacility { get; set; }

        public ICollection<Facility> ConsistsOfFacilities { get; } = [];

        public ICollection<ContactMechanism> ContactMechanisms { get; } = [];

        public ICollection<Party> Parties { get; } = [];

        public int FacilityTypeId { get; set; }

        public FacilityType FacilityType { get; set; } = null!;
    }
}
