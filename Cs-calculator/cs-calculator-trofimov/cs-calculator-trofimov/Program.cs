namespace cs_calculator_trofimov
{
    internal class Program
    {

        public static bool Check_If_Number_Is_Correct(string number)
        {
            string All_Possible_Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            foreach (char digit in number)
            {
                if (!All_Possible_Digits.Contains(digit))
                {
                    return false;
                }
            }

            return true;
        }
        public static string Convert_Number_To_Different_Numeral_System(string number, int starting_system, int needed_system)
        {

            int number_in_decimal = 0;

            if (starting_system != 10)
            {
                int counter = number.Length - 1;
                foreach (char digit in number)
                {
                    if (Char.IsLetter(digit))
                    {
                        number_in_decimal += ((int)digit - 35) * (int)Math.Pow(starting_system, counter);
                    }
                    else
                    {
                        number_in_decimal += (int)digit * (int)Math.Pow(starting_system, counter);
                    }
                    counter--;
                }
            }
            else
            {
                number_in_decimal = Convert.ToInt32(number);
            }

            if (needed_system == 10)
            {
                return number_in_decimal.ToString();
            }

            string result = "";

            while (number_in_decimal > needed_system)
            {
                int remainder = number_in_decimal % needed_system;

                if (remainder >= 10)
                {
                    result += (char)(remainder + 55);
                }
                else
                {
                    result += (remainder);
                }
       
                number_in_decimal /= needed_system;
            }

            if (number_in_decimal >= 10)
            {
                result += (char)(number_in_decimal + 55);
            }
            else
            {
                result += number_in_decimal;
            }

            return new string(result.Reverse().ToArray());
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Калькулятор перевода систем счисления");
            Console.Write("Введите какое число вы хотите перевести: ");
            string users_number = Console.ReadLine();
            bool Is_Number_Correct = Check_If_Number_Is_Correct(users_number);
            while (!Is_Number_Correct)
            {
                Console.Write("Ой-ой! Что-то не так с числом. Возможно вы ввели неправильный символ или использовали незаглавную букву. Попробуйте ещё раз: ");
                users_number = Console.ReadLine();
                Is_Number_Correct = Check_If_Number_Is_Correct(users_number);
            }
            Console.Write("Введите в какой системе счисления ваше число: ");
            string desired_numbers_system_buffer = Console.ReadLine();


            Console.Write("Введите в какую систему счисления вы хотите перевести: ");
            string desired_system_buffer = Console.ReadLine();

        }
    }
}
