using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using RCMS._4.O.Common;
using RCMS._4.O.Entities.RCMSEntities;
using RCMS._4.O.Interfaces.RCMSInterface;
using RCMS._4.O.Utilities.JWTToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RCMS._4.O.API.Controllers
{

    //[JWTCustomAuthorization]
    [Route("api/[controller]")]
    [ApiController]
    public class RCMSController : ControllerBase
    {
        private readonly IRCMSInterface _IRCMSInterface;
        public RCMSController(IRCMSInterface IRCMSInterface)
        {
            _IRCMSInterface = IRCMSInterface;
        }

        [HttpGet("GetPublisher")]
        public async Task<ActionResult<IEnumerable<string>>> GetAll()
        {
            var result = await _IRCMSInterface.GetAll();
            return Ok(result);
        }

        [HttpGet("GetStudentDetails")]
        public async Task<ActionResult<List<StudentEntity>>> GetStudentDetails()
        {
            ResponseInfo<List<StudentEntity>> responseInfo = new Common.ResponseInfo<List<StudentEntity>>();
            try
            {
                responseInfo.Result = await _IRCMSInterface.GetStudentDetails();

                if (responseInfo.Result != null)
                {
                    responseInfo.ResultType = Enums.ResultType.Success;
                    responseInfo.ResultCode = Constant.SUCCESS;
                    responseInfo.ResultDesc = "Success";
                }
                else
                {
                    responseInfo = new ResponseInfo<List<StudentEntity>>()
                    {
                        ResultType = Enums.ResultType.Failed,
                        ResultCode = Constant.FAILED,
                        ResultDesc = "Failed"
                    };
                }
            }
            catch (Exception ex)
            {
                responseInfo = new ResponseInfo<List<StudentEntity>>()
                {
                    ResultType = Enums.ResultType.Exception,
                    ResultCode = Constant.EXCEPTION,
                    ResultDesc = ex.Message.ToString()
                };
            }
            return Ok(responseInfo);
        }

        [HttpPost("AddEmployee")]
        public async Task<ActionResult> AddEmployee(StudentEntity studentEntity)
        {
            ResponseInfo<bool> responseInfo = new Common.ResponseInfo<bool>();
            try
            {
                responseInfo.Result = await _IRCMSInterface.AddEmployee(studentEntity);
                if (responseInfo.Result == true)
                {
                    responseInfo.ResultType = Enums.ResultType.Success;
                    responseInfo.ResultCode = Constant.SUCCESS;
                    responseInfo.ResultDesc = "Success";
                }
                else
                {
                    responseInfo = new ResponseInfo<bool>()
                    {
                        ResultType = Enums.ResultType.Failed,
                        ResultCode = Constant.FAILED,
                        ResultDesc = "Failed"
                    };
                }
            }
            catch (Exception ex)
            {
                responseInfo = new ResponseInfo<bool>()
                {
                    ResultType = Enums.ResultType.Exception,
                    ResultCode = Constant.EXCEPTION,
                    ResultDesc = ex.Message.ToString()
                };
            }
            return Ok(responseInfo);
        }

        [HttpGet]
        [Route("GetAllDataInXMLCustom")]
        public async Task<ActionResult<List<StudentEntity>>> GetAllDataInXMLCustom()
        {
            ResponseInfo<List<StudentEntity>> responseInfo = new ResponseInfo<List<StudentEntity>>();
            List<StudentEntity> studentEntities = new List<StudentEntity>();
            responseInfo.Result = await _IRCMSInterface.GetStudentDetails();
            studentEntities = responseInfo.Result.ToList();
            string xmlData = XMLHelperExtentions.GenerateXML(studentEntities);
            return Ok(xmlData);
        }

        [HttpGet]
        [Route("GetAllDataInXML")]
        public async Task<ActionResult<List<StudentEntity>>> GetAllDataInXML()
        {
            ResponseInfo<List<StudentEntity>> responseInfo = new ResponseInfo<List<StudentEntity>>();
            responseInfo.Result = await _IRCMSInterface.GetStudentDetails();
            string xmlData = XMLHelper.ToXML<List<StudentEntity>>(responseInfo.Result);
            return Ok(xmlData);
        }
    }
}
