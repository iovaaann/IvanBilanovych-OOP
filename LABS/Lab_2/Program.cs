// автор: Іван Біланович
// варіант: 2
// лабораторна робота №2 - дослідження внутрішньої будови класів (клас "вектори")

using System;
using System.Globalization;
using System.Text;

namespace Lab_2
{
    // клас вектора на площині з початком у точці (0; 0)
    public class Vector
    {
        // закриті поля для зберігання координат кінця вектора
        private double x;
        private double y;

        // властивість тільки для читання координати x
        public double X
        {
            get { return x; }
        }

        // властивість тільки для читання координати y
        public double Y
        {
            get { return y; }
        }

        // конструктор за замовчуванням (без параметрів)
        public Vector()
        {
            // ініціалізація координат одиничними значеннями за замовчуванням
            x = 1.0;
            y = 1.0;
        }

        // конструктор з параметрами
        public Vector(double endX, double endY)
        {
            // встановлення переданих координат кінця вектора
            x = endX;
            y = endY;
        }

        // конструктор копіювання
        public Vector(Vector other)
        {
            // перевірка на null та копіювання координат з іншого об'єкта
            if (other != null)
            {
                x = other.x;
                y = other.y;
            }
            else
            {
                x = 0.0;
                y = 0.0;
            }
        }

        // деструктор (фіналізатор) для демонстрації механізму знищення об'єкта
        ~Vector()
        {
            // звільнення ресурсів при знищенні об'єкта
        }

        // метод отримання координати x
        public double GetX()
        {
            // повернення значення закритого поля x
            return x;
        }

        // метод отримання координати y
        public double GetY()
        {
            // повернення значення закритого поля y
            return y;
        }

        // метод обчислення полярного радіуса (довжини вектора r)
        public double CalculatePolarRadius()
        {
            // обчислення відстані від початку координат до кінця вектора
            return Math.Sqrt(x * x + y * y);
        }

        // метод обчислення полярного кута в радіанах
        public double CalculatePolarAngleRadians()
        {
            // використання atan2 для визначення кута з урахуванням чверті
            return Math.Atan2(y, x);
        }

        // метод обчислення полярного кута в градусах
        public double CalculatePolarAngleDegrees()
        {
            // переведення кута з радіанів у градуси
            return CalculatePolarAngleRadians() * (180.0 / Math.PI);
        }

        // метод повернення рядкового представлення даних об'єкта
        public string GetVectorData()
        {
            // формування рядка з декартовими координатами кінця вектора
            return $"Координати кінця вектора: X = {x:F4}, Y = {y:F4}";
        }

        // метод повернення рядкового представлення полярних координат
        public string GetPolarCoordinatesData()
        {
            // обчислення полярного радіуса та кута
            double r = CalculatePolarRadius();
            double angleRad = CalculatePolarAngleRadians();
            double angleDeg = CalculatePolarAngleDegrees();

            // формування рядка з полярними координатами
            return $"Полярні координати: r = {r:F4}, φ = {angleRad:F4} рад ({angleDeg:F2}°)";
        }
    }

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
            double userX = ReadDouble("   Введіть координату кінця вектора X: ");
            double userY = ReadDouble("   Введіть координату кінця вектора Y: ");

            // 2. демонстрація конструктора з параметрами
            Vector v2 = new Vector(userX, userY);
            Console.WriteLine($"   {v2.GetVectorData()}");
            Console.WriteLine($"   {v2.GetPolarCoordinatesData()}\n");

            // 3. демонстрація конструктора копіювання
            Console.WriteLine("3. Створення об'єкта v3 за допомогою конструктора копіювання (копія v2)");
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
                string input = Console.ReadLine();
                if (input != null)
                {
                    input = input.Replace(',', '.');
                }

                double result;
                if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                {
                    return result;
                }
                Console.WriteLine("   Помилка! Введіть коректне число.");
            }
        }
    }
}
