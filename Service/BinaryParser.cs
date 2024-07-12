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
            SubnetCalcHelper helper = new();

            string[] splittedBinary = binaryToConvert.Split(".");
            int partialResult;
            string result = "";
            int counter = 0;

            // Iteriert durch den splitted String Array 
            // Es wird ein leeres Dictionary erstellt um die Umrechnung durchzuführen
            // Das Dictionary wird mit den einzelnen Chars von einem Oktett gefüllt
            // Key = Vielfaches von 2 , 1 bis 128
            // Value = Binärzahl vom Oktett von vorne nach hinten
            foreach (string octet in splittedBinary)
            {
                partialResult = 0;
                Dictionary<int, int> conversionTable = new Dictionary<int, int>();

                char[] singleNumsFromOctet = octet.ToCharArray();
                helper.AddConvertedCharToDic(singleNumsFromOctet, conversionTable);

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

                    // Das unfertige Ergebnis wird mit den fehlenden Nullen aufgefüllt.
                    // Dann wird dieser umgedeht und in eine Liste hinzugefügt
                    // Dabei entsteht dann die richtige Zahl im Binärformat
                    partialResult = binaryString.FillUpWithZeros(partialResult);
                    partialResult = partialResult.ReverseString();
                    AdressInBinary.Add(partialResult);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Die IP-Adresse hat das falsche Format: {ex}");
            }

            // Die einzelnen Oktete in Binär werden dann mit einem . zwischen den einzelnen Indexen als String zusammengefügt
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
