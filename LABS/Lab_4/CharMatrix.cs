

using System;
using System.Text;

namespace Lab_4
{
    // клас символьної матриці з індексатором та перевантаженими операторами
    public class CharMatrix
    {
        // двовимірний масив для зберігання символьних елементів матриці
        private string[,] elements;

        // поля для зберігання кількості рядків та стовпців
        private int rows;
        private int cols;

        // властивість для отримання кількості рядків
        public int Rows
        {
            get { return rows; }
        }

        // властивість для отримання кількості стовпців
        public int Cols
        {
            get { return cols; }
        }

        // конструктор з параметрами розмірності матриці
        public CharMatrix(int rowCount, int colCount)
        {
            // перевірка коректності розмірів матриці
            if (rowCount <= 0 || colCount <= 0)
            {
                throw new ArgumentException("Розмірності матриці повинні бути натуральними числами > 0");
            }

            rows = rowCount;
            cols = colCount;
            elements = new string[rows, cols];

            // початкова ініціалізація порожніми рядками
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    elements[i, j] = "";
                }
            }
        }

        // індексатор для звернення до елементів матриці за індексами [row, col]
        public string this[int row, int col]
        {
            get
            {
                // перевірка виходу індексів за межі матриці
                ValidateIndices(row, col);
                return elements[row, col];
            }
            set
            {
                // перевірка індексів та запис нового символьного значення
                ValidateIndices(row, col);
                elements[row, col] = value ?? "";
            }
        }

        // допоміжний метод перевірки коректності індексів
        private void ValidateIndices(int row, int col)
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols)
            {
                throw new IndexOutOfRangeException(
                    $"Індекси [{row}, {col}] виходять за межі матриці розміру {rows}x{cols}.");
            }
        }

        // метод введення елементів символьної матриці з клавіатури
        public void Input()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"  Елемент [{i}, {j}]: ");
                    string input = Console.ReadLine();
                    elements[i, j] = string.IsNullOrEmpty(input) ? "_" : input;
                }
            }
        }

        // метод виведення матриці на екран у табличному вигляді
        public void Print(string title)
        {
            Console.WriteLine($"{title} (розмір {rows}x{cols}):");
            for (int i = 0; i < rows; i++)
            {
                Console.Write("  | ");
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{elements[i, j],-10} | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        // допоміжний метод повторення символьного елемента задану кількість разів
        private static string ReplicateString(string text, int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < count; k++)
            {
                sb.Append(text);
            }
            return sb.ToString();
        }

        // перевантаження оператора * для реплікації матриці в кілька разів (натуральне число)
        public static CharMatrix operator *(CharMatrix matrix, int count)
        {
            // перевірка, що множник є натуральним числом
            if (count <= 0)
            {
                throw new ArgumentException("Виключна ситуація  коефіцієнт реплікації має бути натуральним числом > 0");
            }

            CharMatrix result = new CharMatrix(matrix.rows, matrix.cols);

            // реплікація кожного елемента матриці count разів
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.cols; j++)
                {
                    result[i, j] = ReplicateString(matrix[i, j], count);
                }
            }

            return result;
        }

        // перевантаження оператора * для порядку операндів (натуральне число * матриця)
        public static CharMatrix operator *(int count, CharMatrix matrix)
        {
            return matrix * count;
        }

        // перевантаження оператора * для реплікації елементів матриці на дійсне число
        public static CharMatrix operator *(CharMatrix matrix, double factor)
        {
            // перевірка додатного значення дійсного множника
            if (factor <= 0)
            {
                throw new ArgumentException("Виключна ситуація дійсний коефіцієнт реплікації має бути більшим за 0");
            }

            // визначення кількості повторень шляхом округлення дійсного числа
            int repeatCount = Math.Max(1, (int)Math.Round(factor, MidpointRounding.AwayFromZero));
            return matrix * repeatCount;
        }

        // перевантаження оператора * для порядку операндів (дійсне число * матриця)
        public static CharMatrix operator *(double factor, CharMatrix matrix)
        {
            return matrix * factor;
        }

        // перевантаження оператора + для поелементного об'єднання двох символьних матриць
        public static CharMatrix operator +(CharMatrix m1, CharMatrix m2)
        {
            // перевірка збіжності розмірностей матриць
            if (m1.rows != m2.rows || m1.cols != m2.cols)
            {
                throw new InvalidOperationException(
                    $"Виключна ситуація  розмірності матриць не збігаються ({m1.rows}x{m1.cols} та {m2.rows}x{m2.cols})!");
            }

            CharMatrix result = new CharMatrix(m1.rows, m1.cols);

            // поелементне об'єднання (конкатенація) символьних елементів
            for (int i = 0; i < m1.rows; i++)
            {
                for (int j = 0; j < m1.cols; j++)
                {
                    result[i, j] = m1[i, j] + m2[i, j];
                }
            }

            return result;
        }
    }
}
