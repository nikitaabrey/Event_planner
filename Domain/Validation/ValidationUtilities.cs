using FluentValidation;

namespace Event_planner.Domain.Validation
{
    public static class ValidationUtilities
    {
        public static bool isValidTime(string _time)
        {
            TimeSpan time;
            return TimeSpan.TryParseExact(_time, "hh\\:mm\\:ss", System.Globalization.CultureInfo.InvariantCulture, out time);
        }

        public static bool isValidDate(string _date)
        {
            DateTime date;
            return DateTime.TryParseExact(_date, "yyyy-mm-dd", System.Globalization.CultureInfo.InvariantCulture,
        System.Globalization.DateTimeStyles.None, out date);
        }


        public static bool isValidYear(int year) {
        DateTime dateTime;
        return DateTime.TryParse(string.Format("1/1/{0}", year), out dateTime);
        }

        public static bool isValidMonth(int month) {
            return month >= 1 && month <= 12;
        }
    }
}
