using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtterlyComplete.Domain.Entities.Core
{
    public class Party
    {
        public int Id { get; set; }

        public ICollection<ContactMechanism> ContactMechanisms { get; } = [];
    }
}
