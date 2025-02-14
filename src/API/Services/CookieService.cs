using API.Options;

namespace API.Services;

public class CookieService(CookieSettings settings)
{
    public CookieOptions GetOptions()
    {
        return new CookieOptions
        {
            HttpOnly = false,
            MaxAge = settings.Expires 
        };
    }
}