using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Junior.SharedModels.DtoModels
{
    public class CompoundElementDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid TypeId { get; set; }
        public List<ElementDto> Elements { get; set; }
    }
}
