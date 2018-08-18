using AutoMapper;
using Junior.DataAccessLayer.Repositories;
using Junior.SharedModels.DtoModels;
using Junior.SharedModels.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Junior.WebAPI.Controllers
{
    /// <summary>  
    /// Compound controller  
    /// </summary> 
    public class CompoundController : ApiController
    {
        private CompoundRepository _repo = new CompoundRepository();

        /// <summary>  
        /// Get all compounds  
        /// </summary>  
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            Log.Information("GET Compound/GetAll triggered");

            var compounds = _repo.GetAllCompounds();
            var compoundDtos= Mapper.Map<List<CompoundDto>>(compounds);

            return Ok(compoundDtos);
        }

        /// <summary>  
        /// Get compound by id  
        /// </summary>  
        /// <param name="id">Compound id</param>  
        [HttpGet]
        public IHttpActionResult GetById(Guid id)
        {
            Log.Information("GET Compound/GetById triggered");

            var compounds = _repo.GetCompoundById(id);
            var compoundDto = Mapper.Map<CompoundDto>(compounds);

            return Ok(compoundDto);
        }

        /// <summary>  
        /// Get compounds filtered by type id  
        /// </summary>  
        /// <param name="id">Type id</param>  
        [HttpGet]
        public IHttpActionResult GetByTypeId(Guid id)
        {
            Log.Information("GET Compound/GetByTypeId triggered");

            var compounds = _repo.GetAllCompoundsByTypeId(id);
            var compoundDtos = Mapper.Map<List<CompoundDto>>(compounds);

            return Ok(compoundDtos);
        }

        /// <summary>  
        /// Gets two compounds with the most closest boiling temperature
        /// </summary>  
        [HttpGet]
        public IHttpActionResult GetTemperatureRelatedCompounds()
        {
            Log.Information("GET Compound/GetTemperatureRelatedCompounds triggered");

            var compoundElements = _repo.GetCompoundElements();
            var relatedCompounds = CompoundCalculator.GetTemperatureRelatedCompounds(compoundElements);

            return Ok(relatedCompounds);
        }

        /// <summary>  
        /// Gets all compound types - an example method
        /// </summary>  
        [HttpGet]
        public IHttpActionResult GetTypes()
        {
            Log.Information("GET Compound/GetTypes triggered");

            var types = _repo.GetAllCompoundTypes();

            return Ok(types);
        }
    }
}
