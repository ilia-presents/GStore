using System.Text;

namespace GStore.Utils.Helpers
{
    public static class LetterConvertor
    {
        public static string ConvertBgToLatin(string strSearch)
        {
            int intStrLenght = strSearch.Length;

            StringBuilder buff = new StringBuilder();

            intStrLenght = intStrLenght - 1;

            char charTemp = ' ';

            for (int i = 0; i <= intStrLenght; i++)
            {
                charTemp = strSearch[i];

                if (char.IsLetter(charTemp))
                {
                    if (char.IsUpper(charTemp))

                        buff.Append(convertCapitalBulgarianCharToLatin(charTemp));
                    else

                        buff.Append(convertBulgarianCharToLatin(charTemp));

                }
                else if (char.IsDigit(charTemp))
                    buff.Append(charTemp);

                else if (char.IsWhiteSpace(charTemp))
                    buff.Append("_");
            }

            return buff.ToString();
        }


        private static string convertBulgarianCharToLatin(char charTemp)
        {
            string strReturn = "";
            switch (charTemp)
            {
                case 'а':

                    strReturn = "a";
                    break;


                case 'б':

                    strReturn = "b";
                    break;


                case 'в':

                    strReturn = "v";
                    break;


                case 'г':

                    strReturn = "g";
                    break;


                case 'д':

                    strReturn = "d";
                    break;


                case 'е':

                    strReturn = "e";
                    break;


                case 'ж':

                    strReturn = "zh";
                    break;


                case 'з':

                    strReturn = "z";
                    break;


                case 'и':

                    strReturn = "i";
                    break;


                case 'й':

                    strReturn = "j";
                    break;


                case 'к':

                    strReturn = "k";
                    break;


                case 'л':

                    strReturn = "l";
                    break;


                case 'м':

                    strReturn = "m";
                    break;


                case 'н':

                    strReturn = "n";
                    break;


                case 'о':

                    strReturn = "o";
                    break;


                case 'п':

                    strReturn = "p";
                    break;


                case 'р':

                    strReturn = "r";
                    break;


                case 'с':

                    strReturn = "s";
                    break;


                case 'т':

                    strReturn = "t";
                    break;


                case 'у':

                    strReturn = "u";
                    break;


                case 'ф':

                    strReturn = "f";
                    break;


                case 'х':

                    strReturn = "h";
                    break;


                case 'ц':

                    strReturn = "tz";
                    break;


                case 'ч':

                    strReturn = "ch";
                    break;


                case 'ш':

                    strReturn = "sh";
                    break;


                case 'щ':

                    strReturn = "sht";
                    break;


                case 'ъ':

                    strReturn = "a";
                    break;


                case 'ь':

                    strReturn = "i";
                    break;


                case 'ю':

                    strReturn = "iu";
                    break;


                case 'я':

                    strReturn = "ia";
                    break;


                default:

                    strReturn = charTemp.ToString();
                    break;

            }

            return strReturn;
        }

        private static string convertCapitalBulgarianCharToLatin(char charTemp)
        {
            string strReturn = "";
            switch (charTemp)
            {
                case 'А':

                    strReturn = "A";
                    break;

                case 'Б':

                    strReturn = "B";
                    break;

                case 'В':

                    strReturn = "V";
                    break;

                case 'Г':

                    strReturn = "G";
                    break;

                case 'Д':

                    strReturn = "D";
                    break;

                case 'Е':

                    strReturn = "E";
                    break;

                case 'Ж':

                    strReturn = "Zh";
                    break;

                case 'З':

                    strReturn = "Z";
                    break;

                case 'И':

                    strReturn = "I";
                    break;

                case 'Й':

                    strReturn = "J";
                    break;

                case 'К':

                    strReturn = "K";
                    break;

                case 'Л':

                    strReturn = "L";
                    break;

                case 'М':

                    strReturn = "M";
                    break;

                case 'Н':

                    strReturn = "N";
                    break;

                case 'О':

                    strReturn = "O";
                    break;

                case 'П':

                    strReturn = "P";
                    break;

                case 'Р':

                    strReturn = "R";
                    break;

                case 'С':

                    strReturn = "S";
                    break;

                case 'Т':

                    strReturn = "T";
                    break;

                case 'У':

                    strReturn = "U";
                    break;

                case 'Ф':

                    strReturn = "F";
                    break;

                case 'Х':

                    strReturn = "H";
                    break;

                case 'Ц':

                    strReturn = "Tz";
                    break;

                case 'Ч':

                    strReturn = "Ch";
                    break;

                case 'Ш':

                    strReturn = "Sh";
                    break;

                case 'Щ':

                    strReturn = "Sht";
                    break;

                case 'Ъ':

                    strReturn = "A";
                    break;

                case 'ь':

                    strReturn = "";
                    break;

                case 'Ю':

                    strReturn = "Iu";
                    break;

                case 'Я':

                    strReturn = "Ia";
                    break;

                default:
                    strReturn = charTemp.ToString();
                    break;
            }

            return strReturn;
        }
    }
}
