using Junior.SharedModels.DomainModels;
using Junior.SharedModels.DtoModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using static Junior.SharedModels.Enums.Enums;

namespace Junior.SharedModels.Utilities
{
    public static class CompoundCalculator
    {
        public static double GetBoilingTemperature(List<CompoundElement> compoundElements, TemperatureType temperatureType)
        {
            try
            {
                //Calculate the product
                double temperatureProduct = 1d;
                int elementCount = 0;
                foreach (var compoundElement in compoundElements)
                {
                    temperatureProduct *= Math.Pow(compoundElement.Element.BoilingTemperatureK, compoundElement.ElementQuantity);
                    elementCount += compoundElement.ElementQuantity;
                }

                //N-th root is actually a number to the power of (1/N)
                double boilingTemperature = Math.Round(Math.Pow(temperatureProduct, 1.0 / elementCount), 3);

                //Convert temperature if needed
                if (temperatureType != TemperatureType.Kelvin)
                {
                    boilingTemperature = TemperatureConverter.Convert(boilingTemperature, temperatureType);
                }

                return boilingTemperature;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GetBoilingTemperature");
                return 0d;
            }
        }

        public static TemperatureDifferenceDto GetTemperatureRelatedCompounds(List<CompoundElement> compoundElements)
        {
            try
            {
                var compoundIds = compoundElements.Select(x => x.CompoundId)
                    .Distinct()
                    .ToList();

                //For each id get distinct elements in list
                //Then calculate the temperature product
                //Finally, calculate boiling temperature
                var boilingTemperatures = new Dictionary<Guid, double>();
                foreach (var id in compoundIds)
                {
                    double temperatureProduct = 1d;
                    int elementCount = 0;

                    var distinctCompoundElements = compoundElements.Where(ce => ce.CompoundId == id)
                        .ToList();

                    foreach (var compoundElement in distinctCompoundElements)
                    {
                        temperatureProduct *= Math.Pow(compoundElement.Element.BoilingTemperatureK, compoundElement.ElementQuantity);
                        elementCount += compoundElement.ElementQuantity;
                    }

                    double boilingTemperature = Math.Round(Math.Pow(temperatureProduct, 1.0 / elementCount), 3);

                    boilingTemperatures.Add(id, boilingTemperature);
                }

                //Calculate temperature differences 
                var temperatureDifferences = new List<TemperatureDifferenceDto>();
                for (int i = 0; i < boilingTemperatures.Count; i++)
                {
                    for (int j = i; j < boilingTemperatures.Count - 1; j++)
                    {
                        temperatureDifferences.Add(new TemperatureDifferenceDto()
                        {
                            CompoundAId = boilingTemperatures.ElementAt(i).Key,
                            CompoundBId = boilingTemperatures.ElementAt(j + 1).Key,
                            Value = Math.Round((Math.Abs(boilingTemperatures.ElementAt(i).Value - boilingTemperatures.ElementAt(j + 1).Value)), 3)
                        });
                    }
                }

                var minDifference = temperatureDifferences.OrderBy(d => d.Value)
                    .First();

                return minDifference;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GetTemperatureRelatedCompound");
                return null;
            }
        }
    }
}