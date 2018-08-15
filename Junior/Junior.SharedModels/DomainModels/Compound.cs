using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Junior.SharedModels.DomainModels
{
    public class Compound
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid TypeId { get; set; }

        public bool Deleted { get; set; }

        [ForeignKey(nameof(TypeId))]
        public virtual CompoundType Type { get; set; }
    }
}
