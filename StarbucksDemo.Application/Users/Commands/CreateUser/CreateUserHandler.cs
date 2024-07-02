using MediatR;
using StarbucksDemo.Core.Entities;

namespace StarbucksDemo.Application.Users.Commands.CreateUser
{
    public class CreateUserHandler(IUsersRepository repository) : IRequestHandler<CreateUserCommand, User>
    {
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await repository
                .CreateUser(
                    firstName: request.FirstName,
                    lastName: request.LastName,
                    cardNumber: request.CardNumber,
                    email: request.Email,
                    cancellationToken: cancellationToken
                );
        }
    }
}
