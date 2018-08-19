using ChemicalCompounds.SharedModels.DomainModels.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemicalCompounds.SharedModels.DomainModels
{
    public class CompoundType : ICompoundType
    {
        public CompoundType()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
