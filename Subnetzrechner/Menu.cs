using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetzrechner
{
    public class Menu
    {
        public void StartMenu()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("1. Subnetzrechner");
                    Console.WriteLine("2. Asymmetrischer Subnetzrechner");
                    int menuInput = int.Parse(Console.ReadLine());
                    
                    Console.Clear();

                    switch (menuInput)
                    {
                        case 1:
                            Console.WriteLine("IP-Adresse:");
                            string ipAdressInput = Console.ReadLine() ?? string.Empty;

                            Console.WriteLine("Subnetzmaske:");
                            string subnetmaskInput = Console.ReadLine() ?? string.Empty;

                            Console.WriteLine("Anzahl Subnetze:");
                            int subnetAmountInput = int.Parse(Console.ReadLine());

                            SubnetEntity inputEntity = new()
                            {
                                IPAdress = ipAdressInput,
                                SubnetMask = subnetmaskInput,
                                SubnetAmount = subnetAmountInput
                            };

                            Console.Clear();

                            new SubnetCalculator().ShowAvailableSubnets(inputEntity);

                            break;

                        case 2:
                            List<int> inputHostAmount = new List<int>();

                            Console.WriteLine("IP-Adresse:");
                            string inputIpAdress = Console.ReadLine() ?? string.Empty;

                            Console.WriteLine("Anzahl Subnetze:");
                            int inputSubnetAmount = int.Parse(Console.ReadLine());


                            for (int i = 0; i < inputSubnetAmount; i++)
                            {
                                Console.WriteLine($"Anzahl Host für Subnetz {i + 1}");
                                int input = int.Parse(Console.ReadLine());
                                inputHostAmount.Add(input);
                                Console.Clear();
                            }

                            AsymSubnetEntity asymInputEntity = new()
                            {
                                IPAdress = inputIpAdress,
                                SubnetAmount = inputSubnetAmount,
                                HostAmount = inputHostAmount,
                            };

                            new AsymSubnetCalculator().CalcAvailableAsymSubnets(asymInputEntity);

                            break;
                    }


                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bitte überprüfen Sie ihre Werte");
                    Console.WriteLine($"Fehlercode: {ex}");
                }
            }
        }
    }
}
