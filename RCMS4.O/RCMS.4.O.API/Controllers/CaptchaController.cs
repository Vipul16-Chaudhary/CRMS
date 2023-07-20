using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RCMS._4.O.Common;
using RCMS._4.O.Entities.CaptchaEntities;
using RCMS._4.O.Interfaces.CaptchaInterface;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RCMS._4.O.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly ICaptchaInterface _IcaptchaInterface;
        public CaptchaController(ICaptchaInterface IcaptchaInterface)
        {
            _IcaptchaInterface = IcaptchaInterface;
        }

        [HttpGet("GenerateCaptchaImage")]
        public async Task<ActionResult> GenerateCaptchaImage(string CaptchaType)
        {
            int width = 100;
            int height = 36;
            Stream stream = null;
            ResponseInfo<HttpResponseMessage> responseInfo = null;
            try
            {
                if (Enums.CaptchaType.Image.ToString() == CaptchaType)
                {
                    var captchaCode = await _IcaptchaInterface.GetAlphaNumericCaptcha();
                    var result = await _IcaptchaInterface.GetImageCaptcha(width, height, captchaCode);
                    // HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
                    stream = new MemoryStream(result.CaptchaByteData);
                    return new FileStreamResult(stream, "image/png");
                }
                else
                {
                    responseInfo = new ResponseInfo<HttpResponseMessage>()
                    {
                        ResultType = Enums.ResultType.Failed,
                        ResultCode = Constant.FAILED,
                        ResultDesc = "Captcha is not loading. Please try again after some time."
                    };
                    return Ok(responseInfo);
                }
            }
            catch (System.Exception ex)
            {
                responseInfo = new ResponseInfo<HttpResponseMessage>()
                {
                    ResultType = Enums.ResultType.Exception,
                    ResultCode = Constant.EXCEPTION,
                    ResultDesc = ex.Message.ToString()
                };
                return Ok(responseInfo);
            }
        }

        [HttpGet("GenerateCaptchaCodeWithParams")]
        public async Task<ActionResult> GenerateCaptchaCodeWithParams(string CaptchaType, string captchaCode)
        {
            int width = 100;
            int height = 36;
            ResponseInfo<string> responseInfo = new ResponseInfo<string>();
            ResponseInfo<CaptchaResult> responseInfoObj = new ResponseInfo<CaptchaResult>();
            try
            {
                if (Enums.CaptchaType.Image.ToString() == CaptchaType && !string.IsNullOrEmpty(captchaCode))
                {
                    responseInfoObj.Result = await _IcaptchaInterface.GetImageCaptcha(width, height, captchaCode);
                    return Ok(responseInfoObj);
                }
                else if (Enums.CaptchaType.Numeric.ToString() == CaptchaType)
                {
                    responseInfo.Result = await _IcaptchaInterface.GetNumericCaptcha();
                    if (responseInfo.Result != null)
                    {
                        responseInfo.ResultType = Enums.ResultType.Success;
                        responseInfo.ResultCode = Constant.SUCCESS;
                        responseInfo.ResultDesc = "Success";
                        return Ok(responseInfo);
                    }
                    else
                    {
                        responseInfo = new ResponseInfo<string>()
                        {
                            ResultType = Enums.ResultType.Failed,
                            ResultCode = Constant.FAILED,
                            ResultDesc = "Failed"
                        };
                        return Ok(responseInfo);
                    }
                }
                else if (Enums.CaptchaType.AlaphaNumeric.ToString() == CaptchaType)
                {
                    responseInfo.Result = await _IcaptchaInterface.GetAlphaNumericCaptcha();
                    if (responseInfo.Result != null)
                    {
                        responseInfo.ResultType = Enums.ResultType.Success;
                        responseInfo.ResultCode = Constant.SUCCESS;
                        responseInfo.ResultDesc = "Success";
                        return Ok(responseInfo);
                    }
                    else
                    {
                        responseInfo = new ResponseInfo<string>()
                        {
                            ResultType = Enums.ResultType.Failed,
                            ResultCode = Constant.FAILED,
                            ResultDesc = "Failed"
                        };
                        return Ok(responseInfo);
                    }
                }
                else if (Enums.CaptchaType.UpperCase.ToString() == CaptchaType)
                {
                    responseInfo.Result = await _IcaptchaInterface.GetOnlyAlphaUpperCaptcha();
                    if (responseInfo.Result != null)
                    {
                        responseInfo.ResultType = Enums.ResultType.Success;
                        responseInfo.ResultCode = Constant.SUCCESS;
                        responseInfo.ResultDesc = "Success";
                        return Ok(responseInfo);
                    }
                    else
                    {
                        responseInfo = new ResponseInfo<string>()
                        {
                            ResultType = Enums.ResultType.Failed,
                            ResultCode = Constant.FAILED,
                            ResultDesc = "Failed"
                        };
                        return Ok(responseInfo);
                    }
                }
                else
                {
                    responseInfoObj = new ResponseInfo<CaptchaResult>()
                    {
                        ResultType = Enums.ResultType.Failed,
                        ResultCode = Constant.FAILED,
                        ResultDesc = "Captcha is not loading. Please try again after some time."
                    };
                    return Ok(responseInfoObj);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(captchaCode))
                {
                    responseInfoObj = new ResponseInfo<CaptchaResult>()
                    {
                        ResultType = Enums.ResultType.Exception,
                        ResultCode = Constant.EXCEPTION,
                        ResultDesc = ex.Message.ToString()
                    };
                }
                else
                {
                    responseInfo = new ResponseInfo<string>()
                    {
                        ResultType = Enums.ResultType.Exception,
                        ResultCode = Constant.EXCEPTION,
                        ResultDesc = ex.Message.ToString()
                    };
                }
                return Ok(responseInfoObj);
            }
        }
    }
}
