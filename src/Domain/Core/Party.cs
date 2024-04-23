using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtterlyComplete.Domain.Common;

namespace UtterlyComplete.Domain.Core
{
    public class Party : Entity
    {
        public ICollection<ContactMechanism> ContactMechanisms { get; } = [];
    }
}
