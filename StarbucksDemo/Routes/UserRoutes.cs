using MediatR;
using Microsoft.AspNetCore.Mvc;
using Queries = StarbucksDemo.Application.Users.Queries;
using Commands = StarbucksDemo.Application.Users.Commands;
using StarbucksDemo.Core.Entities;
using StarbucksDemo.Application.Users.Commands.CreateUser;
using StarbucksDemo.Core.DataTransferObject;
namespace StarbucksDemo.Routes
{
    public static class UserRoutes
    {
        public static WebApplication MapUserRoutes(this WebApplication app)
        {
            var routeResult = app.MapGroup("/api/user")
                                .WithTags("user")
                                .WithOpenApi();

            _ = routeResult.MapGet("/", GetUsers)
                .Produces<IEnumerable<User>>()
                .WithSummary("Gets all the users register in the database");

            _ = routeResult.MapGet("/{id}", GetUsersById)
                .Produces<User>()
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Gets the user register in the database, firter by the unique id provided by the system");

            _ = routeResult.MapPost("/", CreateUser)
                .Produces<IEnumerable<User>>(StatusCodes.Status201Created)
                .WithSummary("Create a new user");

            _ = routeResult.MapPut("/{id}", UpdateUser)
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update the information of the user, filter by id");

            _ = routeResult.MapDelete("/{id}", DeleteUser)
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Remove the user, firlter by id");

            return app;
        }

        public static async Task<IResult> GetUsers([FromServices] IMediator mediator)
        {
            try
            {
                return Results.Ok(await mediator.Send(new Queries.GetUsers.GetUsersQuery()));
            }
            catch (Exception ex) 
            { 
                return Results.Problem(ex.Message,ex.StackTrace,StatusCodes.Status500InternalServerError);
            }
        }

        public static async Task<IResult> CreateUser([FromBody] CreateUserCommand request, [FromServices] IMediator mediator)
        {
            try
            {
                var response = await mediator.Send(new Commands.CreateUser.CreateUserCommand{
                    CardNumber = request.CardNumber,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                });
                return Results.Created($"/api/user/{response.Id}", response);
            }
            catch (Exception ex) 
            {
                return Results.Problem(ex.Message, ex.StackTrace, StatusCodes.Status500InternalServerError);
            }
        }

        public static async Task<IResult> GetUsersById([FromRoute] Guid id, [FromServices] IMediator mediator)
        {
            try
            {
                var response = await mediator.Send(new Queries.GetUsersById.GetUsersById
                {
                    Id = id
                });
                return Results.Ok(response);
            }
            catch (Exception ex) 
            {
                return Results.Problem(ex.Message, ex.StackTrace, StatusCodes.Status500InternalServerError);
            }
        }

        public static async Task<IResult> DeleteUser([FromRoute] Guid id, [FromServices] IMediator mediator)
        {
            try
            {
                _ = await mediator.Send(new Commands.DeleteUser.DeleteUserCommand
                {
                    Id = id
                });
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message, ex.StackTrace, StatusCodes.Status500InternalServerError);
            }
        }

        public static async Task<IResult> UpdateUser([FromRoute] Guid id, [FromBody] UserUpdateDTO request, [FromServices] IMediator mediator)
        {
            try
            {
                _ = await mediator.Send(new Commands.UpdateUser.UpdateUserCommand
                {
                    Id = id,
                    CardNumber = request.CardNumber,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                });
                return Results.NoContent();
            }
            catch(Exception ex)
            {
                return Results.Problem(ex.Message, ex.StackTrace, StatusCodes.Status500InternalServerError);
            }
        }
        
    }
}
