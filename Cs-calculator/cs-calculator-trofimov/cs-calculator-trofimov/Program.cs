namespace cs_calculator_trofimov
{
    internal class Program
    {

        public static bool Check_If_Number_Is_Correct(string number)
        {
            string All_Possible_Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // Все цифры которые могут быть в системах до 36

            foreach (char digit in number)
            {
                if (!All_Possible_Digits.Contains(digit)) // Если данный символ не является одним из нужных, то значит что пользователь дал нам некорректное число
                {
                    return false;
                }
            }

            return true;
        }
        public static string Convert_Number_To_Different_Numeral_System(string number, int starting_system, int needed_system)
        {
            if (starting_system == needed_system)  // Проверяем одинаковые ли системы, в случае если одинаковые то пропускаем все вычисления и выводим то же самое число
            {
                return number;
            }

            int number_in_decimal = 0;

            // Перевод будем делать при помощи деления, поэтому нам нужно чтобы наше число было в десятичной системе

            if (starting_system != 10) // Если число изначально не в десятичной, то мы переводим его в десятичную
            {
                // При переводе в десятичную систему, мы нумеруем все цифры в числе с конца начиная с 0 и заканчивая количеством цифр - 1
                // После этого каждую цифру числа мы умножаем на изначальную систему счисления в степени нумерации этой цифры и все это складываем
                // Например возьмем число A23 в 11-ричной системе. 3 присвоим номер 0, 2 - 1, A (10) - 2.
                // Умножаем и складываем: 10 * 11^2 + 2 * 11^1 + 3 * 11^0 = 10 * 121 + 2 * 11 + 3 * 1 = 1210 + 22 + 3 = 1235

                int counter = number.Length - 1; // Так как тут мы идем с начала числа, а не с конца, то и отсчет мы ведем не с 0, а с последней возможной нумерации
                foreach (char digit in number)
                {
                    if (Char.IsLetter(digit)) // Если наша цифра записывается как буква, то чтобы получить из нее число в C# нам нужно воспользоваться значением в кодировке ASCII
                    {
                        // Запись (int)char даст нам значение данного символа в кодировке ASCII
                        // "A" в кодировке ASCII имеет значение 65, но как цифра она имеет значение 10
                        // Следовательно чтобы получить из буквы нужную нам цифру, то нужно вычесть из значения ASCII число 55

                        number_in_decimal += ((int)digit - 55) * (int)Math.Pow(starting_system, counter); // Умножаем цифру на систему в степени нумерации
                    }
                    else // Если наша цифра обычная, то мы также используем кодировку ASCII
                    {
                        // Теперь мы отталкиваемся от того что 0 - 48, 1 - 49, 2 - 50,...,9 - 57
                        // Следовательно чтобы получить нужную нам цифру, то нужно вычесть из значения ASCII число 48
                        number_in_decimal += ((int)digit - 48) * (int)Math.Pow(starting_system, counter); // Умножаем цифру на систему в степени нумерации
                    }

                    counter--; // Для следующей цифры нумерация будет на 1 меньше
                }
            }
            else // Если же наше число уже в десятичной, то для дальнейших действий мы переводим его из типа строки в тип числа
            {
                number_in_decimal = Convert.ToInt32(number);
            }

            if (needed_system == 10) // После преобразования числа мы уже получаем число в десятичной, поэтому мы больше не делаем действий, а просто возвращаем получившийся результат
            {
                return number_in_decimal.ToString();
            }

            string result = "";


            // При переводе числа при помощи деления, мы будем делить исходное число на систему счисления, остаток записывать в конец числа, а результат ещё раз делить на систему счисления до тех пор, пока результат не будет цифрой в нужной нам системе счисления
            while (number_in_decimal > needed_system) // Если число больше системы счисления, то оно не может быть цифрой в этой системе, значит делим дальше
            {
                int remainder = number_in_decimal % needed_system; // Получаем остаток от деления, который и запишем в конец нового числа

                if (remainder >= 10) // Если этот остаток больше 9, то его нужно записать при помощи буквы 
                {
                    result += (char)(remainder + 55); // Буквы начинаются с A - 65, минимальный возможный остаток в данном случае 10, значит мы прибавляем к значению ASCII 65-10=55
                }
                else
                {
                    result += (remainder); // Если остаток меньше 10, то его просто записываем в конец
                }
       
                number_in_decimal /= needed_system; // Получаем результат деления без остатка, которое мы продолжаем делить если оно все ещё не может быть цифрой 
            }

            // Цикл продолжается до того момента, пока число не сможет стать цифрой переведенного числа, из за этого последняя цифра должна быть добавлена вне цикла

            if (number_in_decimal >= 10)
            {
                result += (char)(number_in_decimal + 55);
            }
            else
            {
                result += number_in_decimal;
            }

            return new string(result.Reverse().ToArray()); // Так как мы при делении мы записываем результат в конец, но в программе мы добавляли их в начало числа, поэтому возвращаем перевернутое число
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
            string desired_numeral_system_buffer = Console.ReadLine();
            int desired_numeral_system = 0;
            bool Is_This_A_Number = int.TryParse(desired_numeral_system_buffer, out desired_numeral_system);
            bool Is_Users_Number_Possible_In_This_System = false;


            foreach (char number in users_number)
            {
                if ("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(0, desired_numeral_system).Contains(number))
                {
                    Is_Users_Number_Possible_In_This_System = true;
                }
                else
                {
                    Is_Users_Number_Possible_In_This_System = false;
                    break;
                }
            }

            while (!Is_This_A_Number || desired_numeral_system > 36 || !Is_Users_Number_Possible_In_This_System)
            {
                Console.Write("Ой-ой! Что-то не так с системой счисления. Напишите систему используя только десятичные числа. Максимальная система счисления для перевода - 36.\nПопробуйте ещё раз: ");
                desired_numeral_system_buffer = Console.ReadLine();
                Is_This_A_Number = int.TryParse(desired_numeral_system_buffer, out desired_numeral_system);
                foreach (char number in users_number)
                {
                    if ("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(0, desired_numeral_system).Contains(number))
                    {
                        Is_Users_Number_Possible_In_This_System = true;
                    }
                    else
                    {
                        Is_Users_Number_Possible_In_This_System = false;
                        break;
                    }
                }
            }

            Console.Write("Введите в какую систему счисления вы хотите перевести: ");
            string desired_system_buffer = Console.ReadLine();
            int desired_system;
            Is_This_A_Number = int.TryParse(desired_system_buffer, out desired_system);
            while (!Is_This_A_Number || desired_system > 36)
            {
                Console.Write("Ой-ой! Что-то не так с системой счисления. Напишите систему используя только десятичные числа. Максимальная система счисления для перевода - 36.\nПопробуйте ещё раз: ");
                desired_system_buffer = Console.ReadLine();
                Is_This_A_Number = int.TryParse(desired_system_buffer, out desired_system);
            }

            Console.WriteLine(Convert_Number_To_Different_Numeral_System(users_number, desired_numeral_system, desired_system));

        }
    }
}
