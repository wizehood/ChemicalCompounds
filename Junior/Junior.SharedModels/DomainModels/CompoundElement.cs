using Junior.SharedModels.DomainModels.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Junior.SharedModels.DomainModels
{
    public class CompoundElement : ICompoundElement
    {
        public CompoundElement()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid CompoundId { get; set; }

        public Guid ElementId { get; set; }

        public int ElementQuantity { get; set; }

        [ForeignKey(nameof(CompoundId))]
        public virtual Compound Compound { get; set; }

        [ForeignKey(nameof(ElementId))]
        public virtual Element Element { get; set; }
    }
}
