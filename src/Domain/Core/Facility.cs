using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UtterlyComplete.Domain.Common;

namespace UtterlyComplete.Domain.Core
{
    public abstract class Facility : Entity
    {
        [Required]
        public string Description { get; set; } = null!;

        [Column("Discriminator")]
        public string Type { get; private set; } = null!;

        public int? SquareFootage { get; set; }

        public int? PartOfFacilityId { get; set; }

        public virtual Facility? PartOfFacility { get; set; }

        public virtual ICollection<Facility> ConsistsOfFacilities { get; } = [];

        public virtual ICollection<ContactMechanism> ContactMechanisms { get; } = [];

        public virtual ICollection<Party> Parties { get; } = [];

        public virtual ICollection<PartyFacility> PartyFacilities { get; } = [];

        public int FacilityTypeId { get; set; }

        public virtual FacilityType FacilityType { get; set; } = null!;
    }
}
