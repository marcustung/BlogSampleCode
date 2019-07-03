/// <summary>
/// 16位：ComputeHash
/// </summary>
/// <param name="input"></param>
/// <returns></returns>
private string getMd5Method(string input)
{
MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            
byte[] myData = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
StringBuilder sBuilder = new StringBuilder();

for (int i = 0; i < myData.Length; i++)
{
    sBuilder.Append(myData[i].ToString("x"));
}

    return string.Format("ComputeHash(16)：{0}", sBuilder.ToString());
}

/// <summary>
/// 32位加密：ComputeHash
/// </summary>
/// <param name="input"></param>
/// <returns><</returns>
private string getMd5Method2(string input)
{
    MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

    byte[] myData = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

    StringBuilder sBuilder = new StringBuilder();

 for (int i = 0; i < myData.Length; i++)
    {
  sBuilder.Append(myData[i].ToString("x2"));
 }

 return string.Format("ComputeHash(32)：{0}", sBuilder.ToString());
}

/// <summary>
/// 32位加密：直接使用HashPasswordForStoringInConfigFile
/// </summary>
/// <param name="input"></param>
/// <returns><</returns>
private string getMd5Method3(string input)
{
 string myReturn = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(input, "MD5");

 return string.Format("HashPasswordForStoringInConfigFile(32)：{0}", myReturn.ToString());
}