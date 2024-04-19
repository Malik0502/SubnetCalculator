﻿using System.Collections;
using System.Globalization;
using System.Numerics;
using System.Xml.XPath;

namespace Service
{
    public class SubnetCalculator
    {
        public void ShowAvailableSubnets(string ipAdress, string subnetmask, int subnetAmount)
        {

        }


        private void CalculateAvailableSubnets(string ipAdress, string subnetmask, int subnetAmount)
        {
            string ipAdressInBinary = StringToBinaryString(ipAdress);
            string subnetmaskInBinary = StringToBinaryString(subnetmask);
        }

        public string StringToBinaryString(string stringToConvert)
        {
            string[] splittedAdress = SplitString(stringToConvert);
            ArrayList AdressInBinaryCode = new ArrayList();
            try
            {
                foreach (var octet in splittedAdress)
                {
                    int parsedOctet = int.Parse(octet);
                    string partialResult = "";

                    for (int i = parsedOctet; i > 0; i/=2){
                        partialResult += parsedOctet % 2;
                        parsedOctet /= 2;
                    }

                    partialResult = FillUpWithZeros(partialResult);
                    partialResult = ReverseString(partialResult);
                    AdressInBinaryCode.Add(partialResult);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Die IP-Adresse hat das falsche Format: {ex}");
            }
            
            return string.Join(".", AdressInBinaryCode.ToArray());
        }

        // Nicht fertig
        // Bis jetzt wird eins der 4 Oktette genommen.
        // Dort werden die acht Zahlen einzeln gespeichert
        // Diese werden dann in einem Dicitonary von 128 bis 1 gespeichert
        // Gerade angefangen Berechnung von Binär zu machen
        // Result += Key * Value (Sollte zur Zahl werden. Konnte noch nicht getestet werden)
        public string BinaryToString(string binaryToConvert)
        {
            string[] splittedBinary = SplitString(binaryToConvert);
            int result = 0;
            foreach (string octet in splittedBinary)
            {
                Dictionary<int, int> conversionTable = new Dictionary<int, int>();
                
                char[] singleNumsFromOctet = octet.ToCharArray();
                CharToInt(singleNumsFromOctet, conversionTable);

                foreach (var item in conversionTable)
                {
                    result += item.Key * item.Value;
                }
            }
            return "";
        }

        private string[] SplitString(string ipAdress){
            string[] splittedString = ipAdress.Split(".");
            return splittedString;
        } 

        private string ReverseString(string stringToReverse){
            return new string(stringToReverse.Reverse().ToArray());
        }

        private string FillUpWithZeros(string stringToFillUp)
        {
            for(int i = stringToFillUp.Count(); i < 8; i++)
            {
                stringToFillUp += 0;
            }

            return stringToFillUp;
        }

        private void CharToInt(char[] charToConvert, Dictionary<int, int> conversionTable){
            int bitCounter = 128;
            int num = 0;
            foreach (var numAsChar in charToConvert)
            {
                string charToString = numAsChar.ToString();
                num = int.Parse(charToString);
                conversionTable.Add(bitCounter, num);
                bitCounter /= 2;
            }
        }
    }
}