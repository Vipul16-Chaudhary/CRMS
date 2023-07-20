using System;

namespace RCMS._4.O.Common
{
    public class Enums
    {
        [Serializable]
        public enum ConnectionType
        {
            RCMS = 1,
            Namantran = 2,
            Batwara = 3,
            Bhoolekh = 4,
            Simankan = 5,
            WebGIS = 6,
            GIS = 7,
            Land = 8

        }

        [Serializable]
        public enum ResultType
        {
            Success = 1,
            Failed = 2,
            Exception = 3,
            Info=4,
            Error=5,
            Interservererror = 6,
            Servicenotfound = 7
        }

        [Serializable]
        public enum CaptchaType
        {
            Image = 1,
            Numeric = 2,
            AlaphaNumeric = 3,
            UpperCase=4,
        }

    }
}
