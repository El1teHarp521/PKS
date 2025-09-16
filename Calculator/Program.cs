using System;

class Calculator
{
    private double currentValue;
    private double memoryValue;
    private bool newInput;

    public Calculator()
    {
        currentValue = 0;
        memoryValue = 0;
        newInput = true;
    }

    public void Run()
    {
        bool exit = false;
        Console.WriteLine("=== КАЛЬКУЛЯТОР ===");

        while (!exit)
        {
            try
            {
                Console.WriteLine($"\nТекущее значение: {currentValue}");
                Console.WriteLine($"Память (M): {memoryValue}");
                Console.WriteLine("Доступные операции: +, -, *, /, %, 1/x, x^2, sqrt, M+, M-, MR, C, exit");
                Console.Write("Введите операцию: ");
                
                string op = Console.ReadLine();

                if (op.ToLower() == "exit")
                {
                    exit = true;
                    continue;
                }

                ProcessOperation(op);
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Неверный формат числа!");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Ошибка: Деление на ноль!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Ошибка: Переполнение числа!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }

        Console.WriteLine("Работа калькулятора завершена.");
    }

    private void ProcessOperation(string operation)
    {
        switch (operation)
        {
            case "+":
            case "-":
            case "*":
            case "/":
            case "%":
                ProcessBinaryOperation(operation);
                break;

            case "1/x":
                ProcessReciprocal();
                break;

            case "x^2":
                ProcessSquare();
                break;

            case "sqrt":
                ProcessSquareRoot();
                break;

            case "M+":
                memoryValue += currentValue;
                Console.WriteLine($"Добавлено в память: {currentValue}");
                newInput = true;
                break;

            case "M-":
                memoryValue -= currentValue;
                Console.WriteLine($"Вычтено из памяти: {currentValue}");
                newInput = true;
                break;

            case "MR":
                currentValue = memoryValue;
                Console.WriteLine($"Восстановлено из памяти: {memoryValue}");
                newInput = true;
                break;

            case "C":
                currentValue = 0;
                newInput = true;
                Console.WriteLine("Калькулятор очищен");
                break;

            default:
                Console.WriteLine("Неизвестная операция!");
                break;
        }
    }

    private void ProcessBinaryOperation(string operation)
    {
        double num1 = GetNumber("Введите первое число: ");
        double num2 = GetNumber("Введите второе число: ");

        switch (operation)
        {
            case "+":
                currentValue = num1 + num2;
                break;
            case "-":
                currentValue = num1 - num2;
                break;
            case "*":
                currentValue = num1 * num2;
                break;
            case "/":
                if (num2 == 0) throw new DivideByZeroException();
                currentValue = num1 / num2;
                break;
            case "%":
                if (num2 == 0) throw new DivideByZeroException();
                currentValue = num1 % num2;
                break;
        }

        Console.WriteLine($"Результат: {currentValue}");
        newInput = true;
    }

    private void ProcessReciprocal()
    {
        double num = GetNumber("Введите число: ");
        if (num == 0) throw new DivideByZeroException();
        currentValue = 1 / num;
        Console.WriteLine($"Результат: {currentValue}");
        newInput = true;
    }

    private void ProcessSquare()
    {
        double num = GetNumber("Введите число: ");
        currentValue = num * num;
        Console.WriteLine($"Результат: {currentValue}");
        newInput = true;
    }

    private void ProcessSquareRoot()
    {
        double num = GetNumber("Введите число: ");
        if (num < 0) throw new ArgumentException("Нельзя извлечь корень из отрицательного числа!");
        currentValue = Math.Sqrt(num);
        Console.WriteLine($"Результат: {currentValue}");
        newInput = true;
    }

    private double GetNumber(string message)
    {
        Console.Write(message);
        return Convert.ToDouble(Console.ReadLine());
    }
}
class Program
{
    static void Main()
    {
        Calculator calculator = new Calculator();
        calculator.Run();
    }
}