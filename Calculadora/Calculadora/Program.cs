using System;

class CalculadoraDesconto
{
    static void Main()
    {
        Console.Write("Digite o valor do produto: R$");
        double valorProduto = double.Parse(Console.ReadLine());
        double descontoPercentual;
        switch (valorProduto)
        {
            case var v when v < 100:
                descontoPercentual = 5;
                break;
            case var v when v <= 500:
                descontoPercentual = 10;
                break;
            default:
                descontoPercentual = 15;
                break;
        }
        Console.Write($"Deseja alterar o desconto sugerido ({descontoPercentual}%)? Digite a nova % ou 0 para manter: ");
        double descontoUsuario = double.Parse(Console.ReadLine());
        if (descontoUsuario > 0)
            descontoPercentual = descontoUsuario;
        double desconto = valorProduto * (descontoPercentual / 100);
        double valorFinal = valorProduto - desconto;
        Console.WriteLine($"Desconto aplicado: {descontoPercentual}% -> R${desconto:F2}");
        Console.WriteLine($"Valor final a pagar: R${valorFinal:F2}");
    }
}
