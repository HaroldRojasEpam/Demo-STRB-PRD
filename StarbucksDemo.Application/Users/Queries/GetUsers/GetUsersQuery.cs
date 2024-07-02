using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarbucksDemo.Core.Entities;

namespace StarbucksDemo.Application.Users.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<IEnumerable<User>>
    {
    }
}
