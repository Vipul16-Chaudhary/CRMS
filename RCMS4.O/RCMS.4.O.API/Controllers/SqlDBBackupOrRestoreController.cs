using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RCMS._4.O.Common;
using RCMS._4.O.Entities.RCMSEntities;
using RCMS._4.O.Interfaces.RCMSInterface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using RCMS._4.O.Interfaces.SqlBackupOrRestoreInterface;
using RCMS._4.O.Entities.SqlBackupOrRestoreEntity;

namespace RCMS._4.O.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlDBBackupOrRestoreController : ControllerBase
    {
        private readonly ISqlBackupOrRestoreInterface _IsqlBackupOrRestoreInterface;
        public SqlDBBackupOrRestoreController(ISqlBackupOrRestoreInterface IsqlBackupOrRestoreInterface) 
        {
            _IsqlBackupOrRestoreInterface = IsqlBackupOrRestoreInterface;
        }

        [HttpGet("GetSqlDatabaseName")]
        public async Task<ActionResult<List<SqlNameDatabaseEntity>>> GetSqlDatabaseName()
        {
            ResponseInfo<List<SqlNameDatabaseEntity>> responseInfo = new Common.ResponseInfo<List<SqlNameDatabaseEntity>>();
            try
            {
                responseInfo.Result = await _IsqlBackupOrRestoreInterface.GetAllDatabaseName();

                if (responseInfo.Result != null)
                {
                    responseInfo.ResultType = Enums.ResultType.Success;
                    responseInfo.ResultCode = Constant.SUCCESS;
                    responseInfo.ResultDesc = "Success";
                }
                else
                {
                    responseInfo = new ResponseInfo<List<SqlNameDatabaseEntity>>()
                    {
                        ResultType = Enums.ResultType.Failed,
                        ResultCode = Constant.FAILED,
                        ResultDesc = "Failed"
                    };
                }
            }
            catch (Exception ex)
            {
                responseInfo = new ResponseInfo<List<SqlNameDatabaseEntity>>()
                {
                    ResultType = Enums.ResultType.Exception,
                    ResultCode = Constant.EXCEPTION,
                    ResultDesc = ex.Message.ToString()
                };
            }
            return Ok(responseInfo);
        }

        [HttpGet("GetSqlDatabaseInstanceName")]
        public async Task<ActionResult<List<SqlInstanceEntity>>> GetSqlDatabaseInstanceName()
        {
            ResponseInfo<List<SqlInstanceEntity>> responseInfo = new Common.ResponseInfo<List<SqlInstanceEntity>>();
            try
            {
                responseInfo.Result = await _IsqlBackupOrRestoreInterface.GetAllDbInstanceName();

                if (responseInfo.Result != null)
                {
                    responseInfo.ResultType = Enums.ResultType.Success;
                    responseInfo.ResultCode = Constant.SUCCESS;
                    responseInfo.ResultDesc = "Success";
                }
                else
                {
                    responseInfo = new ResponseInfo<List<SqlInstanceEntity>>()
                    {
                        ResultType = Enums.ResultType.Failed,
                        ResultCode = Constant.FAILED,
                        ResultDesc = "Failed"
                    };
                }
            }
            catch (Exception ex)
            {
                responseInfo = new ResponseInfo<List<SqlInstanceEntity>>()
                {
                    ResultType = Enums.ResultType.Exception,
                    ResultCode = Constant.EXCEPTION,
                    ResultDesc = ex.Message.ToString()
                };
            }
            return Ok(responseInfo);
        }


        [HttpGet("StartSqlDatabaseBackup")]
        public async Task<ActionResult> StartSqlDatabaseBackup()
        {
            ResponseInfo<bool> responseInfo = new Common.ResponseInfo<bool>();
            try
            {
                responseInfo.Result = await _IsqlBackupOrRestoreInterface.StartDatabaseBackUp();

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

        [HttpGet("RestoreDatabaseBackUp")]
        public async Task<ActionResult> RestoreDatabaseBackUp()
        {
            ResponseInfo<bool> responseInfo = new Common.ResponseInfo<bool>();
            try
            {
                responseInfo.Result = await _IsqlBackupOrRestoreInterface.RestoreDatabaseBackUp();

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

    }
}
