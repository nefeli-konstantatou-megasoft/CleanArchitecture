using CleanArchitecture.Application.Authentication;

namespace CleanArchitecture.Application.Users.LoginUser;

public class LoginUserCommandHandler(
    IAuthenticationService authenticationService) : ICommandHandler<LoginUserCommand>
{
    private readonly IAuthenticationService _authenticationService = authenticationService;

    public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var loginResult = await _authenticationService.LoginUserAsync(request.UserName, request.Password);

        if(loginResult is not null)
            return Result.Error(loginResult);

        return Result.Ok();
    }
}
