namespace AppLogin.ViewModels
{
    public class LoginVM
    {
        public string Email { get; set; } = string.Empty; // Default to an empty string to avoid null reference issues
        public string Password { get; set; } = string.Empty; // Default to an empty string to avoid null reference issues        
    }
}
