using Service.Interfaces;

namespace Service
{
    public class BinaryStringModificator : IBinaryString
    {  
        public int CountOnesInSubnetMask(string subnetmaskBinary)
        {
            IBinaryString binaryString = new BinaryStringModificator
    ();
            IParser parser = new BinaryParser(binaryString);
            char[] subnetmaskAsChars = parser.StringToCharArray(subnetmaskBinary);
            int counter = 0;
            foreach (var binaryNum in subnetmaskAsChars)
            {
                if (binaryNum == '1') counter++;
            }
            return counter;
        }
        
        public string FillUpWithZeros(string stringToFillUp)
        {
            for (int i = stringToFillUp.Count(); i < 8; i++)
            {
                stringToFillUp += 0;
            }
            return stringToFillUp;
        }

        public string IncrementIpAdress(string iPAdress, int startIndex)
        {
            // Teilt den String auf
            string[] splittedString = iPAdress.Split(".");
            int partIndex = startIndex / 8;
            string result = "";

            // Konvertiert zu Dezimalzahl
            int partAsDecimal = Convert.ToInt32(splittedString[partIndex], 2);
            partAsDecimal++;

            // Wenn Zahl == 255,
            // dann wird Oktett, dass vor dem erhöhten Oktett steht um eins erhöht
            // partAsDecimal wird dann auf 0 gesetzt
            if (partAsDecimal >= 255)
            {
                partAsDecimal = 0;
                int partBeforeConverted = Convert.ToInt32(splittedString[partIndex - 1], 2);
                partBeforeConverted++;
                splittedString[partIndex - 1] = Convert.ToString(partBeforeConverted, 2).PadLeft(8, '0');
                splittedString[partIndex] = Convert.ToString(partAsDecimal, 2).PadLeft(8, '0');
            }
            else {
                splittedString[partIndex] = Convert.ToString(partAsDecimal, 2).PadLeft(8, '0');
            }

            // Setzt alles wieder zu einem String zusammen
            foreach (string item in splittedString)
            {
                result += (item != splittedString.Last()) ? item + "." : item;
            }
            
            return result;
        }
    }
}