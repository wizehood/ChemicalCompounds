using Junior.SharedModels.DomainModels.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Junior.SharedModels.DomainModels
{
    public class Element : IElement
    {
        public Element()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double BoilingTemperatureK { get; set; }
    }
}
