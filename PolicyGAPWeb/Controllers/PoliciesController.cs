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
using PolicyGAPWeb.Models;

namespace PolicyGAPWeb.Controllers
{
    public class PoliciesController : Controller
    {

        HttpClient client;
        string url = "http://localhost:62108/api/Policies/";
        string urlAssigment = "http://localhost:62108/api/AssignmentPolicies/";
        string urlCoverageType = "http://localhost:62108/api/Listas/GetCoverageType/";
        public PoliciesController()

        {
            client = new HttpClient();

            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #region CRUD_POLICY
        // GET: Policies
        [Authorize]
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "GetAll");

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
          
            HttpResponseMessage responseMessage = await client.GetAsync(urlCoverageType);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                listCoverageType = JsonConvert.DeserializeObject<List<CoverageType>>(responseData);
                model.ListCoverageType =  new SelectList(listCoverageType, "Id", "Name");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
            if (policy!= null && ModelState.IsValid)
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
                    var responseData = responseMessage.Content.ReadAsAsync<System.Web.Http.HttpError>();
                    var a = JsonConvert.DeserializeObject<object>(responseData.Result.LastOrDefault().Value.ToString());
                    ModelState.AddModelError("", a.ToString());
                   
                    List<CoverageType> listCoverageType = new List<CoverageType>();
        
                    HttpResponseMessage responseMessage1 = await client.GetAsync(urlCoverageType);
                    if (responseMessage1.IsSuccessStatusCode)
                    {
                        var responseData1 = responseMessage1.Content.ReadAsStringAsync().Result;
                        listCoverageType = JsonConvert.DeserializeObject<List<CoverageType>>(responseData1);
                        policy.ListCoverageType =  new SelectList(listCoverageType, "Id", "Name");
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    return View(policy);
                }
            }

          
            List<CoverageType> listCoverageTypeV = new List<CoverageType>();
           
