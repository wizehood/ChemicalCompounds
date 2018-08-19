using AutoMapper;
using ChemicalCompounds.SharedModels.DtoModels;
using ChemicalCompounds.Web;
using ChemicalCompounds.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ChemicalCompounds.Tests
{
    [TestClass]
    public class WebTests
    {
        [ClassInitialize]
        public static void ClassInitializer(TestContext context)
        {
            Mapper.Reset();
            AutoMapperConfig.Configure();
        }

        [TestMethod]
        public void CompoundController_ReturnsViewNamedIndex()
        {
            //Arrage 
            var controller = new CompoundController();

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void CompoundControllerGetEdit_ReturnsMappedElementList()
        {
            //Arrange 
            var controller = new CompoundController();

            //Act
            var compoundJson = controller.GetAll() as JsonResult;
            var serializer = new JavaScriptSerializer();

            var compounds = serializer.Deserialize<List<CompoundDto>>(serializer.Serialize(compoundJson.Data));
            var compoundId = compounds.First().Id;

            var result = controller.Edit(compoundId) as ViewResult;
            var model = (CompoundElementPartialDto)result.Model;

            //Assert
            Assert.IsNotNull(model);
            Assert.IsTrue(model.Elements.Count > 0);
        }
    }
}
