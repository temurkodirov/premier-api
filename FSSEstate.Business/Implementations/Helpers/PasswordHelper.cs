namespace FSSEstate.Business.Implementations.Helpers
{
    public static class PasswordHelper
    {
        private static string salt = Guid.NewGuid().ToString();
        public static (string hash, string salt) PasswordHash(string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password + salt);
            return (hash: passwordHash, salt);
        }
        public static bool Verify(string newPassword, string oldPassword, string salt)
        {
            return BCrypt.Net.BCrypt.Verify(newPassword + salt, oldPassword);
        }

    }
}
