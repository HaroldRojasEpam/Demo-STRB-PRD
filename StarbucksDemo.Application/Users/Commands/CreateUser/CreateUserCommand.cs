using MediatR;
using StarbucksDemo.Core.Entities;

namespace StarbucksDemo.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CardNumber { get; set; }
        public string Email { get; set; }
    }
}
