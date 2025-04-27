using Microsoft.AspNetCore.Mvc.ApiExplorer;
using NormPass.Entities;

namespace NormPass.Services
{
    public class PasswordGenService
    {
        public readonly Random _random = new Random();

        public string GeneratePassword(PasswordGen request)
        {
            var chars = "";

            if (request.IncludeLowerCase)
                chars += "abcdefghijklmnopqrstuvwxyz";

            if (request.IncludeUpperCase)
                chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (request.IncludeNumbers)
                chars += "0123456789";

            if (request.IncludeSymbols)
                chars += "!@£$%^&*()+_?";

            if (string.IsNullOrEmpty(chars))
                return "";

            return new string(Enumerable.Repeat(chars, request.Length)
                .Select(l => l[_random.Next(l.Length)]).ToArray());
        }
    }
}
