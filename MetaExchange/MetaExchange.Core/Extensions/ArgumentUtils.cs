using System.Runtime.CompilerServices;

namespace MetaExchange.Core.Extensions;

public static class ArgumentUtils
{
    public static T NotNull<T>(this T value, [CallerArgumentExpression("value")] string name = null)
    {
        if (value != null)
            return value;

        throw new ArgumentNullException(name);
    }
}