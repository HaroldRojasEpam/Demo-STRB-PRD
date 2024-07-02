using MediatR;
using StarbucksDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksDemo.Application.Users.Queries.GetUsers
{
    public class GetUsersHandler(IUsersRepository repository) : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllUsers(cancellationToken);
        }
    }
}
