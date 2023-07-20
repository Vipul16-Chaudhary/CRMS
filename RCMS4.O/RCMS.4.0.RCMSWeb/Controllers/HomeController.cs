using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RCMS._4._0.RCMSWeb.Models;
using RCMS._4.O.API.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using RCMS._4.O.Entities.RCMSEntities;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using NPOI.SS.Formula.Functions;
using RCMS._4.O.Common;
using RCMS._4.O.Core.Componet.RCMSProcComponet;
using System.Threading;

namespace RCMS._4._0.RCMSWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            ResponseInfo<List<StudentEntity>> response = new ResponseInfo<List<StudentEntity>>();
            string baseUrl = _configuration["LocalUrl:key"];
            string webMethodApi = "RCMS/GetStudentDetails";
            string key = string.Empty;
            string token = string.Empty;
            response = await WebApiResponseFunction.GetDataResponseFromWebAPI<List<StudentEntity>>(baseUrl, webMethodApi, key, token);
            return View(response.Result);
        }

        public async Task<JsonResult> IndexJson()
        {
            ResponseInfo<List<StudentEntity>> response = new ResponseInfo<List<StudentEntity>>();
            string baseUrl = _configuration["LocalUrl:key"];
            string webMethodApi = "RCMS/GetStudentDetails";
            string key = string.Empty;
            string token = string.Empty;
            response = await WebApiResponseFunction.GetDataResponseFromWebAPI<List<StudentEntity>>(baseUrl, webMethodApi, key, token);
            return Json(response.Result);
        }

        public IActionResult Add()
        {
            StudentEntity student = new StudentEntity();
            return View(student);
        }

        //AddOrEdit Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int? employeeId, StudentVM student)
        {
            ResponseInfo<StudentEntity> response = new ResponseInfo<StudentEntity>();
            StudentEntity objstudent = new StudentEntity();
            objstudent = new StudentEntity()
            {
                Name = student.Name,
                Address = student.Address,
                City = student.City,
                Region = student.Region,
                PostalCode = student.PostalCode,
                Country = student.Country,
                Phone = student.Phone,
                Email = student.Email,
            };
            string baseUrl = _configuration["LocalUrl:key"];
            string webMethodApi = "RCMS/AddEmployee";
            string key = string.Empty;
            string token = string.Empty;
            response = await WebApiResponseFunction.PostResponseFromWebAPI<StudentEntity>(baseUrl, webMethodApi, key, token, objstudent);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
