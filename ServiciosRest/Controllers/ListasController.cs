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
    public class ListasController : ApiController
    {
       
        //private IGenericRepository<Client> _clientRepo;
        private IGenericRepository<CoverageType> _coverageTypeRepo;
        public ListasController()
        {
            //this._clientRepo = new GenericRepository<Client>(new Common.Data.PolicyDbContext());
            this._coverageTypeRepo = new GenericRepository<CoverageType>(new Common.Data.PolicyDbContext());
        }

        public ListasController(IGenericRepository<Client> clientRepo, IGenericRepository<CoverageType> coverateRepo)
        {
            //this._clientRepo = clientRepo;
            this._coverageTypeRepo = coverateRepo;
        }


        //// GET: api/Listas
        //[Route("api/Listas/GetClient/")]
        //public IEnumerable<Client> GetClient()
        //{  
        //    return _clientRepo.GetAll();          
        //}

        // GET: api/Listas
        [Route("api/Listas/GetCoverageType/")]
        public IEnumerable<CoverageType> GetCoverageType()
        {            
            return _coverageTypeRepo.GetAll();
        }      
    }
}
