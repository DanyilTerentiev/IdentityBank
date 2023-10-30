namespace IdentityBank.Application.Models.Response;

public class SignInResponse
{
    public SignInResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
}

