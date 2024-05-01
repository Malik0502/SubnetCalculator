namespace Service
{
    public class AsymSubnetCalculator
    {
        private SubnetCalcHelper helper = new();
        private List<string> resultAsymCalc = new();

        public void ShowAvailableAsymSubnets(AsymSubnetEntity inputEntity)
        {
            int counter = 1;
            foreach (string subnet in CalcAvailableAsymSubnets(inputEntity))
            {
                string asyncSubnetAsDecimal = helper.BinaryToString(subnet);
                Console.WriteLine(asyncSubnetAsDecimal);
                if(counter % 2 == 0)
                {
                    Console.WriteLine("");
                }
                counter++;
            }
        }

        public List<string> CalcAvailableAsymSubnets(AsymSubnetEntity inputEntity)
        {
            string iPAdressBinary = inputEntity.IPAdress;
            int subnetAmount = inputEntity.SubnetAmount;

            List<int> hostAmount = inputEntity.HostAmount;
            char[] binaryAsChar = Array.Empty<char>();


            if (hostAmount.Count != 0)
            {
                hostAmount.Sort();
                hostAmount.Reverse();

                int hosts = hostAmount[0];

                // Berechnet mithilfe von neededHosts, die benötigten Hostbits und daraufhin die Subnetzmaske
                int neededHosts = helper.GetMinNeededHosts(hosts);
                int hostbit = Convert.ToInt32(helper.CalcLogarithmus(neededHosts));

                string subnetmaskBinary = CalcSubnetmask(hostbit);
                int amountOnesMask = helper.CountOnesInSubnetMask(subnetmaskBinary);

                // Berechnet die Netzwerkadresse und teilt die einzelnen Zeichen in einem Array auf
                string networkadress = helper.CalcNetworkAdressBinary(iPAdressBinary, subnetmaskBinary);
                char[] networkAdressAsChars = helper.StringToCharArray(networkadress);
                
                string subnet = "";

                for (int i = 0; i < neededHosts + 1; i++)
                {
                    if(i != neededHosts)
                    {
                        // Baut den ersten Teil der Ip-Adresse
                        // Baut den Teil so lang wie Einsen in der Subnetzmaske vorhanden sind.
                        int posCounter;
                        for (posCounter = 0; posCounter < amountOnesMask; posCounter++)
                        {
                            if ((posCounter + 1) % 8 == 0) subnet += networkAdressAsChars[posCounter] + ".";

                            else subnet += networkAdressAsChars[posCounter];
                        }

                        // Wichtigster Teil
                        // Konvertiert den Zähler i ins Binäre System.
                        // Dann wird durch die Zahl als Chars durch iteriert
                        string binary = Convert.ToString(i, 2).PadLeft(Convert.ToInt32(hostbit), '0');
                        binaryAsChar = binary.ToCharArray();
                        foreach (var item in binaryAsChar)
                        {
                            if ((networkAdressAsChars.Length - posCounter) % 8 == 0)
                            {
                                subnet += item + ".";
                            }
                            else
                            {
                                subnet += item;
                            }
                            posCounter++;
                        }

                        // Fügt am Ende die restlichen Nullen hinzu, falls nötig
                        for (int binaryRest = amountOnesMask + Convert.ToInt32(hostbit); binaryRest < networkAdressAsChars.Length; binaryRest++)
                        {
                            if (binaryRest % 8 == 0)
                            {
                                subnet += networkAdressAsChars[binaryRest] + ".";
                            }
                            else
                            {
                                subnet += networkAdressAsChars[binaryRest];
                            }
                        }
                        // Fügt nur wichtige Ip-Adressen in die Liste hinzu. 
                        // Dazu gehört die erste Adresse nach der Netzwerkadresse
                        // Die letzte Adresse vor der Broadcastadresse
                        // Sowie die Broadcastadresse
                        if (i == 1 || i == neededHosts - 1 || i == neededHosts - 2 )
                        {
                            resultAsymCalc.Add(subnet);
                            subnet = "";
                        }
                        else subnet = "";
                    }
                    // Wenn i gleich neededHost ist wird die Netzwerkadresse des nächsten Netzes berechnet
                    // Dafür benutzen wir die Broadcastadresse und erhöhen diese um 1
                    // Dabei entsteht die Netzwerkadresse fürs nächste Subnetz
                    else
                    {
                        resultAsymCalc.Add(helper.IncrementIpAdress(resultAsymCalc.Last(), amountOnesMask));
                        subnet = "";
                    }  
                }
                
                // Die erste Stelle aus hostAmount wird entfernt um eine Abbruchbedingung für die Rekursive Funktion zu haben
                hostAmount.RemoveAt(0);

                // Erstellt ein neues asymSubnetEntity mit den neuen veränderten Werten
                AsymSubnetEntity asymSubnetEntity = new AsymSubnetEntity()
                {
                    IPAdress = resultAsymCalc.Last(),
                    SubnetAmount = subnetAmount - 1,
                    HostAmount = hostAmount,
                };

                // Entfernt die Netzwerkadresse des nächsten Subnetzes und die Broadcastadresse aus der Liste
                // Dies sorgt dafür, dass wir am Ende eine Liste zurückbekommen die den nutzbaren Adressraum darstellt
                resultAsymCalc.RemoveAt(resultAsymCalc.Count - 1);
                resultAsymCalc.RemoveAt(resultAsymCalc.Count - 1);

                // Ruft die Funktion mit den veränderten Werten erneut auf
                CalcAvailableAsymSubnets(asymSubnetEntity);

            }
            else
            {
                return resultAsymCalc;
            }
            return resultAsymCalc;
        }

        public string CalcSubnetmask(double hostbits)
        {
            // Berechnet die Anzahl an Einsen die nötig sind
            // Hostbits = Anzahl Nullen in Subnetzmaske - 32
            int amountOfOnes = 32 - Convert.ToInt32(hostbits);
            string subnetmask = ""; 

            // Setzt die Subnetzmaske in Binär zusammen. Wenn I <= Anzahl Einsen dann wird eine 1 gesetzt
            // Sonst eine 0
            // Falls i Modulo 8 = 0 ergibt, dann ist das Ende eines Oktets erreicht. D.h. Punkt muss gesetzt werden
            for (int i = 1; i <= 32; i++)
            {
                if(i <= amountOfOnes)
                {
                    if (i % 8 == 0 && i != 32)
                    {
                        subnetmask += "1" + ".";
                    }
                    else
                    {
                        subnetmask += "1";
                    }
                }
                else
                {
                    if (i % 8 == 0 && i != 32)
                    {
                        subnetmask += "0" + ".";
                    }
                    else
                    {
                        subnetmask += "0";
                    }
                }
            }
            return subnetmask;
        }
    }
}
