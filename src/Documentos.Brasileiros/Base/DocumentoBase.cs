namespace Documentos.Brasileiros.Base;

public abstract class DocumentoBase
{
    public abstract bool Validar();

    public abstract string ObterComMascaraca();

    public abstract string GerarRegistroValido();

    public virtual int ObterDigitoMod11(string valor, int[] pesos)
    {
        int sum = 0;
        for (int i = 0; i < pesos.Length; ++i)
        {
            sum += pesos[i] * int.Parse(valor[i].ToString());
        }

        int digit = sum % 11;

        return digit < 2 ? 0 : (11 - digit);
    }

    public virtual string ObterDigitosAleatorios(int tamanho)
    {
        Random rand = new();
        string randomDigits = string.Empty;
        for (int i = 0; i < tamanho; i++)
        {
            randomDigits += rand.Next(0, 9);
        }

        return randomDigits;
    }
}