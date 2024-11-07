using Microsoft.Owin;
using System;
using System.Diagnostics;
using System.Globalization;

namespace JAT
{
    public static class Date_MsSqlStandard
    {
        public static string Cast(string DateTimeString)
        {

            var _dateStringReform = DateTimeString;

            Debug.WriteLine("Plain : " + _dateStringReform + "\n\n");

            try
            {
                _dateStringReform = DateTime.ParseExact(_dateStringReform, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                Debug.WriteLine("Reform 1 : " + _dateStringReform + "\n\n");
            }
            catch
            {
                _dateStringReform = DateTime.ParseExact(_dateStringReform, "dd/MM/yyyy hh:mm:ss", null)
                    .ToString("yyyy/MM/dd");
                Debug.WriteLine("Reform 1-1 : " + _dateStringReform + "\n\n");
            }

            

            try
            {
                _dateStringReform = DateTime.Parse(_dateStringReform).ToString("yyyy/MM/dd");
                Debug.WriteLine("Reform 2 : " + _dateStringReform + "\n\n");
            }
            catch
            {
                Debug.WriteLine("Can't be Reform 2, Invalid date format");
            }

            //int _textLen = _dateStringReform.Length;
            //Debug.WriteLine("Input : " + _dateStringReform);
            //if (_textLen > 10)
            //{
            //    _dateStringReform = _dateStringReform.Remove(10);
            //    Debug.WriteLine("Removed : " + _dateStringReform);
            //}

            try
            {
                DateTime date; // Parse the date string using the provided format

                if (DateTime.TryParseExact(_dateStringReform, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                { // Format the date to the desired format
                    string formattedDate = date.ToString("yyyy/MM/dd");
                    _dateStringReform = formattedDate; // Output: 2024/10/30
                    Debug.WriteLine("Reform 3 : " + _dateStringReform);
                }
                //else
                //{
                //    _dateStringReform = DateTime.Parse(_dateStringReform).ToString("yyyy/MM/dd");
                //    Debug.WriteLine("Parse 1 : " + _dateStringReform + "\n\n");
                //}

                //DateString = DateTime.ParseExact(DateString, "dd/MM/yyyy", null).ToString();
                //Debug.WriteLine("Parse 1 : " + DateString);

                //DateString = DateTime.Parse(DateString).ToString("yyyy/MM/dd");
                //Debug.WriteLine("Parse 2 : " + DateString + "\n\n");
            }
            catch
            {
                //*** '30/10/2024'

                //DateString = DateTime.Parse(DateString).ToString("yyyy/MM/dd");

                Debug.WriteLine("ERROR : Reform date/time string format.");
            }
            return _dateStringReform;
        }

        public static string CastQuery(string DateString)
        {
            return $"(SELECT CONVERT(DATE, '{Date_MsSqlStandard.Cast(DateString)}', 23))";
        }
    }
}