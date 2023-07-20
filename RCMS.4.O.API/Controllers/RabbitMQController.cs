using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using RCMS._4.O.Common;
using RCMS._4.O.Entities.RCMSEntities;
using RCMS._4.O.Interfaces.RabbitMQInterface;
using RCMS._4.O.Utilities.JWTToken;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static RCMS._4.O.Common.Enums;

namespace RCMS._4.O.API.Controllers
{
    //[JWTCustomAuthorization]
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMQController : ControllerBase
    {
        private readonly IRabitMQProducerInterface _IrabitMQProducerInterface;
        public RabbitMQController(IRabitMQProducerInterface IrabitMQProducerInterface)
        {
            _IrabitMQProducerInterface = IrabitMQProducerInterface;
        }

        [HttpPost("AddDataInRabbitMQ")]
        public async Task<ActionResult> AddDataInRabbitMQ(StudentEntity student)
        {
            ResponseInfo<List<StudentEntity>> responseInfo = new Common.ResponseInfo<List<StudentEntity>>();
            try
            {
                //for (int i = 1; i <= 5; i++)
                //{
                var result = await _IrabitMQProducerInterface.SendProductMessage(student);
                if (result == true)
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
                // }

            }
            catch (Exception ex)
            {
                responseInfo.ResultType = Enums.ResultType.Exception;
                responseInfo.ResultCode = Constant.EXCEPTION;
                responseInfo.ResultDesc = ex.ToString();
            }
            return Ok(responseInfo);
        }
    }
}
