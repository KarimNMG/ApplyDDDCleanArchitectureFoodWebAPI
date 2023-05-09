using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Project.Application.Authentication.Commands.Register;
using Project.Application.Authentication.Common;
using System.Reflection;

namespace Project.Application.Common.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr

{

    private readonly IEnumerable<IValidator<TRequest>>? _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>>? validators = null)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {


        #region Third Approche

        if (_validators is null || !_validators.Any())
        {
            return await next();
        }

        var errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(iError => iError.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new ValidationFailure(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToList();

        if (errors.Any())
        {
            // return validation result
            TryCreateResponseFromErrors(errors, out var response);
            return response;
        }

        return await next();
        #endregion

        #region Second Approche
        //(dynamic result, var isError) = Validate(request);
        //return isError ? (TResponse)result : await next();
        #endregion

        #region First Approche
        //if (_validator is null)
        //{
        //    return await next();
        //}
        //var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        //if (validationResult.IsValid)
        //{
        //    return await next();
        //}

        //var errors = validationResult
        //    .Errors
        //    .ConvertAll(validationError =>
        //        Error.Validation(validationError.PropertyName, validationError.ErrorMessage));
        //return (dynamic)errors;
        /*
            it's dangrous to use dynamic but 
            if exception occurred we can use error controller and throw Exception
            With the error list
         */
        #endregion

    }

    private (List<Error> errors, bool isError) Validate(TRequest request)
    {
        var errors = new List<Error> { Error.Conflict() };
        return (errors, errors.Any());
    }

    private static bool TryCreateResponseFromErrors(
        List<ValidationFailure> validationFailures,
        out TResponse response)
    {
        List<Error> errors = validationFailures.ConvertAll(x => Error.Validation(
                code: x.PropertyName,
                description: x.ErrorMessage));

        response = (TResponse?)typeof(TResponse)
            .GetMethod(
                name: nameof(ErrorOr<object>.From),
                bindingAttr: BindingFlags.Static | BindingFlags.Public,
                types: new[] { typeof(List<Error>) })?
            .Invoke(null, new[] { errors })!;

        return response is not null;
    }
}