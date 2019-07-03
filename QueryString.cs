// 設定
Response.Redirect("Default.aspx?參數名稱=" + 參數值)
Server.Transfer("Default.aspx?參數名稱=" + 參數值)

// 取得
// 指定特定key
string q = Request.QueryString["參數名稱"];
Response.Write(q);

// 取得集合
NameValueCollection nvc = Request.QueryString; 
foreach (string key in nvc.Keys)
{
    Response.Write("{0} => {1}", key, nvc[key]);
}

// 使用Server.UrlEncode進行編碼
String myURL = "http://www.ABC.com/Search.aspx?q=" + Server.UrlEncode("你好嗎");
Response.Write("<a href=" + myURL + "> 你好嗎 </a>");

// 使用Server.UrlDecode進行解碼
String myDecodedString = Server.UrlDecode(URI);
Response.Write("Decoded：" + myDecodedString );