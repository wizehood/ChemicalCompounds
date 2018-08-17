using Junior.SharedModels.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using static Junior.SharedModels.Enums.Enums;

namespace Junior.Web.Utility
{
    public static class CompoundCalculator
    {
        public static double GetBoilingTemperature(List<CompoundElement> compoundElements, TemperatureType temperatureType)
        {
            //Flatten elements with same id and groupsum their temperatures
            var elementTemperatures = compoundElements.GroupBy(d => d.Element.Id)
                .Select(g => new
                {
                    Key = g.Key,
                    Value = g.Sum(s => s.Element.BoilingTemperatureK * s.ElementQuantity)
                });

            int distinctElementCount = elementTemperatures.Count();

            //Calculate the product
            double temperatureProduct = 1d;
            foreach (var temperature in elementTemperatures)
            {
                temperatureProduct *= temperature.Value;
            }

            //N-th sqrt is actually a number to the power of (1/N)
            double boilingTemperature = Math.Round(Math.Pow(temperatureProduct, 1.0 / distinctElementCount), 3);

            //Convert temperature if needed
            if (temperatureType != TemperatureType.Kelvin)
            {
                boilingTemperature = TemperatureConverter.Convert(boilingTemperature, temperatureType);
            }

            return boilingTemperature;
        }
    }
}