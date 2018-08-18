using AutoMapper;
using Junior.SharedModels.DtoModels;
using Junior.Web;
using Junior.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;

namespace Junior.Tests
{
    [TestClass]
    public class CompoundControllerTests
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
            var compoundId = new Guid("E2095EF6-4EA2-E811-B3B1-70F3959B760A");

            //Act
            var result = controller.Edit(compoundId) as ViewResult;
            var model = (CompoundElementPartialDto)result.Model;

            //Assert
            Assert.IsNotNull(model);
            Assert.IsTrue(model.Elements.Count > 0);
        }
    }
}
