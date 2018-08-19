using AutoMapper;
using ChemicalCompounds.DataAccessLayer.Repositories;
using ChemicalCompounds.SharedModels.DtoModels;
using ChemicalCompounds.SharedModels.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static ChemicalCompounds.SharedModels.Enums.Enums;

namespace ChemicalCompounds.Web.Controllers
{
    public class ElementController : Controller
    {
        private CompoundRepository _repo = new CompoundRepository();

        //Dropdown list
        private static IEnumerable<SelectListItem> _temperatureTypeList = Enum.GetValues(typeof(TemperatureType))
            .Cast<TemperatureType>()
            .Select(e => new SelectListItem()
            {
                Text = e == TemperatureType.Kelvin ? "K" : e == TemperatureType.Celsius ? "°C" : "°F",
                Value = ((int)e).ToString()
            });

        public ActionResult Index()
        {
            Log.Information("GET Element/Index triggered");

            ViewBag.TemperatureTypes = _temperatureTypeList;

            return View();
        }

        public ActionResult GetAll(TemperatureType type)
        {
            Log.Information("GET Element/GetAll triggered");

            var elements = _repo.GetAllElements();

            //Map to DTO object
            var elementDtos = Mapper.Map<List<ElementDto>>(elements);

            //Recalculate temperature
            if (type != TemperatureType.Kelvin)
            {
                foreach (var element in elementDtos)
                {
                    element.BoilingTemperature = TemperatureConverter.Convert(element.BoilingTemperature, type);
                }
            }

            return Json(elementDtos, JsonRequestBehavior.AllowGet);
        }
    }
}