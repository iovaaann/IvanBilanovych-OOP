
using System;
using System.Globalization;
using System.Text;

namespace Lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            // налаштування кодування консолі для коректного відображення української мови
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // виведення заголовка програми
            Console.WriteLine(" Лабораторна робота №2 ");

            // 1. демонстрація конструктора за замовчуванням
            Console.WriteLine("1. Створення об'єкта v1 за допомогою конструктора за замовчуванням");
            Vector v1 = new Vector();
            Console.WriteLine($"   Читання через властивості X = {v1.X:F4}, Y = {v1.Y:F4}");
            Console.WriteLine($"   {v1.GetPolarCoordinatesData()}\n");

            // зчитування координат кінця вектора від користувача для конструктора з параметрами
            Console.WriteLine("2. Введення даних для створення об'єкта v2 конструктором з параметрами");
            double userX = ReadDouble("   Введіть координату кінця вектора X ");
            double userY = ReadDouble("   Введіть координату кінця вектора Y ");

            // 2. демонстрація конструктора з параметрами
            Vector v2 = new Vector(userX, userY);
            Console.WriteLine($"   {v2.GetVectorData()}");
            Console.WriteLine($"   {v2.GetPolarCoordinatesData()}\n");

            // 3. демонстрація конструктора копіювання
            Console.WriteLine("3. Створення об'єкта v3 за допомогою конструктора копіювання (копія 2)");
            Vector v3 = new Vector(v2);
            Console.WriteLine($"   Читання через методи-геттери X = {v3.GetX():F4}, Y = {v3.GetY():F4}");
            Console.WriteLine($"   {v3.GetPolarCoordinatesData()}");
        }

        // допоміжний метод для безпечного зчитування дійсного числа з консолі
        static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Replace(',', '.');
                if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                {
                    return result;
                }
                Console.WriteLine("   Помилка, Введіть коректне число");
            }
        }
    }
}
