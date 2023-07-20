using RCMS._4.O.Entities.CaptchaEntities;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Common
{
    public class CaptchaHelper
    {
        const string Letters = "012346789ABCDEFGHJKLMNPRTUVWXYZabcdefghjklmnprtuvwxyz";

        public string GetNumericCaptcha()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "0123456789";

            Random r = new Random();
            for (int j = 0; j <= 4; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString();

        }

        public string GetAlphaNumericCaptcha()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = Letters;
            Random r = new Random();
            for (int j = 0; j <= 4; j++)
            { 
                randomText.Append(alphabets[r.Next(alphabets.Length)]); 
            }
            return randomText.ToString();

        }

        public string GetOnlyAlphaUpperCaptcha()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random r = new Random();
            for (int j = 0; j <= 4; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString();

        }

        //public static string GenerateCaptchaCode()
        //{
        //    Random rand = new Random();
        //    int maxRand = Letters.Length - 1;

        //    StringBuilder sb = new StringBuilder();

        //    for (int i = 0; i < 4; i++)
        //    {
        //        int index = rand.Next(maxRand);
        //        sb.Append(Letters[index]);
        //    }
        //    return sb.ToString();
        //}
        public  CaptchaResult GetImageCaptcha(int width, int height, string captchaCode)
        {
            using (Bitmap baseMap = new Bitmap(width, height))
            using (Graphics graph = Graphics.FromImage(baseMap))
            {
                Random rand = new Random();
                graph.Clear(GetRandomLightColor());
                DrawCaptchaCode();
                DrawDisorderLine();
                //AdjustRippleEffect();
                MemoryStream ms = new MemoryStream();
                baseMap.Save(ms, ImageFormat.Png);
                return new CaptchaResult
                {
                    CaptchaCode = captchaCode,
                    CaptchaByteData = ms.ToArray(),
                    Timestamp = DateTime.Now
                };

                int GetFontSize(int imageWidth, int captchCodeCount)
                {
                    var averageSize = imageWidth / captchCodeCount;
                    return Convert.ToInt32(averageSize);
                }

                Color GetRandomDeepColor()
                {
                    int redlow = 160, greenLow = 100, blueLow = 100;
                    return Color.FromArgb(rand.Next(redlow), rand.Next(greenLow), rand.Next(blueLow));
                }

                Color GetRandomLightColor()
                {
                    int low = 180, high = 255;
                    int nRend = rand.Next(high) % (high - low) + low;
                    int nGreen = rand.Next(high) % (high - low) + low;
                    int nBlue = rand.Next(high) % (high - low) + low;
                    return Color.FromArgb(nRend, nGreen, nBlue);
                }

                void DrawCaptchaCode()
                {
                    SolidBrush fontBrush = new SolidBrush(Color.Black);
                    int fontSize = GetFontSize(width, captchaCode.Length);
                    Font font = new Font(FontFamily.GenericSerif, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
                    for (int i = 0; i < captchaCode.Length; i++)
                    {
                        fontBrush.Color = GetRandomDeepColor();
                        int shiftPx = fontSize / 6;
                        float x = i * fontSize + rand.Next(-shiftPx, shiftPx) + rand.Next(-shiftPx, shiftPx);
                        int maxY = height - fontSize;
                        if (maxY < 0) maxY = 0;
                        float y = rand.Next(0, maxY);
                        graph.DrawString(captchaCode[i].ToString(), font, fontBrush, x, y);
                    }
                }

                void DrawDisorderLine()
                {
                    Pen linePen = new Pen(new SolidBrush(Color.DarkBlue), 2);
                    for (int i = 0; i < rand.Next(3, 5); i++)
                    {
                        linePen.Color = GetRandomDeepColor();
                        Point startPoint = new Point(rand.Next(0, width), rand.Next(0, height));
                        Point endPoint = new Point(rand.Next(0, width), rand.Next(0, height));
                        graph.DrawLine(linePen, startPoint, endPoint);
                    }
                }
            }
        }
    }
}
