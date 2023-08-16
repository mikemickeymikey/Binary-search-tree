using ArbreBinariCerca;

public class Program
{
    public static void Main(string[] args)
    {
        ArbreBinariCerca<int, string> abc = new ArbreBinariCerca<int, string>();
        abc.Afegeix(10, "a");
        abc.Afegeix(9, "b");
        abc.Afegeix(11, "c");
        abc.Afegeix(8, "d");
        abc.Afegeix(1, "e");
        string valor1 = abc.ConsultaValor(9);
        string valor2 = abc.ConsultaValor(1);
        if (valor1 != null) Console.WriteLine("Valor: " + valor1);
        else Console.WriteLine("Valor no trobat.");
        if (valor2 != null) Console.WriteLine("Valor: " + valor2);
        else Console.WriteLine("Node no trobat.");
        /*abc.Elimina(abc.ConsultaNode(9));
        if (valor1 != null) Console.WriteLine("Valor: " + valor1);
        else Console.WriteLine("Valor no trobat.");*/
        List<KeyValuePair<int, string>> llistaPre = abc.PreOrdre();
        List<KeyValuePair<int, string>> llistaPost = abc.PostOrdre();
        List<KeyValuePair<int, string>> llistaIn = abc.InOrdre();
        Console.WriteLine("Pre-Ordre: ");
        foreach(KeyValuePair<int, string> pair in llistaPre) Console.WriteLine(pair.Key + " " + pair.Value);
        Console.WriteLine("Post-Ordre: ");
        foreach (KeyValuePair<int, string> pair in llistaPost) Console.WriteLine(pair.Key + " " + pair.Value);
        Console.WriteLine("In-Ordre: ");
        foreach (KeyValuePair<int, string> pair in llistaIn) Console.WriteLine(pair.Key + " " + pair.Value);
    }
}