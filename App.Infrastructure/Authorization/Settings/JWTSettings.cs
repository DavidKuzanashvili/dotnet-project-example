namespace App.Infrastructure.Authorization.Settings
{
    public class JWTSettings
    {
        public string Key { get; set; }
        public string ExpireMinute { get; set; }
        public string RefreshKey { get; set; }
        public string RefreshExpireMinute { get; set; }
    }
}
