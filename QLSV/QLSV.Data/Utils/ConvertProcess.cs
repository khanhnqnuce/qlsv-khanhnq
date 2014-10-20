using System;
using System.Text.RegularExpressions;

namespace QLSV.Data.Utils
{
    public static class ConvertProcess
    {
        public enum TypeDateTime
        {
            NgayThang,
            ThangNgay
        }

        public static DateTime DateTime(string input, TypeDateTime type)
        {
            var regex = new Regex("[-|\\/]");
            if (string.IsNullOrEmpty(input))
            {
                return System.DateTime.Now;
            }
            var strdate = regex.Split(input);
            switch (type)
            {
                case TypeDateTime.NgayThang:
                    return new DateTime(int.Parse(strdate[2]), int.Parse(strdate[1]), int.Parse(strdate[0]));
                case TypeDateTime.ThangNgay:
                    return new DateTime(int.Parse(strdate[2]), int.Parse(strdate[0]), int.Parse(strdate[1]));
                default:
                    return System.DateTime.Now;
            }
        }

        public static DateTime DateTime(string input)
        {
            return DateTime(input, TypeDateTime.NgayThang);
        }
    }
}
