using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarbucksDemo.Core.Entities;

namespace StarbucksDemo.Application.Users
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken);
        Task<User> GetUserById(Guid id, CancellationToken cancellationToken);
        Task<User> CreateUser(string firstName, string lastName, int cardNumber, string email, CancellationToken cancellationToken);
        Task<bool> UpdateUser(Guid id, string firstName, string lastName, int cardNumber, string email, CancellationToken cancellationToken);
        Task<bool> DeleteUser(Guid UserId, CancellationToken cancellationToken);
    }
}