            HttpResponseMessage responseMessageV = await client.GetAsync(urlCoverageType);
            if (responseMessageV.IsSuccessStatusCode)
            {
                var responseDataV = responseMessageV.Content.ReadAsStringAsync().Result;
                listCoverageTypeV = JsonConvert.DeserializeObject<List<CoverageType>>(responseDataV);
                policy.ListCoverageType = new SelectList(listCoverageTypeV, "Id", "Name");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
            HttpResponseMessage responseMessage1 = await client.GetAsync(urlCoverageType);
            if (responseMessage1.IsSuccessStatusCode)
            {
                var responseData = responseMessage1.Content.ReadAsStringAsync().Result;
                listCoverageType = JsonConvert.DeserializeObject<List<CoverageType>>(responseData);
                list = new SelectList(listCoverageType, "Id", "Name");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
        public async Task<ActionResult> Edit(Policy policy)
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

                    List<CoverageType> listCoverageType = new List<CoverageType>();
                    SelectList list = null;                   
                    HttpResponseMessage responseMessage1 = await client.GetAsync(urlCoverageType);
                    if (responseMessage1.IsSuccessStatusCode)
                    {
                        var responseData1 = responseMessage1.Content.ReadAsStringAsync().Result;
                        listCoverageType = JsonConvert.DeserializeObject<List<CoverageType>>(responseData1);
                        list = new SelectList(listCoverageType, "Id", "Name");
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    model.ListCoverageType = list;
                    return View(model);
                }
            }


            List<CoverageType> listCoverageType1 = new List<CoverageType>();           
            HttpResponseMessage responseMessage2 = await client.GetAsync(urlCoverageType);
            if (responseMessage2.IsSuccessStatusCode)
            {
                var responseData1 = responseMessage2.Content.ReadAsStringAsync().Result;
                listCoverageType1 = JsonConvert.DeserializeObject<List<CoverageType>>(responseData1);
                policy.ListCoverageType = new SelectList(listCoverageType1, "Id", "Name");                
                return View(policy);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


        }

        // GET: Policies/Delete/5
        [Authorize]

        public async Task<ActionResult> Delete(int? id)
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
        #endregion
        #region AssignmentPolicy
        [Authorize]
        public async Task<ActionResult> AssignmentPolicy(int id)
        {
            AssignmentPolicy assignmentPolicy = new Common.Model.AssignmentPolicy();
            List<Client> listClient = new List<Client>();
            var url1 = "http://localhost:62108/api/Listas/GetClient/";
            HttpResponseMessage responseMessage1 = await client.GetAsync(url1);
            if (responseMessage1.IsSuccessStatusCode)
            {
                var responseData1 = responseMessage1.Content.ReadAsStringAsync().Result;
                listClient = JsonConvert.DeserializeObject<List<Client>>(responseData1);
               
                HttpResponseMessage responseMessageAP = await client.GetAsync(urlAssigment+ "GetByPolicyId/" + id);

                if (responseMessageAP.IsSuccessStatusCode)
                {
                    var responseDataAP = responseMessageAP.Content.ReadAsStringAsync().Result;
                    assignmentPolicy = JsonConvert.DeserializeObject<AssignmentPolicy>(responseDataAP);
                  
                }
                assignmentPolicy.ListClients = new SelectList(listClient, "Id", "Name");
                assignmentPolicy.PolicyId = id;
                assignmentPolicy.DateAssigmentPolicy = DateTime.Now;

                return View(assignmentPolicy);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AssignmentPolicy(AssignmentPolicy assignmentPolicy)
        {

            if (ModelState.IsValid)
            {
                
                var data = JsonConvert.SerializeObject(assignmentPolicy);
                var contentPost = new StringContent(data, Encoding.UTF8, "application/json");
              

                HttpResponseMessage responseMessage = await client.PostAsync(urlAssigment +"Save", contentPost);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var responseData = responseMessage.Content.ReadAsAsync<System.Web.Http.HttpError>();
                    //var a = JsonConvert.DeserializeObject<object>(responseData.Result.LastOrDefault().Value.ToString());
                    ModelState.AddModelError("", responseData.Result.LastOrDefault().Value.ToString());
                    assignmentPolicy.Id = 0;

                    List<Client> listClient = new List<Client>();
                    var url1 = "http://localhost:62108/api/Listas/GetClient/";
                    HttpResponseMessage responseMessage1 = await client.GetAsync(url1);
                    if (responseMessage1.IsSuccessStatusCode)
                    {
                        var responseData1 = responseMessage1.Content.ReadAsStringAsync().Result;
                        listClient = JsonConvert.DeserializeObject<List<Client>>(responseData1);
                        assignmentPolicy.ListClients = new SelectList(listClient, "Id", "Name");
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    return View(assignmentPolicy);
                }
            }
            else
            {

                List<Client> listClientModel = new List<Client>();
                var urlModel = "http://localhost:62108/api/Listas/GetClient/";
                HttpResponseMessage responseMessageModel = await client.GetAsync(urlModel);
                if (responseMessageModel.IsSuccessStatusCode)
                {
                    var responseDataModel = responseMessageModel.Content.ReadAsStringAsync().Result;
                    listClientModel = JsonConvert.DeserializeObject<List<Client>>(responseDataModel);
                    assignmentPolicy.ListClients = new SelectList(listClientModel, "Id", "Name");
                    return View(assignmentPolicy);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

        }
        #endregion
        //private async Task<SelectList>  GetListCoverageType()
        //{
        //    List<CoverageType> listCoverageType = new List<CoverageType>();
        //    SelectList list= null;
        //    var url1 = "http://localhost:62108/api/Listas/GetCoverageType/";
        //    HttpResponseMessage responseMessage1 = await client.GetAsync(url1);
        //    if (responseMessage1.IsSuccessStatusCode)
        //    {
        //        var responseData1 = responseMessage1.Content.ReadAsStringAsync().Result;
        //        listCoverageType= JsonConvert.DeserializeObject<List<CoverageType>>(responseData1);
        //       return list=  new SelectList(listCoverageType, "Id", "Name");

        //    }
        //    return list;
        //}
    }
}
