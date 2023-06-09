﻿using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project.WebApi.Common.Http;

namespace Project.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.All(err => err.Type == ErrorType.Validation))
        {
            ValidationProblem(errors);
        }

        // We can add custom logic here to handle different types of errors


        HttpContext.Items[HttpContextItemKeys.Erros] = errors;
        return Problem(errors[0]);
    }


    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in errors)
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        return ValidationProblem(modelStateDictionary);
    }
    private IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
        return Problem(statusCode: statusCode, title: error.Description);
    }
}