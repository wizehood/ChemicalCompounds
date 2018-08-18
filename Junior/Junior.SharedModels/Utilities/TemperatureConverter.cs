using System;
using static Junior.SharedModels.Enums.Enums;

namespace Junior.SharedModels.Utilities
{
    public static class TemperatureConverter
    {
        //Calculate boiling points, reference: https://www.thoughtco.com/temperature-conversion-formulas-609324
        public static double Convert(double temperatureK, TemperatureType temperatureType)
        {
            switch (temperatureType)
            {
                case TemperatureType.Celsius:
                    return Math.Round(temperatureK - 273.15, 3);
                case TemperatureType.Fahrenheit:
                    return Math.Round((temperatureK - 273.15) * (9d / 5) + 32, 3);
                default:
                    return 0d;
            }
        }
    }
}