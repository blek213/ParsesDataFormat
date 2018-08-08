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
            CultureInfo.CurrentCulture = new CultureInfo("gu-IN"); //Задаем культуру 

            string inputString = "03-04-2018"; //Задаем время

            string validatedString = ValidateDateTime(inputString);

            List<string> formatsList = FindFormat(Formats, validatedString);

            string resultString = FiltrationPatterns(validatedString, formatsList);
            
            if(resultString != "")
            {
                Console.WriteLine(resultString);
            }
            else
            {
                Console.WriteLine("Не был найден подходящий формат");
            }

            Console.ReadKey();
        }

        //Warning....................................
        //Внимание. Если что-то происходит не так, то баг где-то тут находится:

        private static string  FiltrationPatterns(string validatedString, List<string> formatList) //Функция фильтрации найденных паттернов
        {
            string resultString = "";

            CutUslessElements cutUslessElements = new CutUslessElements();

            var containsDot = validatedString.Contains(".");
            var containsShesh = validatedString.Contains("/");
            var containsHyphen = validatedString.Contains("-");
            var contrainsZeroSpace = validatedString.Contains(" ");

            List<string> NotRemoveItems = new List<string>();

            if (containsDot == true)
            {
                //Отсечем ненужные форматы.Например, если у нас 05.07.2006, то d/ M / yyyy нам не нужен.

                NotRemoveItems = DeepValidationDateTime(validatedString, NotRemoveItems, formatList,"/");

                if (NotRemoveItems.Count != 1)
                {
                    return cutUslessElements.CutUsless(validatedString, NotRemoveItems, '.');
                }
            }

            if (containsShesh == true)
            {
                NotRemoveItems = DeepValidationDateTime(validatedString,NotRemoveItems, formatList, ".");

                if (NotRemoveItems.Count != 1)
                {
                    return cutUslessElements.CutUsless(validatedString, NotRemoveItems, '/');
                }
            }

            if(containsHyphen == true)
            {

                NotRemoveItems = DeepValidationDateTime(validatedString, NotRemoveItems, formatList, "/");
              
                if (NotRemoveItems.Count != 1)
                {
                    return cutUslessElements.CutUsless(validatedString, NotRemoveItems, '-');
                }
            }

            if(contrainsZeroSpace == true)
            {
                NotRemoveItems = DeepValidationDateTime(validatedString, NotRemoveItems, formatList, ".");
                NotRemoveItems = DeepValidationDateTime(validatedString, NotRemoveItems, formatList, "/");
                NotRemoveItems = DeepValidationDateTime(validatedString, NotRemoveItems, formatList, "-");

                if (NotRemoveItems.Count != 1)
                {
                    return cutUslessElements.CutUsless(validatedString, NotRemoveItems, '-');
                }
            }

            if (containsDot == false && containsShesh == false && containsHyphen == false && contrainsZeroSpace == false)
            {
                return formatList.FirstOrDefault();
            }

            return resultString;
        }

        private static List<string> DeepValidationDateTime(string validatedString, List<string> NotRemoveItems, List<string> formatList, string character)//Функция удаляет ненужные данные ()
        {
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

            //Специфические страны. Валидация форматов. 
            //Индия
            if (CultureInfo.CurrentCulture.Name == "gu-IN")
            {
                foreach (var format in NotRemoveItems)
                {
                    var checkFirstElement = format.IndexOf("M");

                    if (checkFirstElement == 0)
                    {

                    }
                    else
                    {
                        DeepValidatedItems.Add(format);
                    }
                }

                return DeepValidatedItems;
            }


            if (character == "/" && validatedString.Contains(".")) //Логично предположить, что паттерны состоят из точек 
            {
                //Поскольку состоят из точек, то выходит из этого день.месяц.год  
                foreach (var format in NotRemoveItems)
                {
                    var checkFirstElement = format.IndexOf("M");

                    if (checkFirstElement == 0)
                    {

                    }

                    else
                    {
                        DeepValidatedItems.Add(format);
                    }
                }

                return DeepValidatedItems;
            }

            if (character == "." && validatedString.Contains("/")) //Поскольку состоит из слешей, то выходит из этого месяць.день.год 
            {
                foreach (var format in NotRemoveItems)
                {
                    var checkFirstElement = format.IndexOf("d");

                    if (checkFirstElement == 0)
                    {

                    }
                    else
                    {
                        DeepValidatedItems.Add(format);
                    }
                }
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
