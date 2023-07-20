using RCMS._4.O.Common;
using RCMS._4.O.Entities.CaptchaEntities;
using RCMS._4.O.Interfaces.CaptchaInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Repository.CaptchaRepository
{
    public class CaptchaRepository : ICaptchaInterface
    {
        public Task<string> GetAlphaNumericCaptcha()
        {
            return Task.FromResult( new CaptchaHelper().GetAlphaNumericCaptcha());
        }

        public Task<string> GetNumericCaptcha()
        {
            return Task.FromResult(new CaptchaHelper().GetNumericCaptcha());
        }

        public Task<string> GetOnlyAlphaUpperCaptcha()
        {
            return Task.FromResult(new CaptchaHelper().GetOnlyAlphaUpperCaptcha());
        }

        public Task<CaptchaResult> GetImageCaptcha(int width, int height, string captchaCode)
        {
            return Task.FromResult(new CaptchaHelper().GetImageCaptcha(width, height, captchaCode));
        }
    }
}
