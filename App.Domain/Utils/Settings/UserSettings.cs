namespace App.Domain.Utils.Settings
{
    public class UserSettings
    {
        public UserSettings()
        {
            UserId = "";
            UserName = "";
            UserRoles = new string[0];
        }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string[] UserRoles { get; set; }
    }

}
