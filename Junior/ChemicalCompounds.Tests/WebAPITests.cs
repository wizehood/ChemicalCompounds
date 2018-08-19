using AutoMapper;
using ChemicalCompounds.SharedModels.DomainModels;
using ChemicalCompounds.SharedModels.DtoModels;
using ChemicalCompounds.WebAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace ChemicalCompounds.Tests
{
    [TestClass]
    public class WebAPITests
    {
        [ClassInitialize]
        public static void ClassInitializer(TestContext context)
        {
            Mapper.Reset();
            Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<Compound, CompoundDto>();
            });
        }

        [TestMethod]
        public void CompoundControllerGetAll_ReturnsListOfCompoundDtos()
        {
            //Arrange 
            var controller = new CompoundController();

            //Act
            var result = controller.GetAll();

            //Assert
            Assert.AreEqual(typeof(OkNegotiatedContentResult<List<CompoundDto>>), result.GetType());
            Assert.IsTrue(((OkNegotiatedContentResult<List<CompoundDto>>)result).Content.Count > 0);
        }

        [TestMethod]
        public void CompoundControllerGetTypes_ReturnsListOf4CompoundTypes()
        {
            //Arrange 
            var controller = new CompoundController();

            //Act
            var result = controller.GetTypes() as OkNegotiatedContentResult<List<CompoundType>>;

            //Assert
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(4, result.Content.Count);
        }
    }
}
