using Service.Interfaces;

namespace Service
{
    public class UserInteraction : IUserInteraction
    {
        private readonly IParser parser;
        private readonly IAsymCalculator asymCalculator;
        private readonly ICalculator calculator;

        public UserInteraction(IParser parser, IAsymCalculator asymCalculator, ICalculator calculator) 
        {
            this.parser = parser;
            this.asymCalculator = asymCalculator;
            this.calculator = calculator!;
        }

        /// <summary>
        /// Zeigt alle berechneten Subnetze in der Konsole an
        /// </summary>
        /// <param name="inputEntity"></param>
        public void ShowAvailableAsymSubnets(AsymSubnetEntity inputEntity)
        {
            int counter = 1;
            foreach (string subnet in asymCalculator.CalcAvailableAsymSubnets(inputEntity))
            {
                string asyncSubnetAsDecimal = parser.BinaryToString(subnet);
                Console.WriteLine(asyncSubnetAsDecimal);
                if (counter % 2 == 0) Console.WriteLine("");

                counter++;
            }
        }

        /// <summary>
        /// Zeigt alle Subnetze an, die berechnet werden in Dezimalformat an
        /// </summary>
        /// <param name="inputEntity"></param>
        public void ShowAvailableSubnets(SubnetEntity inputEntity)
        {
            if (!ValidateUserInput(inputEntity))
            {
                Console.WriteLine("Ihre Eingaben haben das falsche Format");
                Environment.Exit(0);
            }
            foreach (string subnet in calculator.CalcAvailableSubnets(inputEntity))
            {
                string subnetAsDecimal = parser.BinaryToString(subnet);
                Console.WriteLine(subnetAsDecimal);
            }
        }

        /// <summary>
        /// Prüft ob die Eingaben des Nutzers die geeignete Länge für eine Mögliche Ip Adresse bzw. Subnetzmaske hat 
        /// <para>
        /// Eine Ip-Adresse / Subnetzmaske mitsamt Punkten kann mindestens 7 und maximal 15 Zeichen beinhalten
        /// Ebenfalls wenn eines der beiden 0 ist wird false ausgegeben 
        /// </para>
        /// </summary>
        /// <param name="inputEntity"></param>
        public bool ValidateUserInput(SubnetEntity inputEntity)
        {
            int inputIpAdressLength = inputEntity.IPAdress!.Length;
            int inputSubnetmaskLength = inputEntity.SubnetMask!.Length;

            return (inputIpAdressLength > 15 || inputIpAdressLength < 7 || inputSubnetmaskLength > 15 || inputSubnetmaskLength < 7 || inputIpAdressLength == 0 || inputSubnetmaskLength == 0)
                ? false : true;
        }
    }
}
