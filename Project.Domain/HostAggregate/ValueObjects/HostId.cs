using Project.Domain.Common.Models;


namespace Project.Domain.HostAggregate.ValueObjects;

public class HostId : ValueObject
{
    public Guid Value { get; }

    private HostId(Guid value)
    {
        Value = value;
    }

    public static HostId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static HostId CreateUnique(Guid guid)
    {
        return new(guid);
    }
}
