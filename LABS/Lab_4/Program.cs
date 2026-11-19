
using System;
using System.Text;

namespace Lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            // налаштування кодування консолі для коректного виведення української мови
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // виведення заголовка програми
            Console.WriteLine(" Лабораторна робота №4 ");
            
            try
            {
                // створення першої символьної матриці 2x2 та заповнення через індексатор
                CharMatrix matrixA = new CharMatrix(2, 2);
                matrixA[0, 0] = "A";
                matrixA[0, 1] = "B";
                matrixA[1, 0] = "C";
                matrixA[1, 1] = "D";

                // створення другої символьної матриці 2x2 та заповнення через індексатор
                CharMatrix matrixB = new CharMatrix(2, 2);
                matrixB[0, 0] = "1";
                matrixB[0, 1] = "2";
                matrixB[1, 0] = "3";
                matrixB[1, 1] = "4";

                // виведення початкових матриць
                matrixA.Print("Початкова символьна матриця A");
                matrixB.Print("Початкова символьна матриця B");

                // демонстрація звернення до елемента через індексатор
                Console.WriteLine($"Звернення через індексатор matrixA[0, 1] = \"{matrixA[0, 1]}\"\n");

                // демонстрація операції '*' реплікації матриці на натуральне число (3)
                CharMatrix replicatedInt = matrixA * 3;
                replicatedInt.Print("Реплікація матриці A на натуральне число (matrixA * 3)");

                // демонстрація операції '*' реплікації елементів матриці на дійсне число (2.4 -> 2)
                CharMatrix replicatedDouble = matrixB * 2.4;
                replicatedDouble.Print("Реплікація елементів матриці B на дійсне число (matrixB * 2.4)");

                // демонстрація операції '+' поелементного об'єднання матриць однакової розмірності
                CharMatrix combined = matrixA + matrixB;
                combined.Print("Поелементне об'єднання матриць (matrixA + matrixB)");

                // демонстрація виключної ситуації при незбіжності розмірностей матриць
                Console.WriteLine("Спроба об'єднати матриці різної розмірності (2x2 та 3x2)");
                CharMatrix matrixC = new CharMatrix(3, 2);
                CharMatrix invalidSum = matrixA + matrixC;
                invalidSum.Print("Результат");
            }
            catch (Exception ex)
            {
                // обробка та виведення повідомлення про виключну ситуацію
                Console.WriteLine($" виключення ({ex.GetType().Name})]: {ex.Message}");
            }
        }
    }
}
