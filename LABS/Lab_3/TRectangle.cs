
using System;
using System.Globalization;

namespace Lab_3
{
    // клас прямокутника з обробкою виключних ситуацій
    public class TRectangle
    {
        // поля для зберігання довжин сторін прямокутника
        private double a;
        private double b;

        // властивість для читання сторони a
        public double A
        {
            get { return a; }
        }

        // властивість для читання сторони b
        public double B
        {
            get { return b; }
        }

        // конструктор без параметрів
        public TRectangle()
        {
            // ініціалізація сторін одиничними значеннями
            a = 1.0;
            b = 1.0;
        }

        // конструктор з параметрами
        public TRectangle(double sideA, double sideB)
        {
            // збереження переданих довжин сторін
            a = sideA;
            b = sideB;
        }

        // конструктор копіювання
        public TRectangle(TRectangle other)
        {
            // перевірка на null перед копіюванням
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other), "Об'єкт для копіювання не може бути null");
            }

            // копіювання довжин сторін з іншого прямокутника
            a = other.a;
            b = other.b;
        }

        // метод перевірки, чи є прямокутник виродженим (хоча б одна сторона <= 0)
        public bool IsDegenerate()
        {
            return a <= 0 || b <= 0;
        }

        // метод генерації виключної ситуації, якщо прямокутник вироджений
        public void ValidateNotDegenerate()
        {
            if (IsDegenerate())
            {
                throw new InvalidOperationException(
                    $"Виключна ситуація: один з прямокутників вироджений (сторони a = {a:F2}, b = {b:F2}) Сторони мають бути додатними");
            }
        }

        // метод введення даних прямокутника з клавіатури
        public void Input()
        {
            // зчитування довжини сторони a
            Console.Write("Введіть довжину сторони a ");
            string inputA = Console.ReadLine()?.Replace(',', '.');
            if (!double.TryParse(inputA, NumberStyles.Any, CultureInfo.InvariantCulture, out a))
            {
                throw new FormatException("Некоректний формат числа для сторони a");
            }

            // зчитування довжини сторони b
            Console.Write("Введіть довжину сторони b ");
            string inputB = Console.ReadLine()?.Replace(',', '.');
            if (!double.TryParse(inputB, NumberStyles.Any, CultureInfo.InvariantCulture, out b))
            {
                throw new FormatException("Некоректний формат числа для сторони b");
            }

            // перевірка введеного прямокутника на виродженість
            ValidateNotDegenerate();
        }

        // метод виведення даних прямокутника на екран
        public void Print(string label = "Прямокутник")
        {
            Console.WriteLine($"{label}: сторона a = {a:F2}, сторона b = {b:F2}");
        }

        // метод визначення площі прямокутника
        public double GetArea()
        {
            // перевірка на виродженість перед обчисленням площі
            ValidateNotDegenerate();
            return a * b;
        }

        // метод визначення периметру прямокутника
        public double GetPerimeter()
        {
            // перевірка на виродженість перед обчисленням периметру
            ValidateNotDegenerate();
            return 2.0 * (a + b);
        }

        // метод порівняння поточного прямокутника з іншим прямокутником
        public string CompareWith(TRectangle other)
        {
            // перевірка обох прямокутників на виродженість
            ValidateNotDegenerate();
            other.ValidateNotDegenerate();

            // перевірка рівності відповідних сторін
            bool sameSides = (Math.Abs(a - other.a) < 1e-9 && Math.Abs(b - other.b) < 1e-9) ||
                             (Math.Abs(a - other.b) < 1e-9 && Math.Abs(b - other.a) < 1e-9);

            if (sameSides)
            {
                return "Прямокутники рівні за сторонами та площею";
            }

            // порівняння за площею
            double area1 = GetArea();
            double area2 = other.GetArea();

            if (Math.Abs(area1 - area2) < 1e-9)
            {
                return $"Прямокутники мають однакову площу ({area1:F2}), але різні сторони";
            }

            return area1 > area2
                ? $"Перший прямокутник більший за площею ({area1:F2} > {area2:F2})."
                : $"Другий прямокутник більший за площею ({area2:F2} > {area1:F2}).";
        }

        // перевантаження оператора + (додавання відповідних сторін)
        public static TRectangle operator +(TRectangle r1, TRectangle r2)
        {
            // перевірка, чи жоден з прямокутників не є виродженим
            r1.ValidateNotDegenerate();
            r2.ValidateNotDegenerate();

            // створення нового прямокутника із сумою відповідних сторін
            return new TRectangle(r1.a + r2.a, r1.b + r2.b);
        }

        // перевантаження оператора - (віднімання довжин відповідних сторін)
        public static TRectangle operator -(TRectangle r1, TRectangle r2)
        {
            // перевірка вихідних прямокутників на виродженість
            r1.ValidateNotDegenerate();
            r2.ValidateNotDegenerate();

            // обчислення різниці відповідних сторін
            TRectangle result = new TRectangle(r1.a - r2.a, r1.b - r2.b);

            // перевірка, чи отриманий прямокутник не став виродженим
            result.ValidateNotDegenerate();
            return result;
        }

        // перевантаження оператора * (множення сторін на деяке число)
        public static TRectangle operator *(TRectangle r, double factor)
        {
            // перевірка початкового прямокутника на виродженість
            r.ValidateNotDegenerate();

            // створення нового прямокутника з масштабованими сторонами
            TRectangle result = new TRectangle(r.a * factor, r.b * factor);

            // перевірка результату на виродженість (якщо множник <= 0)
            result.ValidateNotDegenerate();
            return result;
        }

        // перевантаження оператора * для випадку (число * прямокутник)
        public static TRectangle operator *(double factor, TRectangle r)
        {
            return r * factor;
        }
    }
}
