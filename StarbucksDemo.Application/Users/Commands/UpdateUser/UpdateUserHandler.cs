using MediatR;
using StarbucksDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksDemo.Application.Users.Commands.UpdateUser
{
    public class UpdateUserHandler(IUsersRepository repository) : IRequestHandler<UpdateUserCommand, bool>
    {
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await repository
                .UpdateUser(
                    id: request.Id,
                    firstName: request.FirstName,
                    lastName: request.LastName,
                    cardNumber: request.CardNumber,
                    email: request.Email,
                    cancellationToken: cancellationToken
                );
        }
    }
}
