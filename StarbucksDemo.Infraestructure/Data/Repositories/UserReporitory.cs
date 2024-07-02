using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StarbucksDemo.Application.Users;
using StarbucksDemo.Core.Entities;
using StarbucksDemo.Core.Exceptions;
using StarbucksDemo.Infraestructure.Data.Models;
using EnumType = StarbucksDemo.Core.Enums.EntityType;

namespace StarbucksDemo.Infraestructure.Data.Repositories
{
    internal class UserRepository : IUsersRepository
    {
        private readonly ApplicationDbContext context;
        public UserRepository(ApplicationDbContext _applicationDbContext)
        {
            this.context = _applicationDbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken)
        {
            var result = await this.context.Users.AsNoTracking().ToListAsync(cancellationToken);
            return result;
        }

        public async Task<User> CreateUser(string firstName, string lastName, int cardNumber, string email, CancellationToken cancellationToken)
        {
            var user = new User
            {
                CardNumber = cardNumber,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            var id = this.context.Add(user).Entity.Id;
            await this.context.SaveChangesAsync(cancellationToken);
            var result = await this.context.Users
                .Where(r => r.Id == id)
                .AsNoTracking()
                .FirstAsync(cancellationToken);
            return user;
        }

        public async Task<User> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var result = await this.context.Users
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            NullObjectException.ThrowObjectNullable(result, EnumType.User);
            return result;
        }

        public async Task<bool> UpdateUser(Guid id, string firstName, string lastName, int cardNumber, string email, CancellationToken cancellationToken)
        {
            try
            {
                var user = this.context.Users.FirstOrDefault(x => x.Id == id);
                NullObjectException.ThrowObjectNullable(user, EnumType.User);

                user.FirstName = firstName;
                user.LastName = lastName;
                user.Email = email;
                user.CardNumber = cardNumber;
                user.DateModified = DateTime.Now;

                _ = this.context.Update(user);
                _ = await this.context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }

        }

        public async Task<bool> DeleteUser(Guid UserId, CancellationToken cancellationToken)
        {
            try
            {
                _ = this.context.Remove(this.context.Users.Single(x => x.Id == UserId));
                _ = await this.context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
