using System;

class Program
{
    static void Main()
    {
        Console.Title = "Calculadora Simples";
        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine("====================================");
        Console.WriteLine("         CALCULADORA SIMPLES        ");
        Console.WriteLine("====================================");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Escolha a operação:");
        Console.WriteLine("1 - Soma (+)");
        Console.WriteLine("2 - Subtração (-)");
        Console.WriteLine("3 - Multiplicação (*)");
        Console.WriteLine("4 - Divisão (/)");
        Console.ResetColor();

        Console.Write("\nDigite a opção: ");
        int opcao = Convert.ToInt32(Console.ReadLine());

        Console.Write("\nDigite o primeiro número: ");
        double numero1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Digite o segundo número: ");
        double numero2 = Convert.ToDouble(Console.ReadLine());

        double resultado = 0;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n====================================");

        switch (opcao)
        {
            case 1:
                resultado = numero1 + numero2;
                Console.WriteLine("Resultado da Soma: " + resultado);
                break;
            case 2:
                resultado = numero1 - numero2;
                Console.WriteLine("Resultado da Subtração: " + resultado);
                break;
            case 3:
                resultado = numero1 * numero2;
                Console.WriteLine("Resultado da Multiplicação: " + resultado);
                break;
            case 4:
                if (numero2 != 0)
                {
                    resultado = numero1 / numero2;
                    Console.WriteLine("Resultado da Divisão: " + resultado);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erro: divisão por zero!");
                }
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opção inválida.");
                break;
        }

        Console.WriteLine("====================================");
        Console.ResetColor();
    }
}
