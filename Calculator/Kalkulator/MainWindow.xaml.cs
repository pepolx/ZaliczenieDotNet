using System;
using System.Windows;
using System.Windows.Controls;

namespace Kalkulator
{
    public partial class MainWindow : Window
    {
        Calculator calculator = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = calculator;
        }

        private void AppendNumber(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string number = button.Content.ToString();
            calculator.AppendNumber(number);
        }

        private void AppendOperation(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string operation = button.Content.ToString();
            calculator.AppendOperation(operation);
        }

        private void AppendPercentageOperation(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string percentage = button.Content.ToString();
            calculator.AppendPercentageOperation(percentage);
        }

        private void AppendComma(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string comma = button.Content.ToString();
            calculator.AppendComma(comma);
        }

        private void AppendMinus(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string minus = button.Content.ToString();
            calculator.AppendMinus(minus);
        }

        private void ClearInput(object sender, RoutedEventArgs e)
        {
            calculator.ClearInput();
        }

        private void DeleteLastChar(object sender, RoutedEventArgs e)
        {
            calculator.DeleteLastChar();
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string calculation_way = button.Content.ToString();
            calculator.Calculate(calculation_way);
        }
    }
}
