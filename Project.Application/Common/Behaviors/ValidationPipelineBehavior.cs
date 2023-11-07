using FluentValidation;
using MediatR;
using Project.Domain.Common.Errors;
using CustomValidationResult = Project.Domain.Common.Errors.CustomValidationResult;

namespace Project.Application.Common.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result

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
        // Validate Request
        // If any errors, return validation result
        // Otherwise, return next()

        #region Third Approche

        if (_validators is null || !_validators.Any())
        {
            return await next();
        }

        var errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new Error(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            // return validation result
            return createValidationResult<TResponse>(errors);
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

    private static TResult createValidationResult<TResult>(Error[] errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (CustomValidationResult.WithErrors(errors) as TResult)!;
        }
        object validationResult = typeof(CustomValidationResult)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(CustomValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;

        return (TResult)validationResult;
    }
}