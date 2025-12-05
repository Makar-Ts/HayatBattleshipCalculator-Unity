using System.Text;

public class RandomStringGenerator
{
    private readonly static string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static string GenerateRandomString(int length)
    {
        StringBuilder result = new(length);
        System.Random random = new();

        for (int i = 0; i < length; i++)
        {
            int randomIndex = random.Next(0, characters.Length);
            result.Append(characters[randomIndex]);
        }

        return result.ToString();
    }
}