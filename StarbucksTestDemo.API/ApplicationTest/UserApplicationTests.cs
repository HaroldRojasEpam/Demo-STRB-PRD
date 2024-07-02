using MediatR;
using NSubstitute;
using Shouldly;
using Queries = StarbucksDemo.Application.Users.Queries;
using Commands = StarbucksDemo.Application.Users.Commands;
using Entities = StarbucksDemo.Core.Entities;
using DTO = StarbucksDemo.Core.DataTransferObject;
using Routes = StarbucksDemo.Routes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;

namespace StarbucksTestDemo.API.Unit.ApplicationTest
{
    public class UserApplicationTests
    {
        #region Ok Region
        [Fact]
        public async Task CreateUsers_Ok()
        {
            var mediator = Substitute.For<IMediator>();
            var request = new Commands.CreateUser.CreateUserCommand
            {
                CardNumber = 1,
                Email = "harold_rojas@epam.com",
                FirstName = "Test First Name",
                LastName = "Test Last Name"
            };

            _ = mediator
                .Send(Arg.Any<Commands.CreateUser.CreateUserCommand>())
                .ReturnsForAnyArgs(
                    new Entities.User{
                         Id = Guid.Empty,
                         CardNumber = 1,
                         Email = "harold_rojas@epam.com",
                         FirstName = "Test First Name",
                         LastName = "Test Last Name",
                         DateCreated = DateTime.Now,
                         DateModified = DateTime.Now
                    }
                );
            var response = await Routes.UserRoutes.CreateUser(request, mediator);

            var result = response.ShouldBeOfType<Created<Entities.User>>();

            result.StatusCode.ShouldBe(StatusCodes.Status201Created);

            var value = result.Value.ShouldBeOfType<Entities.User>();

            _ = value.Id.ShouldBeOfType<Guid>();
            value.Id.ShouldBe(Guid.Empty);
            _ = value.FirstName.ShouldBeOfType<string>();
            value.FirstName.ShouldBe("Test First Name");
            _ = value.LastName.ShouldBeOfType<string>();
            value.LastName.ShouldBe("Test Last Name");
            _ = value.CardNumber.ShouldBeOfType<int>();
            value.CardNumber.ShouldBe(1);
            _ = value.Email.ShouldBeOfType<string>();
            value.Email.ShouldBe("harold_rojas@epam.com");
        }

        [Fact]
        public async Task GetUsers_Ok()
        {
            var mediator = Substitute.For<IMediator>();
            _ = mediator
                .Send(Arg.Any<Queries.GetUsers.GetUsersQuery>())
                .ReturnsForAnyArgs([
                    new Entities.User{
                         Id = Guid.Empty,
                         CardNumber = 1,
                         Email = "harold_rojas@epam.com",
                         FirstName = "Test First Name",
                         LastName = "Test Last Name",
                         DateCreated = DateTime.Now,
                         DateModified = DateTime.Now
                    }
                ]);
            var response = await Routes.UserRoutes.GetUsers(mediator);

            var result = response.ShouldBeOfType<Ok<IEnumerable<Entities.User>>>();

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);

            var value = result.Value.ShouldBeAssignableTo<IEnumerable<Entities.User>>();

            _ = value.FirstOrDefault().Id.ShouldBeOfType<Guid>();
            value.FirstOrDefault().Id.ShouldBe(Guid.Empty);
            _ = value.FirstOrDefault().FirstName.ShouldBeOfType<string>();
            value.FirstOrDefault().FirstName.ShouldBe("Test First Name");
            _ = value.FirstOrDefault().LastName.ShouldBeOfType<string>();
            value.FirstOrDefault().LastName.ShouldBe("Test Last Name");
            _ = value.FirstOrDefault().CardNumber.ShouldBeOfType<int>();
            value.FirstOrDefault().CardNumber.ShouldBe(1);
            _ = value.FirstOrDefault().Email.ShouldBeOfType<string>();
            value.FirstOrDefault().Email.ShouldBe("harold_rojas@epam.com");
        }

        [Fact]
        public async Task GetUserById_Ok()
        {
            var mediator = Substitute.For<IMediator>();
            _ = mediator
                .Send(Arg.Any<Queries.GetUsersById.GetUsersById>())
                .ReturnsForAnyArgs(
                    new Entities.User{
                         Id = Guid.Empty,
                         CardNumber = 1,
                         Email = "harold_rojas@epam.com",
                         FirstName = "Test First Name",
                         LastName = "Test Last Name",
                         DateCreated = DateTime.Now,
                         DateModified = DateTime.Now
                    }
                );
            var response = await Routes.UserRoutes.GetUsersById(Guid.Empty, mediator);

            var result = response.ShouldBeOfType<Ok<Entities.User>>();

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);

            var value = result.Value.ShouldBeOfType<Entities.User>();

            _ = value.Id.ShouldBeOfType<Guid>();
            value.Id.ShouldBe(Guid.Empty);
            _ = value.FirstName.ShouldBeOfType<string>();
            value.FirstName.ShouldBe("Test First Name");
            _ = value.LastName.ShouldBeOfType<string>();
            value.LastName.ShouldBe("Test Last Name");
            _ = value.CardNumber.ShouldBeOfType<int>();
            value.CardNumber.ShouldBe(1);
            _ = value.Email.ShouldBeOfType<string>();
            value.Email.ShouldBe("harold_rojas@epam.com");
        }
        [Fact]
        public async Task UpdateUser_NoContent()
        {
            var mediator = Substitute.For<IMediator>();
            var request = new DTO.UserUpdateDTO
            {
                CardNumber = 1,
                Email = "harold_rojas@epam.com",
                FirstName = "Test First Name",
                LastName = "Test Last Name"
            };
            _ = mediator.Send(Arg.Any<Commands.UpdateUser.UpdateUserCommand>()).ReturnsForAnyArgs(true);

            var response = await Routes.UserRoutes.UpdateUser(Guid.NewGuid(), request,mediator);
            var result = response.ShouldBeOfType<NoContent>();
            result.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
        }
        [Fact]
        public async Task DeleteUser_NoContent()
        {
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Any<Commands.DeleteUser.DeleteUserCommand>()).ReturnsForAnyArgs(true);
            var response = await Routes.UserRoutes.DeleteUser(Guid.NewGuid(), mediator);
            var result = response.ShouldBeOfType<NoContent> ();
            result.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
        }
        #endregion
    }
}
