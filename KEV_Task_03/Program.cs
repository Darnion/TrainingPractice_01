using System;

namespace KEV_Task_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string secretInfo = "Top secret";
            string secretKey = "p@55W0RD";
            var attempts = 0;

            while (attempts < 3)
            {
                attempts++;
                
                Console.Write("Введите пароль: ");
                if (Console.ReadLine() == secretKey)
                {
                    Console.WriteLine(secretInfo);
                    break;
                }

                Console.WriteLine($"Пароль неверный. Осталось попыток: {3 - attempts}");
            }

            Console.ReadKey();
        }
    }
}
