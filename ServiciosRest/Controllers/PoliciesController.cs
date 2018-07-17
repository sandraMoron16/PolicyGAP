using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Common.Model;
using Common.Data;
using System.Data.Entity.Infrastructure;
using System.Net.Http;

namespace ServiciosRest.Controllers
{

    public class PoliciesController : ApiController
    {

        private IGenericRepository<Policy> _policyRepo;
    

        public PoliciesController()
        {
            this._policyRepo = new GenericRepository<Policy>(new Common.Data.PolicyDbContext());
     
        }
        public PoliciesController(IGenericRepository<Policy> policyRepo, IGenericRepository<CoverageType> coverateRepo)
        {
            this._policyRepo = policyRepo;         
        }

      

        // GET api/policies
        [Route("api/Policies/GetAll/")]
        public IEnumerable<Policy> GetAll()
        {
            return _policyRepo.GetAll();
        }

        // GET api/Policies/5
        [HttpGet]
        public Policy Get(int id)
        {
            Policy policy = _policyRepo.Find(x => x.Id == id).AsQueryable().FirstOrDefault();
            if (policy == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return policy;
        }

        // POST api/Policies
        [Route("api/Policies/Save/")]
        [HttpPost]
        public HttpResponseMessage Save(Policy policy)
        {           

            if (ModelState.IsValid)
            {                             
                _policyRepo.Add(policy);
                _policyRepo.Save();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, policy);                
                return response;
            }
            else
            {
               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // PUT api/Policies/5
        [Route("api/Policies/Edit/")]     
        [HttpPut]
        public HttpResponseMessage Edit(Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
                 
            try
            {
                _policyRepo.Edit(policy);
                _policyRepo.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/Policies/5
     
        [HttpDelete]     
        public HttpResponseMessage Delete(int id)
        {
            Policy policy = _policyRepo.Find(x => x.Id == id).AsQueryable().FirstOrDefault();
            if (policy == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                _policyRepo.Delete(policy);
                _policyRepo.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

       

    }
}
