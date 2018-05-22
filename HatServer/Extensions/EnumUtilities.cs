using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace HatServer.Extensions
{
    public static class EnumUtilities
    {
        [NotNull]
        public static string GetEnumDescription<TEnum>([NotNull] this TEnum item)
            => item.GetType()
                   .GetField(item.ToString())
                   .GetCustomAttributes(typeof(DescriptionAttribute), false)
                   .Cast<DescriptionAttribute>()
                   .FirstOrDefault()?.Description ?? string.Empty;

        public static void SeedEnumValues<T, TEnum>(this DbSet<T> dbSet, Func<TEnum, T> converter)
            where T : class => Enum.GetValues(typeof(TEnum))
            .Cast<object>()
            .Select(value => converter((TEnum)value))
            .ToList()
            .ForEach(instance => dbSet.Add(instance));
    }
}
