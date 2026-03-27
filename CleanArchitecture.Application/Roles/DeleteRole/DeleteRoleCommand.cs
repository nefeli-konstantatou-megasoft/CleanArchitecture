
namespace CleanArchitecture.Application.Roles.DeleteRole;

public class DeleteRoleCommand(string name) : ICommand
{
    public string Name { get; set; } = name;
}
