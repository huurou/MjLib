namespace MjLib;

public abstract class ValueObject<T> : IEquatable<ValueObject<T>>
    where T : ValueObject<T>
{
    public abstract bool EqualsCore(ValueObject<T>? other);

    public bool Equals(ValueObject<T>? other)
    {
        return EqualsCore(other);
    }

    public override bool Equals(object? obj)
    {
        return EqualsCore(obj as T);
    }

    public abstract int GetHashCodeCore();

    public override int GetHashCode()
    {
        return GetHashCodeCore();
    }

    public static bool operator ==(ValueObject<T>? x, ValueObject<T>? y)
    {
        return x?.EqualsCore(y) ?? y is null;
    }

    public static bool operator !=(ValueObject<T>? x, ValueObject<T>? y)
    {
        return !(x == y);
    }
}