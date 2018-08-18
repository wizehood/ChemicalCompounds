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
                //Flatten compound elements and groupsum their temperatures
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
                //Flatten compound elements and groupsum their temperatures
                var elementTemperatures = compoundElements.GroupBy(d => new { d.Element.Id, d.CompoundId })
                    .Select(g => new
                    {
                        Key = g.Key.CompoundId,
                        Value = g.Sum(s => s.Element.BoilingTemperatureK * s.ElementQuantity)
                    });

                var compoundIds = elementTemperatures.Select(x => x.Key)
                    .Distinct()
                    .ToList();

                var compoundTemperatures = new Dictionary<Guid, double>();

                //For each id get number of distinct elements in list
                //Then get the temperatures
                //Finally, calculate boiling temperature
                foreach (var id in compoundIds)
                {
                    var elementCount = elementTemperatures.Where(ce => ce.Key == id)
                        .Count();

                    var distinctElementTemperatures = elementTemperatures.Where(t => t.Key == id)
                        .Select(t => t.Value)
                        .ToList();

                    double temperatureProduct = 1d;
                    foreach (var temperature in distinctElementTemperatures)
                    {
                        temperatureProduct *= temperature;
                    }

                    double boilingTemperature = Math.Round(Math.Pow(temperatureProduct, 1.0 / elementCount), 3);

                    compoundTemperatures.Add(id, boilingTemperature);
                }

                //Calculate temperature differences 
                var temperatureDifferences = new List<TemperatureDifferenceDto>();
                for (int i = 0; i < compoundTemperatures.Count; i++)
                {
                    for (int j = i; j < compoundTemperatures.Count - 1; j++)
                    {
                        temperatureDifferences.Add(new TemperatureDifferenceDto()
                        {
                            CompoundAId = compoundTemperatures.ElementAt(i).Key,
                            CompoundBId = compoundTemperatures.ElementAt(j + 1).Key,
                            Value = Math.Round((Math.Abs(compoundTemperatures.ElementAt(i).Value - compoundTemperatures.ElementAt(j + 1).Value)), 3)
                        });
                    }
                }

                var minDifference = temperatureDifferences.OrderBy(d=>d.Value)
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