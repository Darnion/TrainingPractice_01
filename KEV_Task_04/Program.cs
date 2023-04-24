using System;

namespace KEV_Task_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            var counter = 0;
            Player player = new Player();
            Boss boss = new Boss();

            Console.WriteLine($"Игра \"Победи БОССА\"\n" +
                              $"Условия:\n" +
                              $"Максимальное количество здоровья БОССА - {boss.MaxHP}\n" +
                              $"Максимальное количество здоровья игрока - {player.MaxHP}\n" +
                              $"Игрок и БОСС ходят по очереди, первый ход определяется случайно\n" +
                              $"Урон от БОССА случайный для каждого хода\n" +
                              $"БОСС восстанавливает случайное количество здоровья после своего хода\n" +
                              $"Игрок может производить следующие действия:\n" +
                              $"\n" +
                              $"Атака - атака, наносящая от {player.StartDamage} единиц урона\n" +
                              $"Урон зависит от текущей силы игрока\n" +
                              $"\n" +
                              $"Фаталити - атака, уничтожающая БОССА\n" +
                              $"Для использования необходимо совершить три удачных парирования подряд\n" +
                              $"\n" +
                              $"Уворот - с большим шансом (90%) позволяет проигнорировать следующую атаку БОССА и ослабить его\n" +
                              $"При неудаче игрок получает двойной урон\n" +
                              $"\n" +
                              $"Выпить зелье - восстанавливает 20% от максимального здоровья игрока каждый ход в течение 3 ходов, увеличивает силу следующей атаки игрока в 2 раза\n" +
                              $"\n" +
                              $"Парирование - с шансом 50% отражает урон БОССА в самого БОССА. Так же добавляет к атаке эффект вампиризма 100% на 3 хода\n" +
                              $"\n" +
                              $"Начальное здоровье БОССА - {boss.CurrentHP}\n" +
                              $"Начальное здоровье игрока - {player.CurrentHP}\n");

            player.IsMyTurn = random.Next(2) == 1;

            while (player.CurrentHP > 0 && boss.CurrentHP > 0)
            {
                counter++;

                Console.WriteLine($"Игровой такт №{counter}\n" +
                                  $"Атакует {(player.IsMyTurn ? "игрок" : "БОСС")}");

                if (player.IsMyTurn)
                {
                    Console.Write($"Выберите действие - ");
                    player.CurrentAction = Console.ReadLine();
                    EndTurn(ref boss, ref player);
                }    
                else
                {
                    Console.WriteLine($"Сила удара - {boss.CurrentDamage}");
                    EndTurn(ref boss, ref player);
                }

                Console.WriteLine($"Осталось здоровья у игрока - {player.CurrentHP}\n" +
                                  $"Осталось здоровья у БОССА - {boss.CurrentHP}\n");
            }

            if (player.CurrentHP <= 0)
            {
                Console.WriteLine("У Вас закончилось здоровье. БОСС победил!");
            }
            else
            {
                Console.WriteLine("БОСС повержен! Вы победили!");
            }

            Console.ReadKey();
        }

        private static void EndTurn(ref Boss boss, ref Player player)
        {
            Random random = new Random();

            if (player.IsMyTurn)
            {
                switch (player.CurrentAction.ToLower())
                {
                    case "атака":
                        boss.CurrentHP -= player.CurrentDamage;
                        Console.WriteLine($"Вы наносите {player.CurrentDamage} урона");
                        if (player.VampireAttackAmount > 0)
                        {
                            player.CurrentHP += player.CurrentDamage;
                            Console.WriteLine($"Вампиризм восстанавливает Вам {player.CurrentDamage} очков здоровья\n" +
                                              $"Действие эффекта продлится ещё {player.VampireAttackAmount - 1} ходов");
                        }
                        player.CurrentDamage = player.StartDamage;
                        break;
                    case "фаталити":
                        if (player.ParryInARow >= 3)
                        {
                            boss.CurrentHP = 0;
                        }
                        else
                        {
                            Console.WriteLine("Не сработало. Вы пропускаете ход");
                        }
                        break;
                    case "выпить зелье":
                        player.HealingAmount = 3;
                        player.CurrentDamage *= 2;
                        break;
                    case "уворот":
                            break;
                    case "парирование":
                        break;
                    default:
                        Console.WriteLine("Вы выбрали неверное действие и пропускаете ход");
                        break;
                }
            }
            else
            {
                var bossDamageModifier = 0;
                switch (player.CurrentAction.ToLower())
                {
                    case "уворот":
                        if (random.Next(10) > 0)
                        {
                            Console.WriteLine("А ты ловкий! Босс промахнулся и в следующий раз ударит слабее на 60 единиц!");
                            bossDamageModifier = -60;
                        }
                        else
                        {
                            Console.WriteLine("Увернуться не удалось! Получаешь удвоенный урон.");
                            player.CurrentHP -= boss.CurrentDamage * 2;
                        }
                        break;
                    case "парирование":
                        if (random.Next(2) == 1)
                        {
                            Console.WriteLine("Парирование не удалось! В этот раз удача не на твоей стороне.");
                            player.CurrentHP -= boss.CurrentDamage;
                            player.ParryInARow = 0;
                        }
                        else
                        {
                            Console.WriteLine("Получилось! Ты отразил урон обратно.");
                            boss.CurrentHP -= boss.CurrentDamage;
                            player.ParryInARow++;
                            player.VampireAttackAmount = 4;
                        }
                        break;
                    default:
                        player.CurrentHP -= boss.CurrentDamage;
                        break;
                }

                if (player.HealingAmount > 0)
                {
                    player.CurrentHP += player.MaxHP / 5;
                    player.HealingAmount--;
                    Console.WriteLine($"Вы восстанавливаете {player.MaxHP / 5} очков здоровья\n" +
                                      $"Эффект лечения продлится ещё {player.HealingAmount} ходов");
                }

                player.VampireAttackAmount--;
                boss.CurrentDamage = random.Next(70, 150) + bossDamageModifier;
                var bossHealing = random.Next(1, 100);
                boss.CurrentHP += bossHealing;
                Console.WriteLine($"БОСС востанавливает {bossHealing} очков здоровья");
            }

            player.IsMyTurn = !player.IsMyTurn;
        }

        class Boss
        {
            private Random random = new Random();
            private int maxHP;
            private int currentHP;
            private int currentDamage;

            public Boss()
            {
                this.MaxHP = random.Next(500, 1000);
                this.CurrentHP = random.Next(300, MaxHP);
                this.CurrentDamage = random.Next(70, 150);
            }

            public int MaxHP { get => maxHP; set => maxHP = value; }
            public int CurrentHP { get => currentHP; set => currentHP = value <= MaxHP ? value : MaxHP; }
            public int CurrentDamage { get => currentDamage; set => currentDamage = value; }
        }

        class Player
        {
            private Random random = new Random();
            private int maxHP;
            private int currentHP;
            private int startDamage;
            private int currentDamage;
            private int parryInARow;
            private bool isMyTurn;
            private string currentAction;
            private int vampireAttackAmount;
            private int healingAmount;

            public Player()
            {
                random.Next(1000);
                this.MaxHP = random.Next(500, 1000);
                this.CurrentHP = random.Next(200, MaxHP);
                this.StartDamage = random.Next(30, 100);
                this.CurrentDamage = StartDamage;
                this.ParryInARow = 0;
                this.CurrentAction = "";
                this.IsMyTurn = true;
                this.VampireAttackAmount = 0;
                this.HealingAmount = 0;
            }

            public int MaxHP { get => maxHP; set => maxHP = value; }
            public int CurrentHP { get => currentHP; set => currentHP = value <= MaxHP ? value : MaxHP; }
            public int StartDamage { get => startDamage; set => startDamage = value; }
            public int CurrentDamage { get => currentDamage; set => currentDamage = value; }
            public int ParryInARow { get => parryInARow; set => parryInARow = value; }
            public bool IsMyTurn { get => isMyTurn; set => isMyTurn = value; }
            public string CurrentAction { get => currentAction; set => currentAction = value; }
            public int VampireAttackAmount { get => vampireAttackAmount; set => vampireAttackAmount = value; }
            public int HealingAmount { get => healingAmount; set => healingAmount = value; }
        }
    }
}
