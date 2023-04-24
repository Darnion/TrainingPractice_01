using System;

namespace KEV_Task_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите количество золота: ");
                var input = Console.ReadLine();
                int.TryParse(input, out var gold);

                var price = 11;
                Console.WriteLine($"Цена за кристалл - {price} золотых\nСколько кристаллов хотите приобрести?");
                input = Console.ReadLine();
                int.TryParse(input, out var amount);

                bool isEnough = gold >= price * amount;
                gold -= Convert.ToByte(isEnough) * price * amount;
                Console.WriteLine($"Остаток золота = {gold}, баланс кристаллов = {amount * Convert.ToByte(isEnough)}");

                Console.WriteLine("Хотите завершить торговлю? (используйте exit для выхода)");
                if (Console.ReadLine().ToLower() == "exit")
                {
                    break;
                }
            }
        }
    }
}
