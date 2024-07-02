using MediatR;
using StarbucksDemo.Application.Users.Queries.GetUsersById;
using StarbucksDemo.Core.Entities;
using StarbucksDemo.Core.Exceptions;
using EnumType = StarbucksDemo.Core.Enums.EntityType;

namespace StarbucksDemo.Application.Users.Commands.DeleteUser
{
    public class DeleteUserHandler(IUsersRepository repository) : IRequestHandler<DeleteUserCommand, bool>
    {
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.GetUserById(request.Id, cancellationToken);
            NullObjectException.ThrowObjectNullable(result, EnumType.User);
            return await repository.DeleteUser(request.Id, cancellationToken);
        }

    }
}
