namespace App.Domain.Utils.Settings
{
    public class UserSettings
    {
        public UserSettings()
        {
            UserId = string.Empty;
            Roles = new string[0];
        }

        public string UserId { get; set; }
        public string[] Roles { get; set; }
    }
}
