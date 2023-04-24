using System;

namespace KEV_Task_01
{
    internal class Program
    {
        static void Main(string[] args)
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

            Console.ReadKey();
        }
    }
}
