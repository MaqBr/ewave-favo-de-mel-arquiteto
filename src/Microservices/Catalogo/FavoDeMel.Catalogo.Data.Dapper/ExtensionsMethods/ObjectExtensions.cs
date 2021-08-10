using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace FavoDeMel.Catalogo.Data.Dapper.ExtensionsMethods
{
    /// <summary>
    /// Classe de extensões de objetos.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Verifica se o objeto é simples ou complexo.
        /// </summary>
        /// <param name="instance">Instância do objeto.</param>
        /// <returns></returns>
        public static bool IsSimpleObject(this object instance)
        {
            var type = instance.GetType();
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return type.GetGenericArguments()[0].IsSimpleObject();
            }
            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(decimal));
        }

        /// <summary>
        /// Verifica se um objeto é nulo ou possui valores.
        /// </summary>
        /// <param name="obj">Instância do objeto.</param>
        /// <returns></returns>
        public static bool IsEmptyObject(this object obj)
        {
            var empty = obj == null;
            if (obj != null)
            {
                obj.GetType().GetProperties().ForEach(property =>
                {
                    if (property.GetValue(obj) != null) empty = false;
                });
            }
            return empty;
        }

        /// <summary>
        /// Converte o objeto em um dicionário com as suas propriedades e valores.
        /// </summary>
        /// <param name="instance">Instância do objeto.</param>
        /// <returns></returns>
        public static IDictionary<string, object> ToObjectDictionary(this object instance)
        {
            var result = new Dictionary<string, object>();
            if (instance != null)
            {
                if (instance is IDictionary<string, object>)
                    return (IDictionary<string, object>)instance;

                if (instance is IEnumerable<object>)
                {
                    var enumerable = (IEnumerable<dynamic>)instance;
                    foreach (var item in enumerable)
                    {
                        if (IsSimpleObject(item) == false)
                        {
                            foreach (var subItem in ToObjectDictionary(item))
                                result.Add(subItem.Name, subItem.Value);
                        }
                        else
                        {
                            result.Add(item.Name, item.Value);
                        }
                    }
                }
                else
                {
                    instance.GetType().GetProperties().ForEach(property =>
                    {
                        if (property != null) result.Add(property.Name, property.GetValue(instance));
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// Converte as propriedades do objeto em um dicionário com seus valores.
        /// </summary>
        /// <param name="instance">Instância do objeto.</param>
        /// <returns></returns>
        public static IDictionary<string, object> PropertiesToDictionary(this object instance)
        {
            var response = new Dictionary<string, object>();
            instance.GetType().GetProperties().ForEach(property => response.Add(property.Name, property.GetValue(instance)));
            return response;
        }

        /// <summary>
        /// Define o valor de uma propriedade do objeto.
        /// </summary>
        /// <param name="instance">Instância do objeto.</param>
        /// <param name="propertyName">Nome da propriedade.</param>
        /// <param name="value">Valor a ser definido.</param>
        public static void SetPropertyValue(this object instance, string propertyName, object value)
        {
            var property = instance.GetType().GetProperty(propertyName);
            if (property != null)
            {
                property.SetValue(instance, value);
            }
        }

        /// <summary>
        /// Retorna o valor de uma propriedade do objeto.
        /// </summary>
        /// <typeparam name="T">TipoEvento do objeto.</typeparam>
        /// <param name="instance">Instância do objeto.</param>
        /// <param name="propertyName">Nome da propriedade.</param>
        /// <returns></returns>
        public static object GetPropertyValue<T>(this object instance, string propertyName)
        {
            return instance.GetType().GetProperty(propertyName).GetValue(instance);
        }

        public static string CreatePath<T>(this T instance, Expression<Func<T, object>> pathExpression)
        {
            var getMemberNameFunc = new Func<Expression, MemberExpression>(expression => expression as MemberExpression);
            var memberExpression = getMemberNameFunc(pathExpression.Body);
            var names = new Stack<string>();

            while (memberExpression != null)
            {
                names.Push(memberExpression.Member.Name);
                memberExpression = getMemberNameFunc(memberExpression.Expression);
            }

            return string.Join(".", names);
        }

        /// <summary>
        /// Converte uma lista de objetos em uma tabela com os registros.
        /// </summary>
        /// <typeparam name="T">TipoEvento do objeto.</typeparam>
        /// <param name="dados">Lista com os dados.</param>
        /// <param name="tableName">Nome da tabela.</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this ICollection<T> dados, string tableName)
        {
            var props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable(tableName ?? typeof(T).Name);

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                Type collumnType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                table.Columns.Add(prop.Name, collumnType);
            }

            object[] values = new object[props.Count];

            foreach (T item in dados)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }

                table.Rows.Add(values);
            }

            return table;
        }

        /// <summary>
        /// Copia as propriedades de um objeto para outro.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public static void CopyPropertiesTo<TSource, TDestination>(this TSource source, TDestination dest)
        {
            var sourceProps = typeof(TSource).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TDestination).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }
            }
        }
    }
}
