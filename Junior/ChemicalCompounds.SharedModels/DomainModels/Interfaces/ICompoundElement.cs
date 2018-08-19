using System;

namespace ChemicalCompounds.SharedModels.DomainModels.Interfaces
{
    public interface ICompoundElement
    {
        Guid Id { get; set; }

        Guid CompoundId { get; set; }

        Guid ElementId { get; set; }

        int ElementQuantity { get; set; }

        Compound Compound { get; set; }

        Element Element { get; set; }
    }
}
