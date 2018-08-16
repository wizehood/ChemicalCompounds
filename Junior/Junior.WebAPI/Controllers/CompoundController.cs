using AutoMapper;
using Junior.DataAccessLayer.Repositories;
using Junior.SharedModels.DtoModels;
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
        private CompoundRepository repo = new CompoundRepository();

        /// <summary>  
        /// Get all compounds  
        /// </summary>  
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var compounds = repo.GetAllCompounds();
            var compoundDtos= Mapper.Map<List<CompoundDto>>(compounds);

            return Ok(compoundDtos);
        }

        /// <summary>  
        /// Get compounds filtered by type id  
        /// </summary>  
        /// <param name="id">Type id</param>  
        [HttpGet]
        public IHttpActionResult GetByTypeId(Guid id)
        {
            var compounds = repo.GetAllCompoundsByTypeId(id);
            var compoundDtos = Mapper.Map<List<CompoundDto>>(compounds);

            return Ok(compoundDtos);
        }

        /// <summary>  
        /// Gets all compound types - an example method
        /// </summary>  
        [HttpGet]
        public IHttpActionResult GetTypes()
        {
            var types = repo.GetAllCompoundTypes();

            return Ok(types);
        }
    }
}
