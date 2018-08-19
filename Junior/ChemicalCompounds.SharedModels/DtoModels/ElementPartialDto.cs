using System;

namespace ChemicalCompounds.SharedModels.DtoModels
{
    public class ElementPartialDto
    {
        public Guid Id { get; set; }

        public Guid CompoundElementId { get; set; }

        public int Quantity { get; set; }
    }
}