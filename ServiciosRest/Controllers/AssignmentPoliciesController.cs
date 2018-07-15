using Common.Data;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiciosRest.Controllers
{
    public class AssignmentPoliciesController : ApiController
    {
        private IGenericRepository<AssignmentPolicy> _assignmentPolicyRepo;
        public AssignmentPoliciesController()
        {         
            this._assignmentPolicyRepo = new GenericRepository<AssignmentPolicy>(new PolicyDbContext());

        }
        // GET: api/AssignmentPolicies   
      
        public HttpResponseMessage AssignmentPolicies(AssignmentPolicy assignmentPolicy)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _assignmentPolicyRepo.Add(assignmentPolicy);
                    _assignmentPolicyRepo.Save();

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, assignmentPolicy);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = assignmentPolicy.Id }));
                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
          
        }
     
    }
}
