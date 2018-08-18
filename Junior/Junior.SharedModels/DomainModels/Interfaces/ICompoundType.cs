using System;

namespace Junior.SharedModels.DomainModels.Interfaces
{
    public interface ICompoundType
    {
        Guid Id { get; set; }

        string Name { get; set; }
    }
}
