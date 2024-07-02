using MediatR;
using StarbucksDemo.Core.Entities;
using StarbucksDemo.Core.Exceptions;
using EnumType = StarbucksDemo.Core.Enums.EntityType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksDemo.Application.Users.Queries.GetUsersById
{
    public class GetUsersByIdHandler(IUsersRepository repository) : IRequestHandler<GetUsersById, User>
    {
        public async Task<User> Handle(GetUsersById request, CancellationToken cancellationToken)
        {
            var result = await repository.GetUserById(request.Id, cancellationToken);
            NullObjectException.ThrowObjectNullable(result, EnumType.User);
            return result;
        }
    }
}
