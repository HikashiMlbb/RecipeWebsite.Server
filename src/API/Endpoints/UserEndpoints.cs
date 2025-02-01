using Application.Users.UseCases;
using Application.Users.UseCases.GetById;
using Application.Users.UseCases.Login;
using Application.Users.UseCases.Register;
using Application.Users.UseCases.Update;
using Domain.UserEntity;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app, string path)
    {
        var route = app.MapGroup(path).WithTags("User Endpoints:");

        route.MapPost("/login", Login);
        route.MapPost("/signup", SignUp);
        route.MapPut("/", Update);
        route.MapGet("/{id:int}", Get);
    }

    #region Private Implementation of Endpoints

    private static async Task<IResult> Login([FromBody]UserDto dto, [FromServices]UserLogin userLogin)
    {
        var loginResult = await userLogin.LoginAsync(dto);

        return loginResult.IsSuccess
            ? Results.Ok(loginResult.Value!)
            : Results.Problem(statusCode: 401, title: loginResult.Error!.Code, detail: loginResult.Error.Description);
    }

    private static async Task<IResult> SignUp([FromBody]UserDto dto, [FromServices]UserRegister userRegister)
    {
        var signUpResult = await userRegister.RegisterAsync(dto);

        if (signUpResult.IsSuccess) return Results.Ok(signUpResult.Value);
        if (signUpResult.Error == UserErrors.UserAlreadyExists) return Results.Conflict(signUpResult.Error);
        if (signUpResult.Error == UserDomainErrors.UsernameUnallowedSymbols || signUpResult.Error == UserDomainErrors.UsernameLengthOutOfRange) return Results.BadRequest(signUpResult.Error);

        return Results.StatusCode(500);
    }

    private static async Task<IResult> Get(int id, [FromServices]UserGetById userGet)
    {
        var result = await userGet.GetUserAsync(id);

        return result is null
            ? Results.NotFound()
            : Results.Ok(new
            {
                Id = result.Id.Value,
                Username = result.Username.Value,
                Recipes = result.Recipes.Select(x => new
                {
                    Id = x.Id.Value,
                    Title = x.Title.Value,
                    ImageName = x.ImageName.Value,
                    Difficulty = x.Difficulty.ToString(),
                    CookingTime = x.CookingTime.ToString(),
                    Rating = x.Rate.Value,
                    Votes = x.Rate.TotalVotes
                })
            });
    }

    private static async Task<IResult> Update([FromBody]UserUpdateDto dto, [FromServices]UserUpdate userUpdate)
    {
        var result = await userUpdate.UpdateAsync(dto);

        if (result.IsSuccess) return Results.Ok();
        
        if (result.Error == UserErrors.UserNotFound) return Results.NotFound();
        if (result.Error == UserErrors.PasswordIsIncorrect) return Results.BadRequest(result.Error);
        
        return Results.StatusCode(500);
    }
        
    #endregion
}