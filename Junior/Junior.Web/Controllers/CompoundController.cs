using AutoMapper;
using Junior.DataAccessLayer.Repositories;
using Junior.SharedModels.DtoModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Junior.Web.Controllers
{
    public class CompoundController : Controller
    {
        private CompoundRepository repo = new CompoundRepository();

        public ActionResult Index()
        {
            Log.Information("GET Compound/Index triggered");

            return View();
        }

        public ActionResult GetAllCompounds()
        {
            Log.Information("GET Compound/GetAllCompounds triggered");

            var compounds = repo.GetAllCompounds();

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
        public ActionResult Create(CompoundElementDto compoundElement)
        {
            Log.Information("POST Compound/Create triggered");

            ModelState.Clear();

            if (ModelState.IsValid)
            {
                bool isSuccess = repo.CreateCompoundElement(compoundElement);
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

            bool isSuccess = repo.DeleteCompound(id);

            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            Log.Information("GET Compound/Edit triggered");

            var compoundElements = repo.GetCompoundElementsByCompoundId(id);
            if (compoundElements.Count == 0)
            {
                return View("Error");
            }

            //Map to DTO object
            var compoundElementDto = new CompoundElementDto()
            {
                CompoundId = compoundElements.First().Compound.Id,
                Name = compoundElements.First().Compound.Name,
                TypeId = compoundElements.First().Compound.TypeId,
                Elements = Mapper.Map<List<ElementDto>>(compoundElements)
            };

            PopulateCombos();

            return View(compoundElementDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompoundElementDto compoundElement)
        {
            Log.Information("POST Compound/Edit triggered");

            ModelState.Clear();

            if (ModelState.IsValid)
            {
                bool isSuccess = repo.UpdateCompoundElement(compoundElement);
                if (isSuccess)
                {
                    return Redirect("/Compound/Index");
                }
                ModelState.AddModelError(string.Empty, "Error saving compound.");
            }

            PopulateCombos();

            return View(compoundElement);
        }



        public void PopulateCombos()
        {
            var types = repo.GetAllCompoundTypes();
            ViewBag.Types = types;

            var names = repo.GetAllElements();
            ViewBag.ElementNames = names;
        }
    }
}