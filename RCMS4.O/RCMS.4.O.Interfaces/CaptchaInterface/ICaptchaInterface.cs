using RCMS._4.O.Entities.CaptchaEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Interfaces.CaptchaInterface
{
    public interface ICaptchaInterface
    {
        Task<string> GetNumericCaptcha();
        Task<string> GetAlphaNumericCaptcha();
        Task<string> GetOnlyAlphaUpperCaptcha();
        Task<CaptchaResult> GetImageCaptcha(int width, int height, string captchaCode);
    }
}
