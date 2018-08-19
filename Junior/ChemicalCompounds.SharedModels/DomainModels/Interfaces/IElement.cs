using System;

namespace ChemicalCompounds.SharedModels.DomainModels.Interfaces
{
    public interface IElement
    {
        Guid Id { get; set; }

        string Name { get; set; }

        double BoilingTemperatureK { get; set; }
    }
}
