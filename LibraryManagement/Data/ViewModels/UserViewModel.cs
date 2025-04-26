namespace LibraryManagement.Data.ViewModels;

public class UserViewModel
{
    public string Username { get; set; }

    public string? PhoneNumber { get; set; }

    public List<string> Roles { get; set; }
}