using System;
using System.Collections.Generic;
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
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU"); //Задаем культуру 

            string inputString = "03.04.2018"; //Задаем время

            string validatedString = ValidateDateTime(inputString);

            List<string> formatsList = FindFormat(Formats, validatedString);

            formatsList = FiltrationPatterns(validatedString, formatsList);
            
            foreach (var b in formatsList)
            {
                Console.WriteLine(b);
            }

            Console.ReadKey();
        }

        private static List<string> FiltrationPatterns(string validatedString, List<string> formatList) //Функция фильтрации найденных паттернов
        {
            var containsDot = validatedString.Contains(".");
            var containsShesh = validatedString.Contains("/");
            var containsHyphen = validatedString.Contains("-");
            var contrainsZeroSpace = validatedString.Contains(" ");

            List<string> NotRemoveItems = new List<string>();

            if (containsDot == true)
            {
                NotRemoveItems=DeepValidationDateTime(validatedString, NotRemoveItems, formatList,"/");
            }

            if (containsShesh == true)
            {
                NotRemoveItems = DeepValidationDateTime(validatedString,NotRemoveItems, formatList, ".");
            }

            if(containsHyphen == true)
            {
                NotRemoveItems = DeepValidationDateTime(validatedString, NotRemoveItems, formatList, "/");
            }

            if(contrainsZeroSpace == true)
            {
                NotRemoveItems = DeepValidationDateTime(validatedString, NotRemoveItems, formatList, ".");
                NotRemoveItems = DeepValidationDateTime(validatedString, NotRemoveItems, formatList, "/");
            }

            if (containsDot == false && containsShesh == false && containsHyphen == false && contrainsZeroSpace == false)
            {
                return formatList;
            }
        
            return NotRemoveItems;
        }

        private static List<string> DeepValidationDateTime(string validatedString, List<string> NotRemoveItems, List<string> formatList, string character)//Функция удаляет ненужные данные ()
        {
            CutUslessElements cutUslessElements = new CutUslessElements();

            foreach (var format in formatList)
            {
                var checkContains = format.Contains(character);

                if (checkContains == true)
                {

                }
                else
                {
                    NotRemoveItems.Add(format);
                }
            }

            List<string> DeepValidatedItems = new List<string>();

            if (character == "/") //Логично предположить, что паттерны состоят из точек 
            {
                //Поскольку состоят из точек, то выходит из этого день.месяц.год  
                foreach (var format in NotRemoveItems)
                {
                    var checkFirstElement = format.IndexOf("M");

                    if (checkFirstElement == 0)
                    {

                    }
                    else {
                        DeepValidatedItems.Add(format);
                    }
                }

                //List<string> resultList = new List<string>();

                //Отсечем ненужные форматы.Например, если у нас 05.07.2006, то d/ M / yyyy нам не нужен.
                if (DeepValidatedItems.Count != 1)
                {
                    if(character == "/")
                    {
                        return cutUslessElements.CutUsless(validatedString, DeepValidatedItems, ".");

                    }
                }

            return DeepValidatedItems;
            }
            
            if(character == ".") //Поскольку состоит из слешей, то выходит из этого месяць.день.год 
            {

            }

            return NotRemoveItems;
        }

        private static string ValidateDateTime(string dateTime) //Функция удаляет ненужные данные (время, am, pm )
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

        private static string TruncateFromCharacters(string dateTime, string characters) //Функция поиска паттернов для строки с датой
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

        private static List<string> FindFormat(string[] formats, string validatedString)
        {
            List<string> selectedFormats = new List<string>();

            for (var i = 0; i < Formats.Length; i++)
            {
                try
                {       
                    DateTime dt = DateTime.ParseExact(validatedString, formats[i], CultureInfo.CurrentCulture);
                    selectedFormats.Add(formats[i]);
                }

                catch (Exception ex)
                {

                }
            }

            return selectedFormats;        
        }

      
    }
}
