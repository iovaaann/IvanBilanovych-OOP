
using System;
using System.Text;

namespace Lab_1
{
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
