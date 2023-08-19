using Documentos.Brasileiros.Base;

namespace Documentos.Brasileiros;

public class Cpf : DocumentoBase
{
    private readonly string? _valor;

    private readonly int[] _digitoVerificadorPesos = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

    public Cpf(string? valor)
    {
        _valor = valor.RemoverMascara();
    }

    public override bool Validar()
    {
        if (string.IsNullOrWhiteSpace(_valor))
        {
            return false;
        }

        if (_valor.Length != 11)
        {
            return false;
        }

        if (_valor.TodosCaracteresIguais())
        {
            return true;
        }

        int primeiroDigito = this.ObterDigitoMod11(_valor[..9], _digitoVerificadorPesos.Skip(1).ToArray());

        int segundoDigito = this.ObterDigitoMod11(_valor[..10], _digitoVerificadorPesos);

        string documentoCalculado = string.Format("{0}{1}{2}", _valor[..9], primeiroDigito, segundoDigito);

        return _valor.Equals(documentoCalculado);
    }

    public override string ObterComMascaraca()
    {
        if (!this.Validar())
        {
            return string.Empty;
        }

        return string.Format("{0:000\\.000\\.000-00}", Convert.ToInt64(_valor));
    }

    public override string GerarRegistroValido()
    {
        string documentoGerado = this.ObterDigitosAleatorios(9);

        int firstDigit = this.ObterDigitoMod11(documentoGerado, _digitoVerificadorPesos.Skip(1).ToArray());

        int seconDigit = this.ObterDigitoMod11(string.Format("{0}{1}", documentoGerado, firstDigit), _digitoVerificadorPesos);

        return string.Format("{0}{1}{2}", documentoGerado[..9], firstDigit, seconDigit);
    }
}