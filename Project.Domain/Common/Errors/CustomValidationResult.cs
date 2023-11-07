using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Common.Errors;

public sealed class CustomValidationResult : Result, IValidationResult
{
    private CustomValidationResult(Error[] errors)
        : base(false, IValidationResult.ValidationError) => Errors = errors;

    public Error[] Errors { get; }

    public static CustomValidationResult WithErrors(Error[] errors) => new(errors);
}
