using System;
using System.Collections.Generic;
using System.Linq;

namespace FavoDeMel.Domain.Core.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Adiciona action para cada elemento da lista
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">Lista</param>
        /// <param name="itemAction">Objeto Action</param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> itemAction)
        {
            foreach (var item in items)
            {
                itemAction(item);
            }
        }
        /// <summary>
        /// Indica se a lista está nula, ou se não contém nenhum registro
        /// </summary>
        /// <typeparam name="T">TipoEvento da lista</typeparam>
        /// <param name="lista">Lista</param>
        /// <returns>Retorna valor booleano, indicando se a lista está nula ou vazia</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> lista)
        {
            return lista == null || !lista.Any();
        }

        /// <summary>
        /// Indica se a lista não está nula e que contenha itens.
        /// </summary>
        /// <typeparam name="T">TipoEvento da lista</typeparam>
        /// <param name="lista">Lista</param>
        /// <returns>Retorna valor booleano, indicando se a lista não está nula ou vazia</returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> lista)
        {
            return !lista.IsEmpty();
        }

    }
}