namespace SimpleStocks.Models.UserLogin
{
    public class ResponseBody
    {
        public int StatusCode { get; set; }
        public string StatusText { get; set; }
        public LoginResponse User { get; set; }
    }
}
