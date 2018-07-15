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
        private IGenericRepository<Client> _clientRepo;
        private IGenericRepository<CoverageType> _coverageType;
     
        public ListasController()
        {
            PolicyDbContext policyDbContext = PolicyDbContext.Create();
            this._clientRepo = new GenericRepository<Client>(policyDbContext);
            this._coverageType = new GenericRepository<CoverageType>(policyDbContext);

        }

        // GET: api/Listas
        [Route("api/Listas/GetClient/")]
        public IEnumerable<Client> GetClient()
        {
          
                return _clientRepo.GetAll();
          
        }

        // GET: api/Listas
        [Route("api/Listas/GetCoverageType/")]
        public IEnumerable<CoverageType> GetCoverageType()
        {
            return _coverageType.GetAll();

        }

      
    }
}
