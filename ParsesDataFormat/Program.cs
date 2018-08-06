using System;
using System.Globalization;
using System.Linq;

namespace ParsesDataFormat
{
    class Program
    {
        public static string[] Formats = {
                    "yyyyMM",
                    "M/d/yyyy", "MM/dd/yyyy", "MMM/d/yyyy", "MMMM/dd/yyyy",
                    "d/M/yyyy", "dd/MM/yyyy", "d/MMM/yyyy", "dd/MMMM/yyyy",
                    "yyyy/M/d", "yyyy/MM/dd", "yyyy/MMM/d", "yyyy/MMMM/dd",
                    "M-d-yyyy", "MM-dd-yyyy", "MMM-d-yyyy", "MMMM-dd-yyyy",
                    "d-M-yyyy", "dd-MM-yyyy", "d-MMM-yyyy", "dd-MMMM-yyyy",
                    "yyyy-M-d", "yyyy-MM-dd", "yyyy-MMM-d", "yyyy-MMMM-dd",
                    "M.d.yyyy", "MM.dd.yyyy", "MMM.d.yyyy", "MMMM.dd.yyyy",
                    "d.M.yyyy", "dd.MM.yyyy", "d.MMM.yyyy", "dd.MMMM.yyyy",
                    "yyyy.M.d", "yyyy.MM.dd", "yyyy.MMM.d", "yyyy.MMMM.dd",
                    "M d yyyy", "MM dd yyyy", "MMM d yyyy", "MMMM dd yyyy",
                    "d M yyyy", "dd MM yyyy", "d MMM yyyy", "dd MMMM yyyy",
                    "yyyy M d", "yyyy MM dd", "yyyy MMM d", "yyyy MMMM dd",

                     "M/d/yy", "MM/dd/yy", "MMM/d/yy", "MMMM/dd/yy",
                     "d/M/yy", "dd/MM/yy", "d/MMM/yy", "dd/MMMM/yy",
                     "yy/M/d", "yy/MM/dd", "yy/MMM/d", "yy/MMMM/dd",
                     "M-d-yy", "MM-dd-yy", "MMM-d-yy", "MMMM-dd-yy",
                     "d-M-yy", "dd-MM-yy", "d-MMM-yy", "dd-MMMM-yy",
                     "yy-M-d", "yy-MM-dd", "yy-MMM-d", "yy-MMMM-dd",
                     "M.d.yy", "MM.dd.yy", "MMM.d.yy", "MMMM.dd.yy",
                     "d.M.yy", "dd.MM.yy", "d.MMM.yy", "dd.MMMM.yy",
                     "yy.M.d", "yy.MM.dd", "yy.MMM.d", "yy.MMMM.dd",
                     "M d yy", "MM dd yy", "MMM d yy", "MMMM dd yy",
                     "d M yy", "dd MM yy", "d MMM yy", "dd MMMM yy",
                     "yy M d", "yy MM dd", "yy MMM d", "yy MMMM dd",
                   };

        static void Main(string[] args)
        {
            string inputString = "03.12.2010";

            string validatedString = ValidateDateTime(inputString);

            //  DateTime dt =  DateTime.ParseExact(s, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            for (var i = 0; i < Formats.Length; i++)
            {
                try
                {
                    DateTime dt = DateTime.ParseExact(validatedString, Formats[i], CultureInfo.InvariantCulture);

                    Console.WriteLine(Formats[i]);
                }
                catch (Exception ex)
                {

                }
            }

            Console.ReadKey();
        }

        private static string ValidateDateTime(string dateTime)
        {
            var checkCharacter = dateTime.Contains(":");
            var checkAm = dateTime.Contains("am");
            var checkPm = dateTime.Contains("pm");
            var checkAmWithDots = dateTime.Contains("a.m.");
            var checkPmWithDots = dateTime.Contains("p.m.");

            if (checkCharacter == true)
            {
                int positionCharacter = dateTime.IndexOf(':');

                int positionBeforeCharacter = 0;

                for (int i = positionCharacter; i > 0; i--)
                {
                    if (dateTime[i] == ' ')
                    {
                        positionBeforeCharacter = i;
                    }
                }

                return dateTime.Remove(positionBeforeCharacter, dateTime.Length - positionBeforeCharacter);
            }

            if (checkAm == true)
            {
                return TruncateFromCharacters(dateTime, "am");
            }

            if(checkPm == true)
            {
                return TruncateFromCharacters(dateTime, "pm");
            }

            if (checkAmWithDots == true)
            {
                return TruncateFromCharacters(dateTime, "a.m.");
            }

            if (checkPmWithDots == true)
            {
                return TruncateFromCharacters(dateTime, "p.m.");
            }

            return dateTime;
        }

        private static string TruncateFromCharacters(string dateTime, string characters)
        {
            int positionCharacter = dateTime.IndexOf(characters);

            int positionBeforeCharacter = 0;

            for (int i = positionCharacter; i > 0; i--)
            {
                if (dateTime[i] == ' ')
                {
                    positionBeforeCharacter = i;
                }
            }

            return dateTime.Remove(positionBeforeCharacter, dateTime.Length - positionBeforeCharacter);
        }

        private static int FindFormat(string[] formats, string validatedString)
        {
            for (var i = 0; i < Formats.Length; i++)
            {
                try
                {
                    DateTime dt = DateTime.ParseExact(validatedString, Formats[i], CultureInfo.InvariantCulture);

                    Console.WriteLine(Formats[i]);
                }
                catch (Exception ex)
                {

                }
            }

            return 1;        
        }


    }
}
