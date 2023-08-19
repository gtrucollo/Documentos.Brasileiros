using Documentos.Brasileiros.Base;

namespace Documentos.Brasileiros;

public class CpfCnpj : DocumentoBase
{
    private readonly string? _valor;

    public CpfCnpj(string? valor)
    {
        _valor = valor.ObterApenasCaracteresNumericos();
    }

    public override bool Validar()
    {
        if (string.IsNullOrWhiteSpace(_valor))
        {
            return false;
        }

        if (_valor.Length > 11)
        {
            return new Cnpj(_valor).Validar();
        }

        return new Cpf(_valor).Validar();
    }

    public override string ObterComMascaraca()
    {
        if (string.IsNullOrWhiteSpace(_valor))
        {
            return string.Empty;
        }

        if (!this.Validar())
        {
            return string.Empty;
        }

        if (_valor.Length > 11)
        {
            return new Cnpj(_valor).ObterComMascaraca();
        }

        return new Cpf(_valor).ObterComMascaraca();
    }
}