using System;

namespace Junior.SharedModels.DtoModels
{
    public class ElementDto
    {
        public Guid Id { get; set; }

        public Guid CompoundElementId { get; set; }

        public int Quantity { get; set; }
    }
}