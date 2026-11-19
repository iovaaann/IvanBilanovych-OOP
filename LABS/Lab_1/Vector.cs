

using System;

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
                if (int.TryParse(Console.ReadLine(), out int n) && n > 0)
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
                    string input = Console.ReadLine()?.Replace(',', '.');
                    if (double.TryParse(input, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double value))
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

            // виведення всіх координат через пробіл у одному рядку
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
                Console.WriteLine("Попередження нульовий вектор неможливо нормувати.");
                return;
            }

            // ділення кожної координати на довжину вектора
            for (int i = 0; i < dimension; i++)
            {
                coordinates[i] /= length;
            }
        }
    }
}
