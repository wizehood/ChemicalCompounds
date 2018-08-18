using System;

namespace Junior.SharedModels.DomainModels.Interfaces
{
    public interface ICompound
    {
        Guid Id { get; set; }

        string Name { get; set; }

        Guid TypeId { get; set; }

        bool Deleted { get; set; }

        CompoundType Type { get; set; }
    }
}
