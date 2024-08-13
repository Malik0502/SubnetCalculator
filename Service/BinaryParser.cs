using Service.Interfaces;
using System.Collections;

namespace Service
{
    public class BinaryParser : IParser
    {
        private readonly IBinaryString binaryString;

        public BinaryParser(IBinaryString binaryString){
            this.binaryString = binaryString;
        }

        /// <summary>
        /// Nimmt einen String im Binärformat und konvertiert diesen zu String im Dezimalformat
        /// </summary>
        /// <param name="stringToConvert"></param>
        /// <returns></returns>
        public string BinaryToString(string binaryToConvert)
        {
            BinaryParser parser = new(binaryString);
            SubnetCalcHelper helper = new(parser);

            string[] splittedBinary = binaryToConvert.Split(".");
            int partialResult;
            string result = "";
            int counter = 0;

            // Key = Vielfaches von 2 , 1 bis 128
            // Value = Binärzahl vom Oktett von vorne nach hinten
            foreach (string octet in splittedBinary)
            {
                partialResult = 0;
                Dictionary<int, int> conversionTable = new Dictionary<int, int>();

                helper.AddConvertedCharToDic(octet.ToCharArray(), conversionTable);

                foreach (var item in conversionTable)
                {
                    partialResult += item.Key * item.Value;
                }

                // Fügt die passenden Punkte hinzu um das Format der Ip-Adresse zu erfüllen
                result += (counter != splittedBinary.Length - 1) ? partialResult + "." : partialResult;

                counter++;

            }
            return result;
        }

        /// <summary>
        /// Nimmt einen String als Eingabe und gibt einen String in Form von Binärcode zurück
        /// </summary>
        /// <param name="binaryToConvert"></param>
        /// <returns></returns>
        public string StringToBinary(string stringToConvert)
        {
            string[] splittedAdress = stringToConvert.Split(".");
            ArrayList AdressInBinary = new ArrayList();
            try
            {
                // Iteriert durch die 4 Oktette und nutzt Modulo um den Rest herauszufinden
                // Dieser ist dann eine Stelle des Binärcodes
                foreach (var octet in splittedAdress)
                {
                    int parsedOctet = int.Parse(octet);
                    string partialResult = "";

                    for (int i = parsedOctet; i > 0; i /= 2)
                    {
                        partialResult += parsedOctet % 2;
                        parsedOctet /= 2;
                    }

                    partialResult = binaryString.FillUpWithZeros(partialResult).ReverseString();
                    AdressInBinary.Add(partialResult);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Die IP-Adresse hat das falsche Format: {ex}");
            }

            return string.Join(".", AdressInBinary.ToArray());
        }

        /// <summary>
        /// Konvertiert Strings zu einem Array aus einzelnen Chars.
        /// </summary>
        /// <param name="stringToConvert"></param>
        /// <returns></returns>
        public char[] StringToCharArray(string stringToConvert)
        {
            string[] splittedString = stringToConvert.Split(".");
            char[] chars = new char[32];

            for (int i = 0; i < splittedString.Length; i++)
            {
                char[] splittedStringAsCharArray = splittedString[i].ToCharArray();
                splittedStringAsCharArray.CopyTo(chars, splittedStringAsCharArray.Length * i);
            }

            return chars;
        }
    }
}
