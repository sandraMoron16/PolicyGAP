using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Model;
using Common.Data;
using System.Data.Entity.Infrastructure;

namespace ServiciosRest.Controllers
{
    public class PoliciesController : ApiController
    {
       
        private IGenericRepository<Policy> _policyRepo;


        public PoliciesController()
        {
            this._policyRepo = new GenericRepository<Policy>(new PolicyDbContext());
          
        }
        public PoliciesController(IGenericRepository<Policy> policyRepo)
        {
            this._policyRepo = policyRepo;
        }

        // GET api/policies
        public IEnumerable<Policy> Get()
        {           
                return _policyRepo.GetAll();
        }

        // GET api/Policies/5
        public Policy Get(int id)
        {
            Policy policy = _policyRepo.Find(x=>x.Id == id).AsQueryable().FirstOrDefault();
            if (policy == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return policy;
        }

        // POST api/Policies
        public HttpResponseMessage Post(Policy policy)
        {
            if (ModelState.IsValid)
            {
                _policyRepo.Add(policy);
                _policyRepo.Save();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, policy);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = policy.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // PUT api/Policies/5
        public HttpResponseMessage Put(int id, Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != policy.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
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
