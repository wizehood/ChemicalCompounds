using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalCompounds.SharedModels.DtoModels
{
    public class TemperatureDifferenceDto
    {
        public Guid CompoundAId { get; set; }

        public Guid CompoundBId { get; set; }

        public double Value { get; set; }
    }
}
