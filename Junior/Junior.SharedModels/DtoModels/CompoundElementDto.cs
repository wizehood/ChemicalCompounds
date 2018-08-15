using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Junior.SharedModels.DtoModels
{
    public class CompoundElementDto
    {
        public Guid CompoundId { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid TypeId { get; set; }

        public List<ElementDto> Elements { get; set; }
    }
}
