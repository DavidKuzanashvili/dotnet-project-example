using System.Text.Json.Serialization;

namespace App.Infrastructure.Authorization.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        Admin = 1,
        User = 2
    }
}
