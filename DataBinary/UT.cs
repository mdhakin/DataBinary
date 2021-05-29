using System;

namespace DataBinary
{
    public class UT
    {

        /// <summary>
        /// Takes in a long Unix Time stamp and returns a DateTime object
        /// </summary>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// Returns a long Unix time stamp when provided a DateTime object
        /// </summary>
        public static long ToUnixTimestamp(DateTime target)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            var unixTimestamp = System.Convert.ToInt64((target - date).TotalSeconds);

            return unixTimestamp;
        }

        /// <summary>
        /// Returns the current time in the Unix timestamp format in a long
        /// </summary>
        public static Int64 GetCurrentTimestamp()
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            return (Int64)unixTimestamp;
        }

        /// <summary>
        /// Returns the number of seconds in the number  of days it is provided.
        /// </summary>
        public static long TimeSpan(int days)
        {
            long secondsInDay = 864000;

            return secondsInDay * days;
        }
    }
}
