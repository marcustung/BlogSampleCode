private static string HMACSHA256(string message, string key)
{
    var encoding = new System.Text.UTF8Encoding();
    byte[] keyByte = encoding.GetBytes(key);
    byte[] messageBytes = encoding.GetBytes(message);
    using (var hmacSHA256 = new HMACSHA256(keyByte))
    {
        byte[] hashMessage = hmacSHA256.ComputeHash(messageBytes);
        return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
    }
}

HMACSHA256("TEST", "KEY");
// resulg : 615dac1c53c9396d8f69a419a0b2d9393a0461d7ad5f7f3d9beb57264129ef12


//ExtensionMethods

public static class ExtensionMethods
{
    public static string HMACSHA256(this string message, string key)
    {
        var encoding = new System.Text.UTF8Encoding();
        byte[] keyByte = encoding.GetBytes(key);
        byte[] messageBytes = encoding.GetBytes(message);
        using (var hmacSHA256 = new HMACSHA256(keyByte))
        {
            byte[] hashMessage = hmacSHA256.ComputeHash(messageBytes);
            return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
        }
    }
}

static void Main(string[] args)
{
    var result = "TEST".HMACSHA256("KEY");
    Console.WriteLine(result);
}