using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kalkulator
{
    public class Calculator : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string textBlockBinding)
        {
            PropertyChanged?.Invoke(this, new(textBlockBinding));
        }

        private string firstNumber = "";
        private string secondNumber = "";
        private string operation = "";
        private string percentageOperation = "";

        private string expression = "";
        private string result = "";


        public string UserInput
        {
            get 
            {
                if (operation == "")
                {
                    return firstNumber;
                }
                else if (secondNumber == "")
                {
                    return $"{firstNumber} {operation}";
                }
                else
                {
                    return $"{firstNumber} {operation} {secondNumber}{percentageOperation}";
                } 
            }
            set
            {
                firstNumber = value;
                secondNumber = value;
                operation = value;
                percentageOperation = value;
                OnPropertyChanged("UserInput");
            }
        }

        public string Expression
        {
            get
            {
                return expression;
            }
            set
            {
                expression = value;
                OnPropertyChanged("Expression");
            }
        }

        public string Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
                OnPropertyChanged("Result");
            }
        }


        public void AppendNumber(string number)
        {
            if (percentageOperation == "")
            {
                if (operation == "")
                {
                    if (!(number == "0" && firstNumber == "-0"))
                    {
                        if (firstNumber != "0" && firstNumber != "-0")
                        {
                            firstNumber += number;
                    }
                    }
                }
                else
                {
                    if (!(number == "0" && secondNumber == "-0"))
                    {
                        if (secondNumber != "0" && secondNumber != "-0")
                        {
                            secondNumber += number;
                        }
                    }
                }
            }
            OnPropertyChanged("UserInput");
        }

        public void AppendOperation(string operation_)
        {
            if (firstNumber.Length > 0 && secondNumber == "")
            {
                operation = operation_;
            }
            OnPropertyChanged("UserInput");
        }

        public void AppendPercentageOperation(string percentage)
        {
            if (secondNumber != "" && secondNumber != "-")
            {
                percentageOperation = percentage;
            }
            OnPropertyChanged("UserInput");
        }

        public void AppendComma(string comma)
        {
            if (percentageOperation == "")
            {
                if (operation == "")
                {
                    if (!firstNumber.Contains(comma) && firstNumber.Length > 0)
                    {
                        firstNumber += comma;
                    }
                }
                else
                {
                    if (!secondNumber.Contains(comma) && secondNumber.Length > 0)
                    {
                        secondNumber += comma;
                    }
                }
            }
            OnPropertyChanged("UserInput");
        }

        public void AppendMinus(string minus)
        {
            if (percentageOperation == "")
            {
                if (firstNumber == "" && !firstNumber.Contains(minus))
                {
                    firstNumber += minus;
                }
                else if (operation == "" && firstNumber.Last().ToString() != minus)
                {
                    operation = minus;
                }
                else if (secondNumber == "" && !secondNumber.Contains(minus))
                {
                    secondNumber += minus;
                }
            }
            OnPropertyChanged("UserInput");
        }

        public void ClearInput()
        {
            firstNumber = "";
            secondNumber = "";
            operation = "";
            Expression = "";
            Result = "";
            OnPropertyChanged("UserInput");
        }
        public void DeleteLastChar()
        {
            if (percentageOperation == "%")
            {
                percentageOperation = "";
            }
            else if (secondNumber.Length > 0)
            {
                secondNumber = secondNumber.Substring(0, secondNumber.Length - 1);
            }
            else if (operation != "")
            {
                operation = "";
            }
            else if (firstNumber.Length > 0)
            {
                firstNumber = firstNumber.Substring(0, firstNumber.Length - 1);
            }
            OnPropertyChanged("UserInput");
        }

        public void Calculate(string calculation_way)
        {
            if (calculation_way == "=")
            {
                if (firstNumber != "" && secondNumber != "")
                {
                    double res = CalculateWithTwoArguments();
                    Expression = UserInput;
                    UserInput = "";
                    if (res == -1)
                    {
                        Result = "Nie można dzielić przez zero";
                    }
                    else
                    {
                        Result = res.ToString();
                    }
                }
                else
                {
                    Result = "Niepoprawne dane";
                }
            }
            else
            {
                if (firstNumber != "" && operation == "" && secondNumber == "")
                {
                    double num1 = double.Parse(firstNumber);
                    double res;
                    switch (calculation_way)
                    { 
                        case "sqrt":
                            res = Math.Sqrt(num1);
                            Expression = $"sqrt({num1})";
                            UserInput = "";
                            Result = res.ToString();
                            break;
                        case "1/x":
                            res = 1 / num1;
                            Expression = $"1 / {num1}";
                            UserInput = "";
                            Result = res.ToString();
                            break;
                        case "!":
                            if (num1 <= 0 || firstNumber.Contains(","))
                            {
                                Expression = $"{num1}!";
                                UserInput = "";
                                Result = "Tylko liczba całkowita dodatnia";
                            }
                            else
                            {
                                res = 1;
                                for (int i = 1; i <= num1; i++)
                                {
                                    res *= i;
                                }
                                Expression = $"{num1}!";
                                UserInput = "";
                                Result = res.ToString();
                            }
                            break;
                        case "ln":
                            if (num1 <= 0)
                            {
                                Expression = $"ln({num1})";
                                UserInput = "";
                                Result = "Tylko liczba dodatnia";
                            }
                            else
                            {
                                res = Math.Log(num1);
                                Expression = $"ln({num1})";
                                UserInput = "";
                                Result = res.ToString();
                            }
                            break;
                        case "floor":
                            res = Math.Floor(num1);
                            Expression = $"floor({num1})";
                            UserInput = "";
                            Result = res.ToString();
                            break;
                        case "ceil":
                            res = Math.Ceiling(num1);
                            Expression = $"ceil({num1})";
                            UserInput = "";
                            Result = res.ToString();
                            break;
                    }
                }
                else
                {
                    Result = "Niepoprawne dane";
                }
            }
        }

        private double CalculateWithTwoArguments()
        {
            double num1 = double.Parse(firstNumber);
            double num2 = double.Parse(secondNumber);
            if (percentageOperation != "")
            {
                num2 = num1 * (num2 / 100);
            }
            switch (operation)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "*":
                    return num1 * num2;
                case "/":
                    if (num2 == 0)
                    {
                        return -1;
                    }
                    else
                    {
                        return num1 / num2;
                    }
                case "^":
                    return Math.Pow(num1, num2);
                case "mod":
                    return num1 % num2;
                default:
                    return -2;
            }
        }
    }
}
