using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        private int PowerNumber(int currentNumber, int digit, int bit)
        {
            Console.WriteLine(currentNumber);
            return checked(currentNumber + digit * (int)Math.Pow(10, bit));
        }

        public int Parse(string stringValue)
        {
            if (stringValue is null)
            {
                throw new ArgumentNullException("stringValue");
            }

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                throw new FormatException("stringValue is null or empty");
            }

            if (stringValue.Equals("-2147483648"))
            {
                return int.MinValue;
            }
            else if (stringValue.Equals("2147483647"))
            {
                return int.MaxValue;
            }

            var number = 0;
            var bit = 0;
            for (int i = stringValue.Length - 1; i >= 0; i--)
            {
                switch (stringValue[i])
                {
                    case '0':
                        bit++;
                        break;
                    case '1':
                        number = PowerNumber(number, 1, bit++);
                        break;
                    case '2':
                        number = PowerNumber(number, 2, bit++);
                        break;
                    case '3':
                        number = PowerNumber(number, 3, bit++);
                        break;
                    case '4':
                        number = PowerNumber(number, 4, bit++);
                        break;
                    case '5':
                        number = PowerNumber(number, 5, bit++);
                        break;
                    case '6':
                        number = PowerNumber(number, 6, bit++);
                        break;
                    case '7':
                        number = PowerNumber(number, 7, bit++);
                        break;
                    case '8':
                        number = PowerNumber(number, 8, bit++);
                        break;
                    case '9':
                        number = PowerNumber(number, 9, bit++);
                        break;
                    case '-':
                        if (i != 0)
                        {
                            throw new FormatException("Char '-' is in the middle of the variable");
                        }
                        number *= -1;
                        break;
                    case '+':
                        if (i != 0)
                        {
                            throw new FormatException("Char '+' is in the middle of the variable");
                        }
                        break;
                    case ' ':
                        break;
                    default:
                        throw new FormatException("There is no defined digit for this char: " + stringValue[i]);
                }
            }

            return number;
        }
    }
}