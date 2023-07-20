using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using NPOI.SS.Formula.Functions;
using RCMS._4.O.Entities.RCMSEntities;
using RCMS._4.O.Interfaces.RadisCacheInterface;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using RCMS._4.O.Common;
using RCMS._4.O.Utilities.JWTToken;

namespace RCMS._4.O.API.Controllers
{
    //[JWTCustomAuthorization]
    [Route("api/[controller]")]
    [ApiController]
    public class RadisCacheController : ControllerBase
    {
        private readonly IRadisCacheInterface _IradisCacheInterface;
        public RadisCacheController(IRadisCacheInterface IradisCacheInterface)
        {
            _IradisCacheInterface = IradisCacheInterface;
        }

        [HttpPost("AddRadisCacheData")]
        public async Task<ActionResult> AddRadisCacheData(StudentEntity student)
        {
            ResponseInfo<List<StudentEntity>> responseInfo = new Common.ResponseInfo<List<StudentEntity>>();
            try
            {
                var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
                var res = await _IradisCacheInterface.SetData<StudentEntity>("Employee", student, expirationTime);
                if (res == true)
                {
                    responseInfo.ResultType = Enums.ResultType.Success;
                    responseInfo.ResultCode = Constant.SUCCESS;
                    responseInfo.ResultDesc = "Success";
                }
                else
                {
                    responseInfo.ResultType = Enums.ResultType.Failed;
                    responseInfo.ResultCode = Constant.FAILED;
                    responseInfo.ResultDesc = "Failed";
                }
            }
            catch (Exception ex)
            {
                responseInfo.ResultType = Enums.ResultType.Exception;
                responseInfo.ResultCode = Constant.EXCEPTION;
                responseInfo.ResultDesc = ex.ToString();
            }
            return Ok(responseInfo);

        }

        [HttpGet("GetRadisSetData")]
        public async Task<ActionResult<StudentEntity>> GetRadisSetData()
        {
            ResponseInfo<StudentEntity> responseInfo = new Common.ResponseInfo<StudentEntity>();
            try
            {
                responseInfo.Result = _IradisCacheInterface.GetData<StudentEntity>("Employee");
                if (responseInfo.Result != null)
                {
                    responseInfo.ResultType = Enums.ResultType.Success;
                    responseInfo.ResultCode = Constant.SUCCESS;
                    responseInfo.ResultDesc = "Success";
                }
                else
                {
                    responseInfo = new ResponseInfo<StudentEntity>()
                    {
                        ResultType = Enums.ResultType.Failed,
                        ResultCode = Constant.FAILED,
                        ResultDesc = "Failed"
                    };
                }
            }
            catch (Exception ex)
            {
                responseInfo = new ResponseInfo<StudentEntity>()
                {
                    ResultType = Enums.ResultType.Exception,
                    ResultCode = Constant.EXCEPTION,
                    ResultDesc = ex.Message.ToString()
                };
            }
            return Ok(responseInfo);

        }
    }
}
