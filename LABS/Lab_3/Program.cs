// автор: Іван Біланович
// варіант: 2
// лабораторна робота №3 - обробка виключних ситуацій (клас "прямокутник" - TRectangle)

using System;
using System.Globalization;
using System.Text;

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
                throw new ArgumentNullException("other", "Об'єкт для копіювання не може бути null.");
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
                    $"Виключна ситуація: один з прямокутників вироджений (сторони a = {a:F2}, b = {b:F2}). Сторони мають бути додатними!");
            }
        }

        // метод введення даних прямокутника з клавіатури
        public void Input()
        {
            // зчитування довжини сторони a
            Console.Write("Введіть довжину сторони a: ");
            string inputA = Console.ReadLine();
            if (inputA != null)
            {
                inputA = inputA.Replace(',', '.');
            }
            if (!double.TryParse(inputA, NumberStyles.Any, CultureInfo.InvariantCulture, out a))
            {
                throw new FormatException("Некоректний формат числа для сторони a.");
            }

            // зчитування довжини сторони b
            Console.Write("Введіть довжину сторони b: ");
            string inputB = Console.ReadLine();
            if (inputB != null)
            {
                inputB = inputB.Replace(',', '.');
            }
            if (!double.TryParse(inputB, NumberStyles.Any, CultureInfo.InvariantCulture, out b))
            {
                throw new FormatException("Некоректний формат числа для сторони b.");
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
                return "Прямокутники рівні за сторонами та площею.";
            }

            // порівняння за площею
            double area1 = GetArea();
            double area2 = other.GetArea();

            if (Math.Abs(area1 - area2) < 1e-9)
            {
                return $"Прямокутники мають однакову площу ({area1:F2}), але різні сторони.";
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

    class Program
    {
        static void Main(string[] args)
        {
            // налаштування кодування консолі для коректного виведення української мови
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // виведення заголовка лабораторної роботи
            Console.WriteLine("Лабораторна робота №3 ");

            // робота з коректними прямокутниками та введення даних користувачем
            try
            {
                Console.WriteLine("  Створення прямокутників та виконання операцій ");

                // конструктор без параметрів
                TRectangle rectDefault = new TRectangle();
                rectDefault.Print("Прямокутник за замовчуванням");

                // введення даних користувачем
                TRectangle rect1 = new TRectangle();
                Console.WriteLine("\nВведіть сторони першого прямокутника (rect1):");
                rect1.Input();

                // конструктор з параметрами
                TRectangle rect2 = new TRectangle(2.0, 1.5);

                // конструктор копіювання
                TRectangle rectCopy = new TRectangle(rect1);

                Console.WriteLine();
                rect1.Print("Прямокутник rect1");
                rect2.Print("Прямокутник rect2");
                rectCopy.Print("Копія rect1 (конструктор копіювання)");

                // обчислення площі та периметру
                Console.WriteLine($"\nПлоща rect1 {rect1.GetArea():F2}, Периметр rect1 {rect1.GetPerimeter():F2}");
                Console.WriteLine($"Площа rect2 {rect2.GetArea():F2}, Периметр rect2 {rect2.GetPerimeter():F2}");

                // порівняння прямокутників
                Console.WriteLine($"Порівняння rect1 та rect2: {rect1.CompareWith(rect2)}");

                // демонстрація перевантажених операторів +, -, *
                TRectangle sumRect = rect1 + rect2;
                sumRect.Print("\nСума (rect1 + rect2)");

                TRectangle scaledRect = rect1 * 2.5;
                scaledRect.Print("Добуток (rect1 * 2.5)");

                TRectangle diffRect = rect1 - rect2;
                diffRect.Print("Різниця (rect1 - rect2)");
            }
            catch (Exception ex)
            {
                // перехоплення виключної ситуації під час роботи з введеними даними
                Console.WriteLine($"\n[Перехоплено виключення] {ex.Message}");
            }
            finally
            {
                // блок finally виконується завжди незалежно від наявності помилки
                Console.WriteLine("БЛОК FINALLY №1 , Завершення першого блоку операцій над прямокутниками\n");
            }

            // блок номер 2: гарантована демонстрація виключної ситуації "один з прямокутників вироджений"
            try
            {
                Console.WriteLine(" Демонстрація виключної ситуації (вироджений прямокутник)");

                // створення звичайного та виродженого прямокутника (сторона b = 0)
                TRectangle validRect = new TRectangle(5.0, 4.0);
                TRectangle degenerateRect = new TRectangle(3.0, 0.0);

                validRect.Print("Коректний прямокутник");
                degenerateRect.Print("Вироджений прямокутник");

                // спроба виконати додавання з виродженим прямокутником (генерує виключення)
                Console.WriteLine("Спроба додати коректний та вироджений прямокутники");
                TRectangle errorResult = validRect + degenerateRect;
                errorResult.Print("Результат");
            }
            catch (InvalidOperationException ex)
            {
                // обробка виключної ситуації виродженого прямокутника
                Console.WriteLine($"[ОБРОБКА ВИКЛЮЧЕННЯ ({ex.GetType().Name})]: {ex.Message}");
            }
            finally
            {
                // обов'язкове виконання блоку finally
                Console.WriteLine("БЛОК FINALLY №2 Обробку виключної ситуації завершено успішно");
            }
        }
    }
}
