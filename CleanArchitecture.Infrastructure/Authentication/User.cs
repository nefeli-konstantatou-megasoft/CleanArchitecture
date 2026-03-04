using CleanArchitecture.Domain.Articles;
using CleanArchitecture.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Authentication;

public class User : IdentityUser, IUser
{
    public List<Article> Articles { get; set; } = [];
}
