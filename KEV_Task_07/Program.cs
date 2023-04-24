using System;
using System.Collections.Generic;
using System.Linq;

namespace KEV_Task_07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            Console.Write("Введите размер массива: ");
            var input = Console.ReadLine();

            int.TryParse(input, out var arrSize);

            int[] array = new int[arrSize];

            for (var i = 0; i < arrSize; i++)
            {
                array[i] = random.Next(1000);
            }

            Console.WriteLine("Исходный массив:");
            ShowArray(array);

            Console.WriteLine("\nПеремешанный массив:");
            ShowArray(Shuffle(array));

            Console.ReadKey();
        }

        private static void ShowArray(int[] array)
        {
            Console.Write("[");
            for (var i = 0; i < array.Length - 1; i++)
            {
                Console.Write(array[i] + ", ");
            }
            Console.WriteLine(array.Last() + "]");
        }

        private static int[] Shuffle(int[] startArray)
        {
            Random random = new Random();

            List<int> startIndexArray = new List<int>();
            for (var i = 0; i < startArray.Length; i++)
            {
                startIndexArray.Add(i);
            }

            int[] shuffleIndexArray = new int[startIndexArray.Count];

            for (var i = 0; i < shuffleIndexArray.Length; i++)
            {
                var currentIndex = random.Next(startIndexArray.Count);
                shuffleIndexArray[i] = startIndexArray[currentIndex];
                startIndexArray.RemoveAt(currentIndex);
            }

            int[] shuffleArray = new int[startArray.Length];

            for (var i = 0; i < shuffleArray.Length; i++)
            {
                shuffleArray[i] = startArray[shuffleIndexArray[i]];
            }

            return shuffleArray;
        }
    }
}
