using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.Title = "Aventura Épica - Dormir & Sonhos";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== AVENTURA ÉPICA - Dormir & Sonhos ===");
        Console.ResetColor();

        string playerName;
        int health = 100;
        int maxHealth = 100;
        int gold = 200;
        int level = 1;
        int exp = 0;
        int attack = 10;
        int defense = 5;
        string[] inventory = new string[100];
        int inventoryCount = 0;
        Random rand = new Random();

        string[] marketItems = { "Poção", "Poção+", "Espada", "Escudo", "Anel Mágico", "Armadura", "Cristal Raro" };
        int[] marketPrices = { 10, 150, 60, 55, 120, 80, 350 };
        string[] marketRarity = { "Comum", "Raro", "Comum", "Comum", "Raro", "Raro", "Épico" };

        Console.Write("Digite o nome do herói: ");
        playerName = Console.ReadLine();
        Console.WriteLine($"\nBem-vindo, {playerName}! Sua aventura começa agora...\n");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== MENU DE AÇÕES ===");
            Console.WriteLine("1 - Explorar");
            Console.WriteLine("2 - Visitar cidade (Mercado / Dormir)");
            Console.WriteLine("3 - Ver status do herói");
            Console.WriteLine("4 - Inventário");
            Console.WriteLine("5 - Melhorar ataque (50 moedas)");
            Console.WriteLine("6 - Melhorar defesa (50 moedas)");
            Console.WriteLine("7 - Treinar XP");
            Console.WriteLine("8 - Lutar contra chefão");
            Console.WriteLine("9 - Cheats");
            Console.WriteLine("10 - Sair");
            Console.ResetColor();
            Console.Write("Escolha: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ExplorarASCII(ref health, ref gold, ref exp, ref inventory, ref inventoryCount, attack, defense, ref maxHealth, ref level, rand, marketItems);
                    break;
                case "2":
                    Cidade(ref gold, ref inventoryCount, inventory, marketItems, marketPrices, marketRarity, ref attack, ref defense, ref maxHealth,
                        ref health, ref exp, ref level, rand);
                    break;
                case "3":
                    Status(playerName, health, maxHealth, gold, level, exp, attack, defense);
                    break;
                case "4":
                    Inventario(inventory, inventoryCount);
                    break;
                case "5":
                    MelhorarAtaque(ref attack, ref gold);
                    break;
                case "6":
                    MelhorarDefesa(ref defense, ref gold);
                    break;
                case "7":
                    Treinar(ref exp, ref level, ref maxHealth, ref health);
                    break;
                case "8":
                    ChefaoASCII(ref health, ref gold, ref exp, ref inventory, ref inventoryCount, attack, defense, ref maxHealth, ref level, rand, marketItems);
                    break;
                case "9":
                    Cheats(ref health, ref maxHealth, ref gold, ref attack, ref defense, ref exp, ref level, inventory, ref inventoryCount, rand);
                    break;
                case "10":
                    Console.WriteLine("Obrigado por jogar! Até a próxima!");
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    static void ExplorarASCII(ref int health, ref int gold, ref int exp, ref string[] inventory, ref int inventoryCount,
        int attack, int defense, ref int maxHealth, ref int level, Random rand, string[] marketItems)
    {
        string[] enemies = { "Goblin", "Orc", "Lobo", "Bandido" };
        string enemy = enemies[rand.Next(enemies.Length)];
        int enemyHealth = rand.Next(40 + level * 2, 100 + level * 5);

        DrawEnemy(enemy);

        while (enemyHealth > 0 && health > 0)
        {
            Console.WriteLine($"\nSua vida: {health} | Vida do {enemy}: {enemyHealth}");
            Console.WriteLine("1 - Atacar  2 - Usar Poção  3 - Fugir");
            string action = Console.ReadLine();
            if (action == "1")
            {
                int damage = attack + rand.Next(0, Math.Max(1, attack / 10));
                enemyHealth -= damage;
                Console.WriteLine($"Você causou {damage} de dano!");
                if (enemyHealth > 0)
                {
                    int enemyDamageBase = rand.Next(5 + level / 2, 18 + level / 2);
                    int enemyDamage = enemyDamageBase - defense / 4;
                    if (enemyDamage < 0) enemyDamage = 0;
                    health -= enemyDamage;
                    Console.WriteLine($"{enemy} atacou e causou {enemyDamage} de dano!");
                }
            }
            else if (action == "2")
            {
                bool used = false;
                for (int i = 0; i < inventoryCount; i++)
                {
                    if (inventory[i] == "Poção+" || inventory[i] == "Poção")
                    {
                        int heal = inventory[i] == "Poção+" ? 120 : 40;
                        health += heal;
                        if (health > maxHealth) health = maxHealth;
                        Console.WriteLine($"Você usou {inventory[i]} e recuperou {heal} de vida!");
                        for (int j = i; j < inventoryCount - 1; j++) inventory[j] = inventory[j + 1];
                        inventoryCount--;
                        used = true;
                        break;
                    }
                }
                if (!used) Console.WriteLine("Você não tem Poções!");
            }
            else if (action == "3")
            {
                if (rand.NextDouble() < 0.6)
                {
                    Console.WriteLine("Fuga bem sucedida!");
                    return;
                }
                else
                {
                    Console.WriteLine("Fuga falhou! O inimigo ataca!");
                    int enemyDamageBase = rand.Next(5 + level / 2, 18 + level / 2);
                    int enemyDamage = enemyDamageBase - defense / 4;
                    if (enemyDamage < 0) enemyDamage = 0;
                    health -= enemyDamage;
                    Console.WriteLine($"Você sofreu {enemyDamage} de dano!");
                }
            }
            else Console.WriteLine("Ação inválida!");
        }

        if (health > 0)
        {
            int lootGold = rand.Next(15, 121) + level * 2;
            gold += lootGold;
            int gainedExp = rand.Next(10, 41) + level;
            exp += gainedExp;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nVocê derrotou o {enemy} e ganhou {lootGold} moedas e {gainedExp} XP!");
            Console.ResetColor();
            if (rand.NextDouble() < 0.35 && inventoryCount < inventory.Length)
            {
                string found = marketItems[rand.Next(marketItems.Length)];
                inventory[inventoryCount++] = found;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Você encontrou um item: {found}!");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nVocê morreu durante a exploração!");
            Console.ResetColor();
        }

        if (exp >= level * 100 && level < 1000)
        {
            level++;
            maxHealth += 30;
            exp = 0;
            health = maxHealth;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nParabéns! Você subiu para o nível {level}! Vida máxima aumentada.");
            Console.ResetColor();
        }
    }

    static void DrawEnemy(string name)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        if (name == "Goblin")
        {
            Console.WriteLine("   ,      ,");
            Console.WriteLine("  /(.-\"\"-.)\\");
            Console.WriteLine("  |\\  ><  /|");
            Console.WriteLine("   \\  -- /");
        }
        else if (name == "Orc")
        {
            Console.WriteLine("   (\\_._/)");
            Console.WriteLine("    ( o o )");
            Console.WriteLine("     /  V  \\");
        }
        else if (name == "Lobo")
        {
            Console.WriteLine("  /\\_/\\");
            Console.WriteLine(" ( o.o )");
            Console.WriteLine("  > ^ <");
        }
        else
        {
            Console.WriteLine("  .----.");
            Console.WriteLine(" / 0 0 \\");
            Console.WriteLine(" |  -- |");
            Console.WriteLine("  \\____/");
        }
        Console.ResetColor();
    }

    static void Cidade(ref int gold, ref int inventoryCount, string[] inventory, string[] items, int[] prices, string[] rarity,
        ref int attack, ref int defense, ref int maxHealth, ref int health, ref int exp, ref int level, Random rand)
    {
        while (true)
        {
            DrawMarket();
            Console.WriteLine("1 - Comprar item");
            Console.WriteLine("2 - Comprar Upgrade (arma/armadura/poção superior)");
            Console.WriteLine("3 - Vender (múltiplos)");
            Console.WriteLine("4 - Vender tudo");
            Console.WriteLine("5 - Dormir (Descansar)");
            Console.WriteLine("6 - Atualizar preços");
            Console.WriteLine("0 - Sair do mercado");
            Console.Write("Escolha: ");
            string cityChoice = Console.ReadLine();

            if (cityChoice == "1")
            {
                Console.WriteLine("\nItens disponíveis:");
                for (int i = 0; i < items.Length; i++)
                    Console.WriteLine($"{i + 1} - {items[i]} ({prices[i]} moedas) [{rarity[i]}]");
                Console.Write("Escolha (número) ou 0 para sair: ");
                if (int.TryParse(Console.ReadLine(), out int buyChoice) && buyChoice >= 1 && buyChoice <= items.Length)
                {
                    int index = buyChoice - 1;
                    if (gold >= prices[index] && inventoryCount < inventory.Length)
                    {
                        inventory[inventoryCount++] = items[index];
                        gold -= prices[index];
                        Console.WriteLine($"Você comprou {items[index]} por {prices[index]} moedas.");
                    }
                    else Console.WriteLine("Ouro insuficiente ou inventário cheio!");
                }
            }
            else if (cityChoice == "2")
            {
                Console.WriteLine("\nUpgrades disponíveis:");
                Console.WriteLine("1 - Upgrade de Arma (+15 ataque) - 200 moedas");
                Console.WriteLine("2 - Upgrade de Armadura (+12 defesa) - 200 moedas");
                Console.WriteLine("3 - Poção Superior (Poção+) - 150 moedas");
                Console.Write("Escolha: ");
                string up = Console.ReadLine();
                if (up == "1")
                {
                    if (gold >= 200) { gold -= 200; attack += 15; Console.WriteLine("Upgrade de arma comprado! Ataque aumentado."); }
                    else Console.WriteLine("Ouro insuficiente!");
                }
                else if (up == "2")
                {
                    if (gold >= 200) { gold -= 200; defense += 12; Console.WriteLine("Upgrade de armadura comprado! Defesa aumentada."); }
                    else Console.WriteLine("Ouro insuficiente!");
                }
                else if (up == "3")
                {
                    if (gold >= 150 && inventoryCount < inventory.Length) { gold -= 150; inventory[inventoryCount++] = "Poção+"; Console.WriteLine("Poção+ adicionada ao inventário."); }
                    else Console.WriteLine("Ouro insuficiente ou inventário cheio!");
                }
                else Console.WriteLine("Opção inválida!");
            }
            else if (cityChoice == "3")
            {
                if (inventoryCount == 0) { Console.WriteLine("Você não tem itens para vender!"); }
                else
                {
                    Console.WriteLine("\nSeus itens:");
                    for (int i = 0; i < inventoryCount; i++)
                    {
                        int sellPrice = GetSellPrice(inventory[i], items, prices);
                        Console.WriteLine($"{i + 1} - {inventory[i]} (venda: {sellPrice})");
                    }
                    Console.Write("Digite os números dos itens para vender separados por espaço: ");
                    string line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Array.Sort(parts);
                    Array.Reverse(parts);
                    foreach (string s in parts)
                    {
                        if (int.TryParse(s, out int idx) && idx >= 1 && idx <= inventoryCount)
                        {
                            int sellPrice = GetSellPrice(inventory[idx - 1], items, prices);
                            gold += sellPrice;
                            Console.WriteLine($"Vendeu {inventory[idx - 1]} por {sellPrice} moedas.");
                            for (int j = idx - 1; j < inventoryCount - 1; j++) inventory[j] = inventory[j + 1];
                            inventoryCount--;
                        }
                    }
                }
            }
            else if (cityChoice == "4")
            {
                if (inventoryCount == 0) Console.WriteLine("Você não tem itens para vender!");
                else
                {
                    int total = 0;
                    for (int i = 0; i < inventoryCount; i++) total += GetSellPrice(inventory[i], items, prices);
                    Console.WriteLine($"Você vendeu todos os itens por {total} moedas!");
                    inventoryCount = 0;
                    gold += total;
                }
            }
            else if (cityChoice == "5")
            {
                Dormir(ref health, ref maxHealth, ref gold, ref exp, ref level, ref inventory, ref inventoryCount, rand);
            }
            else if (cityChoice == "6")
            {
                Random r = new Random();
                for (int i = 0; i < prices.Length; i++)
                {
                    double change = 1 + (r.NextDouble() - 0.5) * 0.25;
                    prices[i] = Math.Max(1, (int)(prices[i] * change));
                }
                Console.WriteLine("Preços do mercado atualizados!");
            }
            else if (cityChoice == "0")
            {
                Console.WriteLine("Saindo do mercado.");
                return;
            }
            else Console.WriteLine("Opção inválida!");
        }
    }

    static void DrawMarket()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("  ___________________________");
        Console.WriteLine(" /                           \\");
        Console.WriteLine("|   [ MERCADO DE AVENTURA ]   |");
        Console.WriteLine("|  _   _   _   _   _   _   _  |");
        Console.WriteLine("| | |_| |_| |_| |_| |_| |_| | |");
        Console.WriteLine("| |   MERCADOR: Bem-vindo!   | |");
        Console.WriteLine("| |_______________________| | |");
        Console.WriteLine("|_____________________________|");
        Console.ResetColor();
    }

    static int GetSellPrice(string item, string[] marketItems, int[] marketPrices)
    {
        for (int i = 0; i < marketItems.Length; i++)
        {
            if (marketItems[i] == item) return Math.Max(1, marketPrices[i] / 2);
        }
        if (item == "Poção") return 5;
        if (item == "Poção+") return 75;
        return 2;
    }

    static void Dormir(ref int health, ref int maxHealth, ref int gold, ref int exp, ref int level,
        ref string[] inventory, ref int inventoryCount, Random rand)
    {
        Console.Clear();
        string cama = @"
         _______________
        ||             ||
        ||   [_____]   ||
        ||     | |     ||
        ||_____|_|_____||
        |_______________|
              || ||
             (_____)
        ";
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(cama);
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Você deitou na cama para descansar...");
        Console.ResetColor();

        string[] mensagens = { "Descansando...", "Zzz...", "Você está recuperando suas forças...", "Quase pronto..." };

        for (int i = 0; i < mensagens.Length; i++)
        {
            Console.Write(mensagens[i]);
            for (int j = 0; j < 3; j++)
            {
                Console.Write(".");
                Thread.Sleep(400);
            }
            Console.WriteLine();
            Thread.Sleep(500);
        }

        int healAmount = maxHealth;
        health = maxHealth;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nVocê acordou revigorado! Vida totalmente recuperada.");
        Console.ResetColor();

        double dreamChance = rand.NextDouble();
        if (dreamChance < 0.15)
        {
            int lost = rand.Next(5, 21);
            health -= lost;
            if (health < 1) health = 1;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Você teve um pesadelo e perdeu {lost} de vida durante o sono...");
            Console.ResetColor();
        }
        else if (dreamChance < 0.45)
        {
            int foundGold = rand.Next(20, 201);
            gold += foundGold;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Você sonhou com um baú e encontrou {foundGold} moedas ao acordar!");
            Console.ResetColor();
        }
        else if (dreamChance < 0.7)
        {
            int gainedExp = rand.Next(10, 101);
            exp += gainedExp;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Você teve um sonho inspirador e ganhou {gainedExp} XP!");
            Console.ResetColor();
        }
        else
        {
            if (inventoryCount < inventory.Length)
            {
                inventory[inventoryCount++] = "Poção";
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Você sonhou com um curandeiro e recebeu 1 Poção!");
                Console.ResetColor();
            }
        }

        Thread.Sleep(600);
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey(true);
    }

    static void ChefaoASCII(ref int health, ref int gold, ref int exp, ref string[] inventory, ref int inventoryCount,
        int attack, int defense, ref int maxHealth, ref int level, Random rand, string[] marketItems)
    {
        string[] bosses = { "Dragão Ancião", "Titã de Pedra", "Demônio do Fogo", "Serpente Marinha", "Deus das Sombras" };
        int id = rand.Next(bosses.Length);
        string boss = bosses[id];

        int bossHealth = 1500 + rand.Next(300, 1200) + level * 10;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n=== {boss} apareceu! ===");
        if (boss == "Dragão Ancião")
        {
            Console.WriteLine("                      / \\  //\\");
            Console.WriteLine("             |\\___/| /   \\//  .\\");
            Console.WriteLine("             /O  O  \\/_/   //  | \\ \\");
            Console.WriteLine("            /     /  \\/_  //   |  \\  \\");
            Console.WriteLine("           @___@`    \\/_ //    |   \\   \\");
            Console.WriteLine("              |         \\/_/     |    \\   \\");
            Console.WriteLine("              |  ARKAN   /       |     \\   \\");
        }
        else if (boss == "Titã de Pedra")
        {
            Console.WriteLine("            ____");
            Console.WriteLine("          .-\" /  `-.");
            Console.WriteLine("         /   /  .-\"\\");
            Console.WriteLine("        /   /  /   |");
            Console.WriteLine("       |   |  |    |");
            Console.WriteLine("       |   |  |    |");
            Console.WriteLine("      /____|  |____\\");
        }
        else if (boss == "Demônio do Fogo")
        {
            Console.WriteLine("              (  )");
            Console.WriteLine("             (   )");
            Console.WriteLine("            (     )");
            Console.WriteLine("           /|     |\\");
            Console.WriteLine("          /_|  .  |_\\");
            Console.WriteLine("           /  \\_/  \\");
        }
        else if (boss == "Serpente Marinha")
        {
            Console.WriteLine("        ~~~      ~~~~~~");
            Console.WriteLine("      ~~   ~~  ~~   ~~");
            Console.WriteLine("    ~~        ~~      ~~");
            Console.WriteLine("   ~   ~  ~   ~  ~   ~  ~");
        }
        else
        {
            Console.WriteLine("        .-''''-.");
            Console.WriteLine("       /  .--.  \\");
            Console.WriteLine("      /  /    \\  \\");
            Console.WriteLine("     |  |      |  |");
            Console.WriteLine("    (|  |      |  |)");
            Console.WriteLine("     |  |  __  |  |");
        }
        Console.ResetColor();

        while (bossHealth > 0 && health > 0)
        {
            Console.WriteLine($"\nSua vida: {health} | Vida do {boss}: {bossHealth}");
            Console.WriteLine("1 - Atacar  2 - Usar Poção  3 - Habilidade (dano extra com recoil)");
            string action = Console.ReadLine();
            if (action == "1")
            {
                int damage = attack + rand.Next(0, Math.Max(1, attack / 5));
                bossHealth -= damage;
                Console.WriteLine($"Você causou {damage} de dano!");
            }
            else if (action == "2")
            {
                bool used = false;
                for (int i = 0; i < inventoryCount; i++)
                {
                    if (inventory[i] == "Poção+" || inventory[i] == "Poção")
                    {
                        int heal = inventory[i] == "Poção+" ? 120 : 50;
                        health += heal;
                        if (health > maxHealth) health = maxHealth;
                        Console.WriteLine($"Você usou {inventory[i]} e recuperou {heal} de vida!");
                        for (int j = i; j < inventoryCount - 1; j++) inventory[j] = inventory[j + 1];
                        inventoryCount--;
                        used = true;
                        break;
                    }
                }
                if (!used) Console.WriteLine("Você não tem Poções!");
            }
            else if (action == "3")
            {
                int damage = (int)((attack + rand.Next(10, Math.Max(20, attack / 4))) * 1.25);
                bossHealth -= damage;
                Console.WriteLine($"Você usou sua habilidade e causou {damage} de dano!");
                if (rand.NextDouble() < 0.25)
                {
                    int recoil = rand.Next(20, 81);
                    health -= recoil;
                    Console.WriteLine($"Recuou mal e sofreu {recoil} de dano!");
                }
            }
            else Console.WriteLine("Ação inválida!");

            if (bossHealth > 0)
            {
                int bossDamage = 50 + rand.Next(0, 60) + level / 2 - defense / 3;
                if (bossDamage < 0) bossDamage = 0;
                health -= bossDamage;
                Console.WriteLine($"{boss} atacou e causou {bossDamage} de dano!");
            }
        }

        if (health > 0)
        {
            int rewardGold = 400 + rand.Next(0, 801) + level * 5;
            int rewardExp = 300 + rand.Next(0, 501) + level * 3;
            gold += rewardGold;
            exp += rewardExp;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nVocê derrotou {boss}! Ganhou {rewardGold} moedas e {rewardExp} XP!");
            Console.ResetColor();
            if (rand.NextDouble() < 0.7 && inventoryCount < inventory.Length)
            {
                inventory[inventoryCount++] = marketItems[rand.Next(marketItems.Length)];
                Console.WriteLine("Você recebeu loot raro!");
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nVocê foi derrotado por {boss}...");
            Console.ResetColor();
        }

        if (exp >= level * 100 && level < 1000)
        {
            level++;
            maxHealth += 40;
            exp = 0;
            health = maxHealth;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nSubiu para o nível {level}! Vida máxima aumentada.");
            Console.ResetColor();
        }
    }

    static void Cheats(ref int health, ref int maxHealth, ref int gold, ref int attack, ref int defense, ref int exp, ref int level, string[] inventory, ref int inventoryCount, Random r)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n=== MENU DE CHEATS (avançado) ===");
        Console.WriteLine("1 - +1000 Ouro");
        Console.WriteLine("2 - +500 XP");
        Console.WriteLine("3 - +5 Poções (Poção)");
        Console.WriteLine("4 - Poções superiores (5 x Poção+)");
        Console.WriteLine("5 - Definir ataque manualmente");
        Console.WriteLine("6 - Definir defesa manualmente");
        Console.WriteLine("7 - Definir nível (1 - 1000)");
        Console.WriteLine("8 - Definir vida atual");
        Console.WriteLine("9 - Encher inventário com itens aleatórios");
        Console.WriteLine("10 - Nivel máximo 1000 (cheat extremo)");
        Console.WriteLine("0 - Voltar");
        Console.ResetColor();
        Console.Write("Escolha: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                gold += 1000;
                Console.WriteLine("1000 moedas adicionadas!");
                break;
            case "2":
                exp += 500;
                Console.WriteLine("500 XP adicionados!");
                break;
            case "3":
                for (int i = 0; i < 5 && inventoryCount < inventory.Length; i++) inventory[inventoryCount++] = "Poção";
                Console.WriteLine("5 Poções adicionadas!");
                break;
            case "4":
                for (int i = 0; i < 5 && inventoryCount < inventory.Length; i++) inventory[inventoryCount++] = "Poção+";
                Console.WriteLine("5 Poções+ adicionadas!");
                break;
            case "5":
                Console.Write("Digite o novo valor de ataque: ");
                if (int.TryParse(Console.ReadLine(), out int a)) { attack = a; Console.WriteLine($"Ataque definido para {attack}."); }
                else Console.WriteLine("Valor inválido.");
                break;
            case "6":
                Console.Write("Digite o novo valor de defesa: ");
                if (int.TryParse(Console.ReadLine(), out int d)) { defense = d; Console.WriteLine($"Defesa definida para {defense}."); }
                else Console.WriteLine("Valor inválido.");
                break;
            case "7":
                Console.Write("Digite o nível desejado (1 - 1000): ");
                if (int.TryParse(Console.ReadLine(), out int lvl))
                {
                    level = Math.Max(1, Math.Min(1000, lvl));
                    Console.WriteLine($"Nível definido para {level}.");
                }
                else Console.WriteLine("Valor inválido.");
                break;
            case "8":
                Console.Write("Digite a nova vida atual: ");
                if (int.TryParse(Console.ReadLine(), out int hv))
                {
                    health = hv;
                    if (hv > maxHealth) maxHealth = hv;
                    Console.WriteLine($"Vida atual definida para {health} (max {maxHealth}).");
                }
                else Console.WriteLine("Valor inválido.");
                break;
            case "9":
                string[] possible = { "Poção", "Poção+", "Espada", "Escudo", "Anel Mágico", "Armadura", "Cristal Raro" };
                while (inventoryCount < inventory.Length)
                {
                    inventory[inventoryCount++] = possible[r.Next(possible.Length)];
                }
                Console.WriteLine("Inventário preenchido com itens aleatórios!");
                break;
            case "10":
                level = 1000;
                exp = 0;
                maxHealth = 20000;
                health = maxHealth;
                attack = Math.Max(attack, 2000);
                defense = Math.Max(defense, 1000);
                Console.WriteLine("Você virou um herói nível 1000 (tudo aumentado)!");
                break;
            case "0":
                Console.WriteLine("Saindo do menu de cheats.");
                break;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
    }

    static void Status(string name, int health, int maxHealth, int gold, int level, int exp, int attack, int defense)
    {
        Console.WriteLine($"\nNome: {name}");
        DrawHero(level);
        Console.WriteLine($"Vida: {health}/{maxHealth}");
        Console.WriteLine($"Ouro: {gold}");
        Console.WriteLine($"Nível: {level}  XP: {exp}");
        Console.WriteLine($"Ataque: {attack}");
        Console.WriteLine($"Defesa: {defense}");
    }

    static void DrawHero(int level)
    {
        if (level < 10)
        {
            Console.WriteLine("   O");
            Console.WriteLine("  /|\\  Um herói iniciante");
            Console.WriteLine("  / \\");
        }
        else if (level < 50)
        {
            Console.WriteLine("   /^\\");
            Console.WriteLine("  /o o\\  Herói em crescimento");
            Console.WriteLine("  \\ - /");
            Console.WriteLine("  /| |\\");
        }
        else if (level < 200)
        {
            Console.WriteLine("   /\\");
            Console.WriteLine("  (==)  Guerreiro experiente");
            Console.WriteLine("  /||\\");
            Console.WriteLine("   /\\");
        }
        else if (level < 600)
        {
            Console.WriteLine("   .-.");
            Console.WriteLine("  (o o)  Campeão lendário");
            Console.WriteLine("  |=|=|");
            Console.WriteLine("  / | \\");
        }
        else
        {
            Console.WriteLine("   /\\_/\\");
            Console.WriteLine("  ( O O )  Herói Épico (Nível Máximo)");
            Console.WriteLine("   > ^ <");
            Console.WriteLine("  /|   |\\");
        }

    }

    static void Inventario(string[] inventory, int inventoryCount)
    {
        Console.WriteLine("\nInventário:");
        if (inventoryCount == 0) { Console.WriteLine("- Vazio -"); return; }
        for (int i = 0; i < inventoryCount; i++) Console.WriteLine($"{i + 1} - {inventory[i]}");
    }

    static void MelhorarAtaque(ref int attack, ref int gold)
    {
        if (gold >= 50)
        {
            attack += 5;
            gold -= 50;
            Console.WriteLine($"Ataque melhorado! Novo ataque: {attack}");
        }
        else Console.WriteLine("Ouro insuficiente!");
    }

    static void MelhorarDefesa(ref int defense, ref int gold)
    {
        if (gold >= 50)
        {
            defense += 5;
            gold -= 50;
            Console.WriteLine($"Defesa melhorada! Nova defesa: {defense}");
        }
        else Console.WriteLine("Ouro insuficiente!");
    }

    static void Treinar(ref int exp, ref int level, ref int maxHealth, ref int health)
    {
        int gainedExp = 30;
        exp += gainedExp;
        Console.WriteLine($"Você treinou e ganhou {gainedExp} XP!");
        if (exp >= level * 100 && level < 1000)
        {
            level++;
            maxHealth += 30;
            health = maxHealth;
            exp = 0;
            Console.WriteLine($"Parabéns! Você subiu para o nível {level}!");
        }
    }
}
