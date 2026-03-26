using CleanArchitecture.Application.Authentication;

namespace CleanArchitecture.Application.Users.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IAuthenticationService _authenticationService;

    public RegisterUserCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.RegisterUserAsync(request.UserName, request.UserEmail, request.Password);
        if (result.Succeeded)
            return Result.Ok();

        var errors = new ErrorMessage(UserErrors.RegisterFailed.Code, UserErrors.RegisterFailed.Body + string.Join(", ", result.Errors));
        return Result.Error(errors);
    }
}
