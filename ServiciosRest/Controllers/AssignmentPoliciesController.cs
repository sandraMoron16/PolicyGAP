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

        [HttpGet]
        public AssignmentPolicy GetByPolicyId(int id)
        {
            AssignmentPolicy assignmentPolicy = _assignmentPolicyRepo.Find(x => x.PolicyId == id).AsQueryable().FirstOrDefault();
            if (assignmentPolicy == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return assignmentPolicy;
        }
        // GET: api/AssignmentPolicies   
      [HttpPost]
        public HttpResponseMessage Save(AssignmentPolicy assignmentPolicy)
        {
            string mensaje = string.Empty;
            try
            {
                if (assignmentPolicy.PolicyId > 0)
                {
                    Policy policy = _policyRepo.Find(x => x.Id == assignmentPolicy.PolicyId).AsQueryable().FirstOrDefault();
                    if (policy != null)
                    {
                        if (policy.RiskType == Enums.TypeRisk.High && assignmentPolicy.PercentCoverage > 50 && assignmentPolicy.State == Enums.State.Assign)
                        {
                            mensaje = "El porcentaje no puede ser mayor al 50%";
                        }
                        assignmentPolicy.Id = 0;
                    }
                }  
                
                if(mensaje == string.Empty)
                { 
                                    
                        _assignmentPolicyRepo.Add(assignmentPolicy);
                        _assignmentPolicyRepo.Save();

                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, assignmentPolicy);                        
                        return response;
                    
                }
                else
                {
                    HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
                    return response;
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
          
        }
     
    }
}
