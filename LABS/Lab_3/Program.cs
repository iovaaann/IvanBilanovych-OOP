
using System;
using System.Text;

namespace Lab_3
{
    class Program
    {
        static void Main(string[] args)
        {
            // налаштування кодування консолі для коректного виведення української мови
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // виведення заголовка лабораторної роботи
            Console.WriteLine("Лабораторна робота №3 ");
    

            //  робота з коректними прямокутниками та введення даних користувачем
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
                Console.WriteLine($"Порівняння rect1 та rect2 {rect1.CompareWith(rect2)}");

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

            // блок номер 2  гарантована демонстрація виключної ситуації "один з прямокутників вироджений"
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
