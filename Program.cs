using System;
using System.Globalization;

class Program
{
    static decimal precoEtanol, precoGasolina;
    const double MinRazaoUsoGasolina = 0.73;
    const double kmPorLitroEtanol = 7.0;
    const double kmPorLitroGasolina = 10.0;

    static void Main()
    {
        Console.Clear();
        ExibirCabecalho();
        ObterPrecos();
        AnalisarCombustiveis();
    }

    static void ExibirCabecalho()
    {
        Console.WriteLine("--- Etanol ou Gasolina? ---\n");
        Console.WriteLine("Critério: Quando o etanol custar 73% ou mais do valor da gasolina,");
        Console.WriteLine("a gasolina se torna mais vantajosa (considerando o rendimento).\n");
    }

    static void ObterPrecos()
    {
        precoEtanol = ObterPrecoValido("Digite o preço do etanol (R$).....: ");
        precoGasolina = ObterPrecoValido("Digite o preço da gasolina (R$)...: ");
    }

    static decimal ObterPrecoValido(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string input = Console.ReadLine().Replace(',', '.');
            
            if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor) && valor > 0)
            {
                return valor;
            }
            Console.WriteLine("Valor inválido! Digite um número positivo (ex: 4.75 ou 5,20)");
        }
    }

    static void AnalisarCombustiveis()
    {
        double razao = CalcularRazao();
        string recomendacao = ObterRecomendacao(razao);
        var (custoEtanolKm, custoGasolinaKm) = CalcularCustoPorKm();

        ExibirResultados(razao, recomendacao, custoEtanolKm, custoGasolinaKm);
    }

    static double CalcularRazao()
    {
        return Convert.ToDouble(precoEtanol / precoGasolina);
    }

    static string ObterRecomendacao(double razao)
    {
        return razao >= MinRazaoUsoGasolina ? "GASOLINA" : "ETANOL";
    }

    static (decimal, decimal) CalcularCustoPorKm()
    {
        decimal custoKmEtanol = precoEtanol / (decimal)kmPorLitroEtanol;
        decimal custoKmGasolina = precoGasolina / (decimal)kmPorLitroGasolina;
        return (custoKmEtanol, custoKmGasolina);
    }

    static void ExibirResultados(double razao, string recomendacao, decimal custoEtanolKm, decimal custoGasolinaKm)
    {
        Console.WriteLine($"\nO preço do etanol corresponde a {razao*100:N1}% do preço da gasolina.");
        Console.WriteLine($"\nMotivo: {(recomendacao == "GASOLINA" ? "Acima" : "Abaixo")} do limiar de 73%");

        Console.WriteLine($"\nCusto por km (estimado):");
        Console.WriteLine($"Etanol: R${custoEtanolKm:N3}/km");
        Console.WriteLine($"Gasolina: R${custoGasolinaKm:N3}/km");
        
        Console.WriteLine($"\nRecomendação final: Abasteça com {recomendacao}.");
    }
}