using UtterlyComplete.ApplicationCore.Models.Common;

namespace UtterlyComplete.ApplicationCore.Models
{
    public abstract record ContactMechanismDto(int Id) : EntityDto(Id);
}
