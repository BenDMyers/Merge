using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class SqlDateHelper
{
    static List<String> sqlDatetimeFormats = new List<string>(new String[] { "M/d/yyyy h:mm:ss tt", "M/d h:mm:ss tt" });

    public static DateTime parseSqlDate(String date)
    {
        foreach (String format in sqlDatetimeFormats)
        {
            try
            {
                DateTime time = DateTime.ParseExact(date, format, null);
                // now...... make it into a certain timezone!
                var myTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(time, myTimeZone);

                // we could also do some datetime timezone stuffs to convert everythin into UTC.
                return currentDateTime;
            }
            catch (System.FormatException e)
            {
                // eh, this isn't the right format.. we'll try the next one.
            }
        }
        return DateTime.Now; // couldn't find a datetime format that worked... sooo why not user now?
    }
}