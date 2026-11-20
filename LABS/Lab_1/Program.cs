// автор: Іван Біланович
// варіант: 2
// лабораторна робота №1 - об'єкт класу "вектор"

using System;
using System.Globalization;
using System.Text;

namespace Lab_1
{
    // клас для роботи з математичним вектором
    public class Vector
    {
        // поле для зберігання координат вектора
        private double[] coordinates;

        // поле для зберігання розмірності вектора
        private int dimension;

        // конструктор за замовчуванням
        public Vector()
        {
            // початкова розмірність дорівнює нулю
            dimension = 0;
            coordinates = new double[0];
        }

        // конструктор з параметром розмірності
        public Vector(int size)
        {
            // перевірка коректності розмірності
            if (size < 1)
            {
                size = 1;
            }

            // збереження розмірності та виділення пам'яті під координати
            dimension = size;
            coordinates = new double[dimension];
        }

        // метод введення елементів вектора з клавіатури
        public void Input()
        {
            // запит розмірності вектора, якщо вона ще не задана
            while (dimension <= 0)
            {
                Console.Write("Введіть розмірність вектора (n > 0): ");
                string dimInput = Console.ReadLine();
                int n;
                if (int.TryParse(dimInput, out n) && n > 0)
                {
                    dimension = n;
                    coordinates = new double[dimension];
                }
                else
                {
                    Console.WriteLine("Помилка! Введіть ціле додатне число.");
                }
            }

            // почергове введення кожної координати вектора
            for (int i = 0; i < dimension; i++)
            {
                while (true)
                {
                    Console.Write($"Введіть координату [{i + 1}]: ");
                    string input = Console.ReadLine();
                    if (input != null)
                    {
                        input = input.Replace(',', '.');
                    }

                    double value;
                    if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                    {
                        coordinates[i] = value;
                        break;
                    }
                    Console.WriteLine("Помилка! Введіть дійсне число.");
                }
            }
        }

        // метод виведення елементів вектора у рядку
        public void Print()
        {
            // відкриття дужки вектора
            Console.Write("( ");

            // виведення всіх координат у одному рядку
            for (int i = 0; i < dimension; i++)
            {
                Console.Write($"{coordinates[i]:F4}");
                if (i < dimension - 1)
                {
                    Console.Write("; ");
                }
            }

            // закриття дужки та перехід на новий рядок
            Console.WriteLine(" )");
        }

        // метод визначення довжини (модуля) вектора
        public double GetLength()
        {
            // змінна для накопичення суми квадратів координат
            double sumOfSquares = 0;

            // обчислення суми квадратів усіх координат
            for (int i = 0; i < dimension; i++)
            {
                sumOfSquares += coordinates[i] * coordinates[i];
            }

            // повернення квадратного кореня із суми квадратів
            return Math.Sqrt(sumOfSquares);
        }

        // метод нормування вектора (приведення до одиничної довжини)
        public void Normalize()
        {
            // обчислення поточної довжини вектора
            double length = GetLength();

            // перевірка, чи вектор не є нульовим
            if (length == 0)
            {
                Console.WriteLine("Попередження: нульовий вектор неможливо нормувати.");
                return;
            }

            // ділення кожної координати на довжину вектора
            for (int i = 0; i < dimension; i++)
            {
                coordinates[i] /= length;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // налаштування кодування консолі для коректного відображення української мови
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // виведення заголовка лабораторної роботи
            Console.WriteLine(" Лабораторна робота №1 ");

            // створення об'єкта класу вектор
            Vector vector = new Vector();

            // виклик методу введення елементів вектора
            vector.Input();

            // виведення введених елементів вектора у рядку
            Console.Write("\nПочатковий вектор ");
            vector.Print();

            // визначення та виведення довжини вектора
            double length = vector.GetLength();
            Console.WriteLine($"Довжина вектора {length:F4}");

            // виконання нормування вектора
            vector.Normalize();

            // виведення елементів нормованого вектора у рядку
            Console.Write("Нормований вектор ");
            vector.Print();

            // перевірка довжини вектора після нормування
            Console.WriteLine($"Довжина після нормування {vector.GetLength():F4}");
        }
    }
}
