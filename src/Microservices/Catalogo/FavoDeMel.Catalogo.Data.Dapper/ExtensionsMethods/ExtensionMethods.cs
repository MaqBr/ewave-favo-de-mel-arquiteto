using System;
using System.Collections.Generic;

namespace FavoDeMel.Catalogo.Data.Dapper.ExtensionsMethods
{
    public static class ExtensionMethods
    {
        //
        // Summary:
        //     Iterate through all items in the list executing an action.
        //
        // Parameters:
        //   source:
        //
        //   action:
        //     Action execute for each item in the list.
        //
        // Type parameters:
        //   T:
        //public static void ForEach<T>(this IEnumerable<T> source, Action<T> action);

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
    }
}