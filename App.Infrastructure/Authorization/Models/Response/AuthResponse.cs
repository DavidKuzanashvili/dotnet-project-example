using System.Collections.Generic;

namespace App.Infrastructure.Authorization.Models.Response
{
    public class AuthResponse
    {
        public AuthResponse()
        {
            Errors = new List<AuthErrorResponse>();
        }

        public bool Succeded { get; set; }
        public List<AuthErrorResponse> Errors { get; set; }
        public TokenResponse TokenResponse { get; set; }
    }
}
