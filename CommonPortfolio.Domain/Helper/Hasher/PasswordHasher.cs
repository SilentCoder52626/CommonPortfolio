using CommonPortfolio.Domain.Helper.Hasher;

namespace CommonBoilerPlateEight.Domain.Helper;

public class PasswordHasher : IPasswordHasher
{
    private const string Base64SecretKey = "rspdZ40UHl1ZIZeIJQ+AVT+YCrxsx33d0J/s/tC+Cyk=";
    private readonly byte[] _secretKey;

    public PasswordHasher()
    {
        _secretKey = Convert.FromBase64String(Base64SecretKey);
    }

    public string HashPassword(string password)
    {
        var hashed = BCrypt.Net.BCrypt.HashPassword(password);

        return hashed;
    }

    public bool ValidatePassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

}
