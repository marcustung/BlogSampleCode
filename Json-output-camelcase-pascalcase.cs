[HttpGet]
public ActionResult<UserInfo> Get()
{
    return new UserInfo()
    {
        Name = "Marcus",
        LoginTime = DateTime.Now.ToString(CultureInfo.CurrentCulture)
    };
}
[HttpGet("{id}")]
public ActionResult<Dictionary<string, string>> Get(int id)
{
    return new Dictionary<string, string>
    {
        { "Name", "Marcus" },
        { "LoginTime", DateTime.Now.ToString(CultureInfo.CurrentCulture) }
    };
}
public class UserInfo
{
    public string Name { get; set; }
    public string LoginTime { get; set; }
}