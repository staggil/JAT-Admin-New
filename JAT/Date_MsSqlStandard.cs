using System;
using System.Diagnostics;

namespace JAT
{
    public static class Date_MsSqlStandard
    {
        public static string Cast(string DateTimeString)
        {
            try
            {
                return DateTime.ParseExact(DateTimeString, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
            } 
            catch
            {
                return DateTime.ParseExact(DateTimeString, "dd/MM/yyyy hh:mm:ss", null)
                    .ToString("yyyy/MM/dd");
            }
        }

        public static string CastQuery(string DateString)
        {
            int _textLen = DateString.Length;
            Debug.WriteLine("Raw : " + DateString);
            if (_textLen > 10)
            {
                DateString = DateString.Remove(11);
                Debug.WriteLine("Removed : " + DateString);
            }

            try
            {
                DateString = DateTime.ParseExact(DateString, "dd/MM/yyyy", null).ToString();
                Debug.WriteLine("Parse 1 : " + DateString);

                DateString = DateTime.Parse(DateString).ToString("yyyy/MM/dd");
                Debug.WriteLine("Parse 2 : " + DateString + "\n\n");
            } 
            catch
            {
                DateString = DateTime.Parse(DateString).ToString("yyyy/MM/dd");
                Debug.WriteLine("Parse 2 : " + DateString + "\n\n");
            }

            return $"(SELECT CONVERT(DATE, '{DateString}', 23))";
        }
    }
}