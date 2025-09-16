// See https://aka.ms/new-console-template for more information
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Sistema Escolar ===");
        Console.Write("Quantos alunos deseja cadastrar? ");
        int quantidadeAlunos = int.Parse(Console.ReadLine());
        string[] nomes = new string[quantidadeAlunos];
        double[] medias = new double[quantidadeAlunos];
        for (int i = 0; i < quantidadeAlunos; i++)
        {
            Console.WriteLine($"\n--- Aluno {i + 1} ---");
            Console.Write("Digite o nome do aluno: ");
            nomes[i] = Console.ReadLine();
            double[] notas = new double[3];

            for (int j = 0; j < 3; j++)
            {
                Console.Write($"Digite a nota do aluno {j + 1}: ");
                notas[j] = double.Parse(Console.ReadLine());
            }

            medias[i] = (notas[0] + notas[1] + notas[2]) / 3;

            Console.WriteLine($"\nMédia de {nomes[i]}: {medias[i]:F2}");
            if (medias[i] >= 7)
            {
                Console.WriteLine("Situação: Aprovado");
            }
            else if (medias[i] >= 5)
            {
                Console.WriteLine("Situação: Recuperação");
            }
            else
            {
                Console.WriteLine("Situação: Reprovado");
            }
        }
        Console.WriteLine("\n=== Relatório Geral ===");
        for (int i = 0; i < quantidadeAlunos; i++)
        {
            string situacao;
            if (medias[i] >= 7) situacao = "Aprovado";
            else if (medias[i] >= 5) situacao = "Recuperação";
            else situacao = "Reprovado";

            Console.WriteLine($"{i + 1}. {nomes[i]} - Média: {medias[i]:F2} - {situacao}");
        }

        Console.WriteLine("\nFim do programa. Pressione qualquer tecla para sair.");
        Console.ReadKey();
    }
}

