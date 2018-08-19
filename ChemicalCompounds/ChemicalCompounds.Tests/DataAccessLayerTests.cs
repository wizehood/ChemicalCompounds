using AutoMapper;
using ChemicalCompounds.DataAccessLayer.Repositories;
using ChemicalCompounds.SharedModels.DtoModels;
using ChemicalCompounds.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChemicalCompounds.Tests
{
    [TestClass]
    public class DataAccessLayerTests
    {
        [ClassInitialize]
        public static void ClassInitializer(TestContext context)
        {
            Mapper.Reset();
            AutoMapperConfig.Configure();
        }

        [TestMethod]
        public void GetAllCompounds_ReturnsNullForDeletedEntity()
        {
            //Arrange 
            var repo = new CompoundRepository();

            //Act
            var compound = repo.GetAllCompounds().First();
            repo.DeleteCompound(compound.Id);
            var result = repo.GetAllCompounds()
                .SingleOrDefault(c => c.Id.Equals(compound.Id));

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void UpdateCompoundElement_ElementQuantitiesSuccessfullyUpdated()
        {
            //Arrange 
            var repo = new CompoundRepository();

            //Act
            var compound = repo.GetAllCompounds()
                .First();
            var compoundElements = repo.GetCompoundElementsByCompoundId(compound.Id);
            var compoundElementDto = new CompoundElementPartialDto()
            {
                CompoundId = compoundElements.First().Compound.Id,
                Name = compoundElements.First().Compound.Name,
                TypeId = compoundElements.First().Compound.TypeId,
                Elements = Mapper.Map<List<ElementPartialDto>>(compoundElements)
            };
            var elementQuantities = compoundElementDto.Elements
                .Select(e => e.Quantity)
                .ToList();

            var random = new Random();
            var expectedQuantities = new List<int>();
            foreach (var element in compoundElementDto.Elements)
            {
                var randomNumber = random.Next(100, 500);
                expectedQuantities.Add(randomNumber);
                element.Quantity = randomNumber;
            }

            repo.UpdateCompoundElement(compoundElementDto);
            var newCompoundElements = repo.GetCompoundElementsByCompoundId(compound.Id);
            var newElements = Mapper.Map<List<ElementPartialDto>>(newCompoundElements);
            var newQuantities = newElements
                .Select(e => e.Quantity)
                .ToList();

            //Assert
            Assert.AreEqual(expectedQuantities.Count, newQuantities.Count);
            Assert.IsTrue(expectedQuantities.All(newQuantities.Contains));
        }
    }
}
