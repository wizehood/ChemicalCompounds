using Junior.DataAccessLayer.Repositories;
using Junior.SharedModels.DomainModels;
using Junior.SharedModels.DtoModels;
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
            return View();
        }

        public ActionResult GetAllCompounds()
        {
            var compounds = repo.GetAllCompounds();

            return Json(compounds, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            PopulateCombos();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompoundElementDto compoundElement)
        {
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

            return View();
        }

        public ActionResult Delete(Guid id)
        {
            bool isSuccess = repo.DeleteCompound(id);

            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            var compoundElements = repo.GetCompoundElementByCompoundId(id);
            if (compoundElements.Count == 0)
            {
                return View("Error");
            }

            //Map to DTO object
            var compoundElement = new CompoundElementDto()
            {
                Id = compoundElements.First().Compound.Id,
                Name = compoundElements.First().Compound.Name,
                TypeId = compoundElements.First().Compound.TypeId,
                Elements = compoundElements.Select(ce => new ElementDto
                {
                    Id = ce.Id,
                    ElementId = ce.ElementId,
                    ElementQuantity = ce.ElementQuantity
                }).ToList()
            };

            PopulateCombos();

            return View(compoundElement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompoundElementDto compoundElement)
        {
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