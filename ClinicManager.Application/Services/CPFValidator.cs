using System.Text.RegularExpressions;

namespace ClinicManager.Api.Services
{
    public class CPFValidator
    {
        public static bool IsValid(string cpf)
        {
            // Remover caracteres não numéricos
            cpf = Regex.Replace(cpf, "[^0-9]", "");

            if (cpf.Length != 11)
                return false;

            // Verificar se todos os dígitos são iguais, o que torna o CPF inválido
            var invalidNumbers = new string[] { "00000000000", "11111111111", "22222222222",
                "33333333333", "44444444444", "55555555555", "66666666666", "77777777777",
                "88888888888", "99999999999" };

            if (Array.Exists(invalidNumbers, element => element == cpf))
                return false;

            // Verificação dos dígitos verificadores
            for (int i = 9; i < 11; i++)
            {
                int sum = 0, factor = i + 1;

                for (int j = 0; j < i; j++)
                    sum += int.Parse(cpf[j].ToString()) * (factor--);

                int digit = sum % 11 < 2 ? 0 : 11 - (sum % 11);

                if (digit != int.Parse(cpf[i].ToString()))
                    return false;
            }

            return true;
        }
    }
}
