using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if (stringValue is null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                throw new FormatException();
            }

            if (stringValue.Equals("-2147483648"))
            {
                return int.MinValue;
            }
            else if (stringValue.Equals("2147483647"))
            {
                return int.MaxValue;
            }

            Int32 number = 0;
            int bit = 0;
            try
            {
                for (int i = stringValue.Length - 1; i >= 0; i--)
                {
                    switch (stringValue[i])
                    {
                        case '0':
                            bit++;
                            break;
                        case '1':
                            number = checked(number + 1 * (int)Math.Pow(10, bit++));
                            break;
                        case '2':
                            number = checked(number + 2 * (int)Math.Pow(10, bit++));
                            break;
                        case '3':
                            number = checked(number + 3 * (int)Math.Pow(10, bit++));
                            break;
                        case '4':
                            number = checked(number + 4 * (int)Math.Pow(10, bit++));
                            break;
                        case '5':
                            number = checked(number + 5 * (int)Math.Pow(10, bit++));
                            break;
                        case '6':
                            number = checked(number + 6 * (int)Math.Pow(10, bit++));
                            break;
                        case '7':
                            number = checked(number + 7 * (int)Math.Pow(10, bit++));
                            break;
                        case '8':
                            number = checked(number + 8 * (int)Math.Pow(10, bit++));
                            break;
                        case '9':
                            number = checked(number + 9 * (int)Math.Pow(10, bit++));
                            break;
                        case '-':
                            if (i != 0)
                            {
                                throw new FormatException();
                            }
                            number *= -1;
                            break;
                        case '+':
                            if (i != 0)
                            {
                                throw new FormatException();
                            }
                            break;
                        case ' ':
                            break;
                        default:
                            throw new FormatException();
                    }
                }
            }
            catch (FormatException)
            {
                throw new FormatException();
            }
            catch (Exception)
            {
                throw new OverflowException();
            }

            return number;
        }
    }
}