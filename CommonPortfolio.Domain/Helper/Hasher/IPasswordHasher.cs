namespace CommonPortfolio.Domain.Helper.Hasher;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool ValidatePassword(string password, string hashedPassword);
}
