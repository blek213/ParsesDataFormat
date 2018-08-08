using System;
using System.Collections.Generic;
using System.Text;

namespace ParsesDataFormat
{
     class CutUslessElements
    {
       
        public string CutUsless(string validatedString, List<string> formats, char character)
        {
            string resultString="";

            List<int> countFormat = new List<int>();
            List<int> countString = new List<int>();

            //Считаем для строки 

            //Разбиваем строку на массив, дальше будет проще работать 

            string[] tempArrString = validatedString.Split(new char[] {character});


            //Разбиваем формат на массив 

            foreach (var format in formats)
            {
                string[] tempArrFormat = format.Split(new char[] { character});

                int positiveCount = 0;

                 for(int i = 0; i < 3; i++)
                {
                    if(tempArrFormat[i].Length == tempArrString[i].Length)
                    {
                        positiveCount++;
                    }
                }

                 if(positiveCount == 3)
                {
                    return format;
                }

            }

            return resultString;
        }

        private static int CountSymbols(string format, string character)
        {
            return format.IndexOf(character);
        }

    }
}
