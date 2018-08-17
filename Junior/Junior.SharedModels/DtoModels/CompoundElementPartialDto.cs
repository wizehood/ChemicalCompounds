using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Junior.SharedModels.DtoModels
{
    public class CompoundElementPartialDto
    {
        public Guid CompoundId { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid TypeId { get; set; }

        public double BoilingTemperature { get; set; }

        public List<ElementPartialDto> Elements { get; set; }
    }
}
