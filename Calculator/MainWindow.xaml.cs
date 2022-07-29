using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber;
        OperatorTypes operatorSelected;
        bool calculationEnded;
        public MainWindow()
        {
            InitializeComponent();
            calculationEnded = false;
            lastNumber = 0;
        }

        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            string? selectedValue = (sender as Button).Content.ToString();

            if (resultLabel.Content.ToString() == "0" || calculationEnded)
            {
                resultLabel.Content = selectedValue;
                calculationEnded = false;
            }
            else {
                resultLabel.Content = $"{resultLabel.Content.ToString()}{selectedValue}";
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            lastNumber = 0;
        }

        private void negativeSignButton_Click(object sender, RoutedEventArgs e)
        {
            double temp;
            if (double.TryParse(resultLabel.Content.ToString(), out temp))
            {
                if (temp != 0)
                    temp *= -1;
                resultLabel.Content = temp.ToString();
            }
        }

        private void percentageButton_Click(object sender, RoutedEventArgs e)
        {
            double temp;
            if (double.TryParse(resultLabel.Content.ToString(), out temp))
            {
                temp /= 100;
                resultLabel.Content = temp.ToString();
            }
        }

        private void operatorButton_Click(object sender, RoutedEventArgs e)
        {
            string temp = (sender as Button).Content.ToString();
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                switch (temp)
                {
                    case "/":
                        operatorSelected = OperatorTypes.Divide;
                        break;
                    case "*":
                        operatorSelected = OperatorTypes.Multiply;
                        break;
                    case "-":
                        operatorSelected = OperatorTypes.Minus;
                        break;
                    case "+":
                        operatorSelected = OperatorTypes.Plus;
                        break;
                    default:
                        MessageBox.Show("Error, something went wrong when you clicked the button!");
                        break;
                }
            }
            resultLabel.Content = "0";
        }

        private void equalButton_Click(object sender, RoutedEventArgs e)
        {
            double temp;
            if (double.TryParse(resultLabel.Content.ToString(), out temp))
            {
                switch (operatorSelected)
                {
                    case OperatorTypes.Plus:
                        resultLabel.Content = Operations.Addition(lastNumber, temp).ToString();
                        break;
                    case OperatorTypes.Minus:
                        resultLabel.Content = Operations.Subtraction(lastNumber, temp).ToString();
                        break;
                    case OperatorTypes.Multiply:
                        resultLabel.Content = Operations.Multiplication(lastNumber, temp).ToString();
                        break;
                    case OperatorTypes.Divide:
                        resultLabel.Content = Operations.Division(lastNumber, temp).ToString();
                        break;
                }
                lastNumber = double.Parse(resultLabel.Content.ToString());
                calculationEnded = true;
            }
        }

        private void decimalButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = $"{resultLabel.Content.ToString()}.";
        }
    }

    public enum OperatorTypes
    {
        Plus,
        Minus,
        Multiply,
        Divide
    }

    class Operations
    {
        public static double Addition(double a, double b)
        {
            return a + b;
        }

        public static double Multiplication(double a, double b)
        {
            return a * b;
        }

        public static double Subtraction(double a, double b)
        {
            return a - b;
        }

        public static double Division(double a, double b)
        {
            return a / b;
        }
    }
}
