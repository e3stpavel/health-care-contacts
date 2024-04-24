namespace UtterlyComplete.Domain.Core
{
    public class PartyContactMechanism
    {
        public int PartyId { get; set; }

        public int ContactMechanismId { get; set; }
        
        public DateTime FromDate { get; set; }
        
        public DateTime? ThruDate { get; set; }
        
        public bool? NonSolicitationIndicator { get; set; }
        
        public string? Extension { get; set; }
        
        public string? Comment { get; set; }

    }
}
