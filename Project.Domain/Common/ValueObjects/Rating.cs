﻿using Project.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Common.ValueObjects;

public sealed class Rating : ValueObject
{
    public double Value { get; }

    private Rating(double value)
    {
        Value = value;
    }

    public static Rating CreateNew(double val)
    {
        return new Rating(val);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}