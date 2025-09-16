// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== SIMULADOR DE INVESTIMENTOS ===\n");
        Console.Write("Digite seu nome: ");
        string nome = Console.ReadLine();
        Console.Write("Digite seu capital inicial (R$): ");
        double capitalInicial = LerDouble();
        Console.Write("Digite o aporte mensal (R$): ");
        double aporteMensal = LerDouble();
        Console.Write("Digite o período de investimento (em meses): ");
        int meses = LerInt();
        Console.WriteLine("\nEscolha seu perfil de risco:");
        Console.WriteLine("1 - Conservador (Rendimento ~0,5% ao mês)");
        Console.WriteLine("2 - Moderado (Rendimento ~1,0% ao mês)");
        Console.WriteLine("3 - Maluco (Rendimento ~1,5% ao mês)");
        Console.Write("Digite a opção (1, 2 ou 3): ");
        int perfil = LerIntEntre(1, 3);

        double taxaJurosMensal = perfil switch
        {
            1 => 0.005,
            2 => 0.01,
            3 => 0.015,
            _ => 0.005
        };

        Console.WriteLine("\nSimulando investimento...\n");

        double saldo = capitalInicial;
        List<string> historico = new List<string>();
        for (int i = 1; i <= meses; i++)
        {
            saldo += aporteMensal;
            double rendimento = saldo * taxaJurosMensal;
            saldo += rendimento;

            historico.Add($"Mês {i:D2}: Saldo = {saldo:C2}, Rendimento = {rendimento:C2}");
        }

        Console.WriteLine("=== RELATÓRIO DE INVESTIMENTO ===");
        Console.WriteLine($"Investidor: {nome}");
        Console.WriteLine($"Perfil: {(perfil == 1 ? "Conservador" : perfil == 2 ? "Moderado" : "Maluco")}");
        Console.WriteLine($"Capital Inicial: {capitalInicial:C2}");
        Console.WriteLine($"Aporte Mensal: {aporteMensal:C2}");
        Console.WriteLine($"Período: {meses} meses");
        Console.WriteLine($"Saldo Final: {saldo:C2}\n");

        Console.WriteLine("=== HISTÓRICO MENSAL ===");
        foreach (var item in historico)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\nSimulação concluída!");
        Console.ReadKey();
    }

    static double LerDouble()
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double valor))
                return valor;
            Console.Write("Valor inválido! Digite novamente: ");
        }
    }
    static int LerInt()
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int valor))
                return valor;
            Console.Write("Valor inválido! Digite novamente: ");
        }
    }
    static int LerIntEntre(int min, int max)
    {
        while (true)
        {
            int valor = LerInt();
            if (valor >= min && valor <= max)
                return valor;
            Console.Write($"Valor inválido! Digite um número entre {min} e {max}: ");
        }
    }
}

