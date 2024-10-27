using System;
using System.Globalization;

public class DateConverter
{
    public static string ConvertToPersianDate(DateTime gregorianDate)
    {
        PersianCalendar persianCalendar = new PersianCalendar();
        int year = persianCalendar.GetYear(gregorianDate);
        int month = persianCalendar.GetMonth(gregorianDate);
        int day = persianCalendar.GetDayOfMonth(gregorianDate);

        return $"{year}/{month:D2}/{day:D2}";
    }
}
