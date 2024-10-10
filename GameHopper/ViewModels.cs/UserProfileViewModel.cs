using System;

namespace GameHopper.ViewModels.cs;

public class UserProfileViewModel
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public IFormFile? ProfilePicture { get; set; }
}
