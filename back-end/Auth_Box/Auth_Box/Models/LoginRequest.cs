namespace Auth_Box.Models;

public class LoginRequest
{
    public string email { get; set; } 
    public string password { get; set; } 

    public LoginRequest()
    {
        
    }
}

public class RegisterRequest
{
    public string email { get; set; }
    public string password { get; set; }
    public string full_name { get; set; }
    public string phone_number { get; set; }

    public RegisterRequest()
    {
        
    }
}