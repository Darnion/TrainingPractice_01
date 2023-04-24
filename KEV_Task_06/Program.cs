using System;

namespace KEV_Task_06
{
    internal class Program
    {
        private static void Main()
        {
            string[] fullNamesArray = new string[0];
            string[] postsArray = new string[0];
            var isStillRunning = true;

            while (isStillRunning)
            {
                Console.WriteLine("Введите номер нужного действия:\n" +
                  "1. Добавить досье\n" +
                  "2. Вывести все досье\n" +
                  "3. Удалить досье\n" +
                  "4. Поиск по фамилии\n" +
                  "5. Выход\n");

                var input = Console.ReadLine();
                Console.WriteLine();

                int.TryParse(input, out var action);

                switch (action)
                {
                    case 1:
                        AddDossier(ref fullNamesArray, ref postsArray);
                        break;
                    case 2:
                        ShowAllDossiers(ref fullNamesArray, ref postsArray);
                        break;
                    case 3:
                        DeleteDossier(ref fullNamesArray, ref postsArray);
                        break;
                    case 4:
                        FindBySurname(ref fullNamesArray, ref postsArray);
                        break;
                    case 5:
                        isStillRunning = false;
                        break;
                    default:
                        Console.WriteLine("Вы выбрали неверное действие. Попробуйте ещё раз.");
                        break;
                }
                Console.WriteLine();
            }
        }

        private static void AddDossier(ref string[] fullNamesArray, ref string[] postsArray)
        {
            Array.Resize(ref fullNamesArray, fullNamesArray.Length + 1);
            Array.Resize(ref postsArray, postsArray.Length + 1);

            Console.Write("Введите ФИО: ");
            var fullName = Console.ReadLine();

            Console.Write("Введите должность: ");
            var post = Console.ReadLine();

            fullNamesArray[fullNamesArray.Length - 1] = fullName;
            postsArray[postsArray.Length - 1] = post;

            Console.WriteLine("Досье успешно добавлено.");
        }

        private static void ShowAllDossiers(ref string[] fullNamesArray, ref string[] postsArray)
        {
            Console.WriteLine("Список досье:");
            for (var i = 0; i < fullNamesArray.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {fullNamesArray[i]} - {postsArray[i]}");
            }
        }

        private static void DeleteDossier(ref string[] fullNamesArray, ref string[] postsArray)
        {
            Console.Write("Введите номер досье: ");
            var input = Console.ReadLine();

            int.TryParse(input, out var id);

            for (var i = id - 1; i < fullNamesArray.Length - 1; i++)
            {
                fullNamesArray[i] = fullNamesArray[i + 1];
                postsArray[i] = postsArray[i + 1];
            }

            Array.Resize(ref fullNamesArray, fullNamesArray.Length - 1);
            Array.Resize(ref postsArray, postsArray.Length - 1);

            Console.WriteLine("Досье успешно удалено.");
        }

        private static void FindBySurname(ref string[] fullNamesArray, ref string[] postsArray)
        {
            var match = false;

            Console.Write("Введите фамилию: ");
            var surname = Console.ReadLine();

            for (var i = 0; i < surname.Length; i++)
            {
                if (fullNamesArray[i].StartsWith(surname))
                {
                    Console.WriteLine($"{i + 1}. {fullNamesArray[i]} - {postsArray[i]}");
                    match = true;
                    break;
                }
            }

            if (!match)
            {
                Console.WriteLine("Совпадений нет.");
            }
        }
    }
}
