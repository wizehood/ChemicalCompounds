using ChemicalCompounds.SharedModels.DomainModels.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemicalCompounds.SharedModels.DomainModels
{
    public class Compound : ICompound
    {
        public Compound()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid TypeId { get; set; }

        public bool Deleted { get; set; }

        [ForeignKey(nameof(TypeId))]
        public virtual CompoundType Type { get; set; }
    }
}
