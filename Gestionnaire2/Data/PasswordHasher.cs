namespace Gestionnaire2.Data
{
    using System.Security.Cryptography;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;

    public class PasswordHasher
    {
        private const int SaltSize = 16; // Taille du sel en octets
        private const int KeySize = 32; // Taille de la clé en octets
        private const int Iterations = 10000; // Nombre d'itérations

        public static string HashPassword(string password)
        {
            // Générer un sel unique
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Générer le hachage avec le sel
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: KeySize));

            // Retourner le sel + hachage en base64 (concaténés pour stockage)
            return $"{Convert.ToBase64String(salt)}:{hash}";
        }

        public static bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Séparer le sel et le hachage stockés
            var parts = storedPasswordHash.Split(':');
            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            string storedHash = parts[1];

            // Générer le hachage pour le mot de passe saisi avec le sel existant
            string enteredHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: KeySize));

            // Comparer les deux hachages
            return enteredHash == storedHash;
        }
    }
}
