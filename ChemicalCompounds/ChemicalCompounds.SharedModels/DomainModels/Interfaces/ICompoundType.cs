using System;

namespace ChemicalCompounds.SharedModels.DomainModels.Interfaces
{
    public interface ICompoundType
    {
        Guid Id { get; set; }

        string Name { get; set; }
    }
}
