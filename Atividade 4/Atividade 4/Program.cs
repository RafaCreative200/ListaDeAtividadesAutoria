using System;

class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== AVENTURA ÉPICA 9.0 ===");
        Console.ResetColor();

        string playerName;
        int health = 100;
        int maxHealth = 100;
        int gold = 100;
        int level = 1;
        int exp = 0;
        int attack = 10;
        int defense = 5;
        string[] inventory = new string[20];
        int inventoryCount = 0;
        Random rand = new Random();

        string[] marketItems = { "Poção", "Espada", "Escudo", "Anel Mágico", "Armadura", "Cristal Raro" };
        int[] marketPrices = { 10, 50, 50, 100, 75, 300 };

        Console.Write("Digite o nome do herói: ");
        playerName = Console.ReadLine();
        Console.WriteLine($"\nBem-vindo, {playerName}! Sua aventura começa agora...\n");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== MENU DE AÇÕES ===");
            Console.WriteLine("1 - Explorar");
            Console.WriteLine("2 - Visitar cidade");
            Console.WriteLine("3 - Ver status do herói");
            Console.WriteLine("4 - Inventário");
            Console.WriteLine("5 - Melhorar ataque");
            Console.WriteLine("6 - Melhorar defesa");
            Console.WriteLine("7 - Treinar XP");
            Console.WriteLine("8 - Lutar contra chefão");
            Console.WriteLine("9 - Cheats");
            Console.WriteLine("10 - Sair");
            Console.ResetColor();
            Console.Write("Escolha: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ExplorarASCII(ref health, ref gold, ref exp, ref inventory, ref inventoryCount, rand); break;
                case "2": Cidade(ref gold, inventory, ref inventoryCount, marketItems, marketPrices); break;
                case "3": Status(playerName, health, maxHealth, gold, level, exp, attack, defense); break;
                case "4": Inventario(inventory, inventoryCount); break;
                case "5": MelhorarAtaque(ref attack, ref gold); break;
                case "6": MelhorarDefesa(ref defense, ref gold); break;
                case "7": Treinar(ref exp, ref level, ref maxHealth, ref health); break;
                case "8": ChefaoASCII(ref health, ref gold, ref exp, ref inventory, ref inventoryCount, attack, defense, rand); break;
                case "9": Cheats(ref health, ref maxHealth, ref gold, ref attack, ref defense, ref exp, ref level, inventory, ref inventoryCount); break;
                case "10": Console.WriteLine("Obrigado por jogar! Até a próxima!"); return;
                default: Console.WriteLine("Opção inválida!"); break;
            }
        }
    }

    static void ExplorarASCII(ref int health, ref int gold, ref int exp, ref string[] inventory, ref int inventoryCount, Random rand)
    {
        string[] enemies = { "Goblin", "Orc", "Lobo" };
        string enemy = enemies[rand.Next(enemies.Length)];
        int enemyHealth = rand.Next(20, 61);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nUm {enemy} aparece!");
        Console.WriteLine("   (\\_._/)  ");
        Console.WriteLine("   ( o o )  ");
        Console.WriteLine("   /  V  \\  ");
        Console.ResetColor();

        while (enemyHealth > 0 && health > 0)
        {
            Console.WriteLine($"\nSua vida: {health} | Vida do {enemy}: {enemyHealth}");
            Console.WriteLine("1 - Atacar  2 - Usar Poção");
            string action = Console.ReadLine();
            if (action == "1")
            {
                int damage = rand.Next(10, 21);
                enemyHealth -= damage;
                Console.WriteLine($"Você causou {damage} de dano!");
                if (enemyHealth > 0)
                {
                    int enemyDamage = rand.Next(5, 15);
                    health -= enemyDamage;
                    Console.WriteLine($"{enemy} atacou e causou {enemyDamage} de dano!");
                }
            }
            else if (action == "2")
            {
                bool used = false;
                for (int i = 0; i < inventoryCount; i++)
                {
                    if (inventory[i] == "Poção")
                    {
                        health += 30;
                        if (health > 100) health = 100;
                        Console.WriteLine("Você usou uma Poção e recuperou 30 de vida!");
                        for (int j = i; j < inventoryCount - 1; j++) inventory[j] = inventory[j + 1];
                        inventoryCount--;
                        used = true;
                        break;
                    }
                }
                if (!used) Console.WriteLine("Você não tem Poções!");
            }
        }

        if (health > 0)
        {
            int lootGold = rand.Next(10, 101);
            gold += lootGold;
            int gainedExp = rand.Next(10, 31);
            exp += gainedExp;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nVocê derrotou o {enemy} e ganhou {lootGold} moedas e {gainedExp} XP!");
            Console.ResetColor();
        }
        else Console.WriteLine("\nVocê morreu!");
    }

    static void ChefaoASCII(ref int health, ref int gold, ref int exp, ref string[] inventory, ref int inventoryCount, int attack, int defense, Random rand)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nCHEFÃO!");
        Console.WriteLine("   (X_X)  ");
        Console.WriteLine("   /|\\   ");
        Console.WriteLine("   / \\   ");
        Console.ResetColor();

        int bossHealth = 150;
        while (bossHealth > 0 && health > 0)
        {
            Console.WriteLine($"\nSua vida: {health} | Vida do CHEFÃO: {bossHealth}");
            Console.WriteLine("1 - Atacar  2 - Usar Poção");
            string action = Console.ReadLine();
            if (action == "1")
            {
                int damage = attack + rand.Next(5, 16);
                bossHealth -= damage;
                Console.WriteLine($"Você causou {damage} de dano!");
                if (bossHealth > 0)
                {
                    int bossDamage = 15 + rand.Next(0, 10) - defense;
                    if (bossDamage < 0) bossDamage = 0;
                    health -= bossDamage;
                    Console.WriteLine($"CHEFÃO atacou e causou {bossDamage} de dano!");
                }
            }
            else if (action == "2")
            {
                bool used = false;
                for (int i = 0; i < inventoryCount; i++)
                {
                    if (inventory[i] == "Poção")
                    {
                        health += 30;
                        if (health > 100) health = 100;
                        Console.WriteLine("Você usou uma Poção e recuperou 30 de vida!");
                        for (int j = i; j < inventoryCount - 1; j++) inventory[j] = inventory[j + 1];
                        inventoryCount--;
                        used = true;
                        break;
                    }
                }
                if (!used) Console.WriteLine("Você não tem Poções!");
            }
        }

        if (health > 0)
        {
            gold += 200;
            exp += 100;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nVocê derrotou o CHEFÃO! Ganhou 200 moedas e 100 XP!");
            Console.ResetColor();
        }
        else Console.WriteLine("Você morreu contra o CHEFÃO!");
    }

    static void Cheats(ref int health, ref int maxHealth, ref int gold, ref int attack, ref int defense, ref int exp, ref int level, string[] inventory, ref int inventoryCount)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n=== MENU DE CHEATS ===");
        Console.WriteLine("1 - Adicionar ouro");
        Console.WriteLine("2 - Adicionar XP");
        Console.WriteLine("3 - Adicionar Poção");
        Console.WriteLine("4 - Melhorar ataque");
        Console.WriteLine("5 - Melhorar defesa");
        Console.WriteLine("6 - Recuperar vida");
        Console.ResetColor();
        Console.Write("Escolha: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1": gold += 50000; Console.WriteLine("500 moedas adicionadas!"); break;
            case "2": exp += 10000; Console.WriteLine("100 XP adicionados!"); break;
            case "3": if (inventoryCount < 200) { inventory[inventoryCount++] = "Poção"; Console.WriteLine("Poção adicionada!"); } break;
            case "4": attack += 10000; Console.WriteLine("Ataque aumentado!"); break;
            case "5": defense += 10000; Console.WriteLine("Defesa aumentada!"); break;
            case "6": health = maxHealth; Console.WriteLine("Vida recuperada!"); break;
            default: Console.WriteLine("Opção inválida!"); break;
        }
    }

    static void Status(string name, int health, int maxHealth, int gold, int level, int exp, int attack, int defense)
    {
        Console.WriteLine($"\nNome: {name}\nVida: {health}/{maxHealth}\nOuro: {gold}\nNível: {level}  XP: {exp}\nAtaque: {attack}\nDefesa: {defense}");
    }

    static void Inventario(string[] inventory, int inventoryCount)
    {
        Console.WriteLine("\nInventário:");
        if (inventoryCount == 0)
        {
            Console.WriteLine("O inventário está vazio.");
        }
        else
        {
            for (int i = 0; i < inventoryCount; i++)
            {
                Console.WriteLine($"{i + 1} - {inventory[i]}");
            }
        }
    }

    static void Cidade(ref int gold, string[] inventory, ref int inventoryCount, string[] marketItems, int[] marketPrices)
    {
        Console.WriteLine("\nCidade ainda não implementada.");
    }

    static void MelhorarAtaque(ref int attack, ref int gold)
    {
        Console.WriteLine("\nMelhorar ataque ainda não implementado.");
    }

    static void MelhorarDefesa(ref int defense, ref int gold)
    {
        Console.WriteLine("\nMelhorar defesa ainda não implementado.");
    }

    static void Treinar(ref int exp, ref int level, ref int maxHealth, ref int health)
    {
        Console.WriteLine("\nTreinar ainda não implementado.");
    }
}
