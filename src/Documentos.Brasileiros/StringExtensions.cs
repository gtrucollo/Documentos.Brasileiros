using System.Text.RegularExpressions;

namespace Documentos.Brasileiros;

internal static class StringExtensions
{
    private static readonly Regex _regexApenasNumeros = new(@"[^\d]");

    public static string? RemoverMascara(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        };

        return _regexApenasNumeros.Replace(value, string.Empty).Trim();
    }

    public static bool TodosCaracteresIguais(this string? value)
    {
        return value?.Distinct().Count() == 1;
    }
}
