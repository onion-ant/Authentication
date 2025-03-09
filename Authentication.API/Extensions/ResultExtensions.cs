using Authentication.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<TValue>(this Result<TValue> result)
    {
        if (result.IsCreated)
        {
            return new CreatedResult(result.CreatedLocation, result.Value);
        }
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }

        return ToActionError(result);
    }

    public static IActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return new OkResult();
        }

        return ToActionError(result);
    }

    private static ObjectResult ToActionError(Result result)
    {
        return new ObjectResult(result.Error) { StatusCode = result.Error.StatusCode };
    }
}
