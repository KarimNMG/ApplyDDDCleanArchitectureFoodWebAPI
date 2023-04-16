

using Project.Domain.Common.Models;

namespace Project.Domain.MenuAggregate.ValueObjects;

public sealed class MenuId : ValueObject
{
    public Guid Value { get; }

    private MenuId(Guid value)
    {
        Value = value;
    }

    public static MenuId CreateUnique()
    {
        return new(Guid.NewGuid());
    } //  factory method pattern



    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
