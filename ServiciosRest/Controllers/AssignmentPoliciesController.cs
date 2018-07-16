using Common.Data;
using Common.Enums;
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
        private IGenericRepository<Policy> _policyRepo;
        public AssignmentPoliciesController()
        {         
            this._assignmentPolicyRepo = new GenericRepository<AssignmentPolicy>(new Common.Data.PolicyDbContext());
            this._policyRepo = new GenericRepository<Policy>(new Common.Data.PolicyDbContext());

        }
        // GET: api/AssignmentPolicies   
      
        public HttpResponseMessage AssignmentPolicies(AssignmentPolicy assignmentPolicy)
        {
            try
            {
                if (assignmentPolicy.PolicyId > 0)
                {
                    Policy policy = _policyRepo.Find(x => x.Id == assignmentPolicy.PolicyId).AsQueryable().FirstOrDefault();
                    if (policy != null)
                    {
                        if (policy.RiskType == Enums.TypeRisk.High && assignmentPolicy.PercentCoverage > 50)
                        {
                            ModelState.AddModelError("", "El porcentaje no puede ser mayor al 50%");
                        }
                    }
                }
                
                
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
