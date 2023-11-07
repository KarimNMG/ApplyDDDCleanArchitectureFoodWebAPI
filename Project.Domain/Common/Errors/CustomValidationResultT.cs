using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Common.Errors;

internal class CustomValidationResult<TValue> : Result<TValue>, IValidationResult
{
    public Error[] Errors { get; }

    public CustomValidationResult(Error[] errors)
        : base(default, false, IValidationResult.ValidationError) => Errors = errors;
    public static CustomValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
}
