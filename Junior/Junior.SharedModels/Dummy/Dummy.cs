using Junior.SharedModels.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Junior.SharedModels.Dummy
{
    public static class Dummy
    {
        public static List<CompoundType> CompoundTypes { get; set; } = new List<CompoundType>
        {
            new CompoundType()
            {
                Name = "Covalent"
            },
            new CompoundType()
            {
                Name = "Ionic"
            },
            new CompoundType()
            {
                Name = "Organic"
            },
            new CompoundType()
            {
                Name = "Inorganic"
            }
        };

        //Reference to: https://www.cs.colorado.edu/~kena/classes/7818/f01/assignments/pt.html
        public static List<Element> Elements { get; set; } = new List<Element>
        {
            new Element()
            {
                Name = "Hydrogen",
                BoilingTemperatureK = 20.28
                
            },
            new Element()
            {
                Name = "Nitrogen",
                BoilingTemperatureK = 77.344
            },
            new Element()
            {
                Name = "Carbon",
                BoilingTemperatureK = 5100
            },
            new Element()
            {
                Name = "Sodium",
                BoilingTemperatureK = 1156
            },
            new Element()
            {
                Name = "Oxygen",
                BoilingTemperatureK = 90.188
            },
        };
    }
}