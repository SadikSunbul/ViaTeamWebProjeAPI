namespace Core.Application.Dtos;

public class UserForRegisterDto : IDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Job { get; set; }
    public string Country { get; set; }

    public UserForRegisterDto()
    {
        Email = string.Empty;
        Password = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        Job = string.Empty;
        Country = string.Empty;
    }

    public UserForRegisterDto(string email, string password, string firstName, string lastName,string job,string country)
    {
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Job = job;
        Country = country;
    }
}
