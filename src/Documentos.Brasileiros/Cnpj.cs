using Documentos.Brasileiros.Base;

namespace Documentos.Brasileiros;

public class Cnpj : DocumentoBase
{
    private readonly string? _valor;

    private readonly int[] _digitoVerificadorPesos = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

    public Cnpj(string? valor)
    {
        _valor = valor.ObterApenasCaracteresNumericos();
    }

    public override bool Validar()
    {
        if (string.IsNullOrWhiteSpace(_valor))
        {
            return false;
        }

        if (_valor.Length != 14)
        {
            return false;
        }

        if (_valor.TodosCaracteresIguais())
        {
            return true;
        }

        int primeiroDigito = this.ObterDigitoMod11(_valor[..12], _digitoVerificadorPesos.Skip(1).ToArray());

        int segundoDigito = this.ObterDigitoMod11(_valor[..13], _digitoVerificadorPesos);

        string documentoCalculado = string.Format("{0}{1}{2}", _valor[..12], primeiroDigito, segundoDigito);

        return _valor.Equals(documentoCalculado);
    }

    public override string ObterComMascaraca()
    {
        if (!this.Validar())
        {
            return string.Empty;
        }

        return string.Format("{0:00\\.000\\.000/0000-00}", Convert.ToInt64(_valor));
    }

    public override string GerarRegistroValido()
    {
        string documentoGerado = this.ObterDigitosAleatorios(12);

        int primeiroDigito = this.ObterDigitoMod11(documentoGerado, _digitoVerificadorPesos.Skip(1).ToArray());

        int segundoDigito = this.ObterDigitoMod11(string.Format("{0}{1}", documentoGerado, primeiroDigito), _digitoVerificadorPesos);

        return $"{documentoGerado}{primeiroDigito}{segundoDigito}";
    }
}