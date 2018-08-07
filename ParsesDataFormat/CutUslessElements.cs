using System;
using System.Collections.Generic;
using System.Text;

namespace ParsesDataFormat
{
     class CutUslessElements
    {
       
        public List<string> CutUsless(string validatedString, List<string> formats, string character)
        {
            //Подсчеты для validatedString 

            var dCount = 0;
            var MCount = 0;

            foreach (var format in formats)
            {
                for (var i = 0; i <format.Length; i++)
                {

                    if (format[i].ToString() == "d")
                    {
                        dCount++;
                    }

                    if (format[i].ToString() == "M")
                    {
                        MCount++;
                    }
                }

                return DeleteUslessElements(formats, dCount, MCount, character);
            }

               

            //Проверка только на d и M. Макс. количество = 2. Если 3, то уже возвращай formats или пропуск. 



            return formats;
        }

        private static List<string> DeleteUslessElements(List<string> formats, int dCount,int MCount, string character)
        {
            List<string> resultList = new List<string>();
    
            //Убираем ненужные элементы 
            foreach (var format in formats)
            {
                var dCheck = format.Split("d").Length-1;
                var MCheck = format.Split("M").Length - 1;


             

            }
            return resultList;
        }
    }
}
