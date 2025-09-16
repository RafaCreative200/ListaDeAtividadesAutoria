using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static Dictionary<string, string> passwords = new Dictionary<string, string>();

    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Gerenciador de Senhas Console ===\n");
        Console.ResetColor();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("1 - Gerar nova senha");
            Console.WriteLine("2 - Listar senhas armazenadas");
            Console.WriteLine("3 - Buscar senha por site");
            Console.WriteLine("4 - Sair");
            Console.ResetColor();
            Console.Write("Opção: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GeneratePassword();
                    break;
                case "2":
                    ListPasswords();
                    break;
                case "3":
                    SearchPassword();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    static void GeneratePassword()
    {
        Console.Write("Digite o nome do site/aplicativo: ");
        string site = Console.ReadLine();

        string password = CreateRandomPassword(12);
        string encrypted = Encrypt(password);

        passwords[site] = encrypted;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Senha gerada para {site}: {password}");
        Console.ResetColor();
    }
    static void ListPasswords()
    {
        Console.WriteLine("\nSenhas armazenadas:");
        foreach (var kvp in passwords)
        {
            Console.WriteLine($"{kvp.Key}: {Decrypt(kvp.Value)}");
        }
    }
    static void SearchPassword()
    {
        Console.Write("Digite o nome do site/aplicativo: ");
        string site = Console.ReadLine();

        if (passwords.ContainsKey(site))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Senha: {Decrypt(passwords[site])}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Site não encontrado!");
            Console.ResetColor();
        }
    }
        static string CreateRandomPassword(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";
        Random rand = new Random();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            sb.Append(chars[rand.Next(chars.Length)]);
        }
        return sb.ToString();
    }

    static string Encrypt(string text)
    {
        byte[] data = Encoding.UTF8.GetBytes(text);
        using (SHA256 sha = SHA256.Create())
        {
            byte[] hash = sha.ComputeHash(data);
            return Convert.ToBase64String(hash);
        }
    }

    static string Decrypt(string encrypted)
    {
        return "[Senha segura armazenada]";
    }
}
