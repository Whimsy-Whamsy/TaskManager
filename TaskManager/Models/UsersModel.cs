namespace TaskManager.Models;

public class UsersModel
{
    public int Id { get; set; }
    public string FisrtName { get; set; }
    public string LastName { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
}