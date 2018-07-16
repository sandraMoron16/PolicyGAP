using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Common.Data;
using Common.Model;
using Newtonsoft.Json;

namespace PolicyGAPWeb.Controllers
{
    public class PoliciesController : Controller
    {
       
        HttpClient client;
        string url = "http://localhost:62108/api/Policies/";
        public PoliciesController()

        {

            client = new HttpClient();

            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        // GET: Policies
            [Authorize]
            public async Task<ActionResult> Index()
            {

            HttpResponseMessage responseMessage = await client.GetAsync(url +"GetAll");

                if (responseMessage.IsSuccessStatusCode)
                {
                     var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var Employees = JsonConvert.DeserializeObject<List<Policy>>(responseData);
                    return View(Employees);
                }
                

            return View("Error");

            }



        [Authorize]
        // GET: Policies/Create
        public async Task<ActionResult> Create()
        {
            Policy model = new Policy();
            List<CoverageType> listCoverageType = new List<CoverageType>();
            SelectList list = null;
            var url1 = "http://localhost:62108/api/Listas/GetCoverageType/";
            HttpResponseMessage responseMessage = await client.GetAsync(url1);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                listCoverageType = JsonConvert.DeserializeObject<List<CoverageType>>(responseData);
                model.ListCoverageType= list = new SelectList(listCoverageType, "Id", "Name");
            }

            model.PolicyStartDate = DateTime.Now;
            return View(model);
        }

        // POST: Policies/Create
   
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Policy policy)
        {
            if (ModelState.IsValid)
            {
                var data = JsonConvert.SerializeObject(policy);
                var contentPost = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(url + "Save/", contentPost);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var model = JsonConvert.DeserializeObject<Policy>(responseData);
                    model.ListCoverageType = GetListCoverageType().Result;
                    return View(model);
                }
            }

            return View(policy);
        }

        // GET: Policies/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Policy model = new Policy();
            List<CoverageType> listCoverageType = new List<CoverageType>();
            SelectList list = null;
            var url1 = "http://localhost:62108/api/Listas/GetCoverageType/";
            HttpResponseMessage responseMessage1 = await client.GetAsync(url1);
            if (responseMessage1.IsSuccessStatusCode)
            {
                var responseData = responseMessage1.Content.ReadAsStringAsync().Result;
                listCoverageType = JsonConvert.DeserializeObject<List<CoverageType>>(responseData);
                list = new SelectList(listCoverageType, "Id", "Name");

            }


            HttpResponseMessage responseMessage = await client.GetAsync(url + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var policy = JsonConvert.DeserializeObject<Policy>(responseData);
                policy.ListCoverageType = list;
                return View(policy);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: Policies/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( Policy policy)
        {
            if (ModelState.IsValid)
            {

                var data = JsonConvert.SerializeObject(policy);
                var contentPost = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PutAsync(url + "Edit/", contentPost);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {

                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var model = JsonConvert.DeserializeObject<Policy>(responseData);
                  
                    model.ListCoverageType = GetListCoverageType().Result;
                    return View(model);
                }
            }

          

            policy.ListCoverageType = GetListCoverageType().Result;
            return View(policy);

        }

        // GET: Policies/Delete/5
        [Authorize]
        
        public async Task<ActionResult>  Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage responseMessage = await client.GetAsync(url + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<Policy>(responseData);               
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }

            
        }

        // POST: Policies/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        

        private async Task<SelectList>  GetListCoverageType()
        {
            List<CoverageType> listCoverageType = new List<CoverageType>();
            SelectList list= null;
            var url1 = "http://localhost:62108/api/Listas/GetCoverageType/";
            HttpResponseMessage responseMessage1 = await client.GetAsync(url1);
            if (responseMessage1.IsSuccessStatusCode)
            {
                var responseData1 = responseMessage1.Content.ReadAsStringAsync().Result;
                listCoverageType= JsonConvert.DeserializeObject<List<CoverageType>>(responseData1);
               return list=  new SelectList(listCoverageType, "Id", "Name");

            }
            return list;
        }
    }
}
