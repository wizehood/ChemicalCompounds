using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Junior.SharedModels.DtoModels
{
    public class ElementDto
    {
        public Guid Id { get; set; }
        public Guid ElementId { get; set; }
        public int ElementQuantity { get; set; }
    }
}