namespace SMS.Application.Settings
{
    public class EmailSettings
    {
        public string Host { get; set; } = ConfigManager.Host;
        public int Port { get; set; } = ConfigManager.Port;
        public string From { get; set; } = ConfigManager.From;
        public string UserName { get; set; } = ConfigManager.UserName;
        public string Password { get; set; } = ConfigManager.Password;
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; } = true;
    }
}
