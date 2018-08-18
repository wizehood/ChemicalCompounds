using AutoMapper;
using Junior.DataAccessLayer.Repositories;
using Junior.SharedModels.DtoModels;
using Junior.Web.Utility;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Junior.SharedModels.Enums.Enums;

namespace Junior.Web.Controllers
{
    public class CompoundController : Controller
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
            Log.Information("GET Compound/Index triggered");

            return View("Index");
        }

        public ActionResult GetAll()
        {
            Log.Information("GET Compound/GetAll triggered");

            var compounds = _repo.GetAllCompounds();

            //Map to DTO object
            var compoundDtos = Mapper.Map<List<CompoundDto>>(compounds);

            return Json(compoundDtos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            Log.Information("GET Compound/Create triggered");

            PopulateCombos();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompoundElementPartialDto compoundElement)
        {
            Log.Information("POST Compound/Create triggered");

            ModelState.Clear();

            if (ModelState.IsValid)
            {
                bool isSuccess = _repo.CreateCompoundElement(compoundElement);
                if (isSuccess)
                {
                    return Redirect("Index");
                }

                ModelState.AddModelError(string.Empty, "Error saving compound.");
            }

            PopulateCombos();

            return View(compoundElement);
        }

        public ActionResult Delete(Guid id)
        {
            Log.Information("GET Compound/Delete triggered");

            bool isSuccess = _repo.DeleteCompound(id);

            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            Log.Information("GET Compound/Edit triggered");

            var compoundElements = _repo.GetCompoundElementsByCompoundId(id);
            if (compoundElements.Count == 0)
            {
                return View("Error");
            }

            //Map to DTO object
            var compoundElementDto = new CompoundElementPartialDto()
            {
                CompoundId = compoundElements.First().Compound.Id,
                Name = compoundElements.First().Compound.Name,
                TypeId = compoundElements.First().Compound.TypeId,
                Elements = Mapper.Map<List<ElementPartialDto>>(compoundElements)
            };

            PopulateCombos();

            ViewBag.TemperatureTypes = _temperatureTypeList;

            return View(compoundElementDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompoundElementPartialDto compoundElement)
        {
            Log.Information("POST Compound/Edit triggered");

            ModelState.Clear();

            if (ModelState.IsValid)
            {
                bool isSuccess = _repo.UpdateCompoundElement(compoundElement);
                if (isSuccess)
                {
                    return Redirect("/Compound/Index");
                }
                ModelState.AddModelError(string.Empty, "Error saving compound.");
            }

            PopulateCombos();

            return View(compoundElement);
        }

        public ActionResult GetBoilingTemperature(Guid compoundId, TemperatureType type)
        {
            Log.Information("GET Compound/GetBoilingTemperature triggered");

            var compoundElements = _repo.GetCompoundElementsByCompoundId(compoundId);
            double boilingTemperature = CompoundCalculator.GetBoilingTemperature(compoundElements, type);

            return Json(boilingTemperature, JsonRequestBehavior.AllowGet);
        }

        public void PopulateCombos()
        {
            var types = _repo.GetAllCompoundTypes();
            ViewBag.Types = types;

            var names = _repo.GetAllElements();
            ViewBag.ElementNames = names;
        }
    }
}