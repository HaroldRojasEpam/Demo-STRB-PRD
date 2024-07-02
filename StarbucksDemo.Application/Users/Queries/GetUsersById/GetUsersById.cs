using MediatR;
using StarbucksDemo.Core.Entities;

namespace StarbucksDemo.Application.Users.Queries.GetUsersById
{
    public class GetUsersById : IRequest<User>
    {
        public Guid Id { get; set; }
    }
}
